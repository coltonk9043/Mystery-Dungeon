// Colton K
// A class representing the ingame world.
using Dungeon.Levels;
using Dungeon.Lighting;
using DungeonGame.Entities;
using DungeonGame.Entities.NPCs;
using DungeonGame.Entities.Player;
using DungeonGame.Levels.TerrainFeatures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.IO;
using Ray = Dungeon.Lighting.Ray;
using BoundingBox = DungeonGame.Entities.BoundingBox;
using Dungeon.Levels.TerrainFeatures;
using Dungeon.Entities;
using Dungeon.Projectiles;
using System.Linq;
using Dungeon.Utilities;
using System.Diagnostics;
using Dungeon;

namespace DungeonGame.Levels
{
    public enum Layers
    {
        Underwater = 0,
        Water = 1,
        Ground = 2,
        Decals = 3,
        Wall = 4,
    }

    public class World
    {
        private GenericGame game;

        // Render Targets
        private RenderTarget2D renderTargetShadows;
        private RenderTarget2D renderTargetWorld;
        private RenderTarget2D renderTargetEntities;
        private RenderTarget2D renderTargetDynamicLighting;
        private RenderTarget2D renderTargetStaticLighting;
        private RenderTarget2D renderTargetLighting;
        private Effect shadowMask;
        private Effect lightingEffect;
        private VertexPosition[] lightingVAO;


        // Map Information
        private string dungeonName;
        public int sizeX;
        public int sizeY;
        public int layers;
        private Layer[] tiles;
        private bool gridmap = false;
        private string tilesetTextureFile;
        private Texture2D[] tileTextures;
        private List<Entity> entityList = new List<Entity>();
        private List<AbstractPlayer> playerList = new List<AbstractPlayer>();

        // Realtime Lighting Variables
        private float time = 0.0f;
        private Color skyColor = new Color(255, 255, 255);
        private float skyAlpha;
        private const int rays = 360;
        int[] ind = new int[(rays + 2) * 3]; // Real-time Lighting Indices

        private Texture2D lightingTexture;
        private readonly BlendState lightingBlend = new BlendState()
        {
            ColorSourceBlend = Blend.One, // multiplier of the source color
            ColorBlendFunction = BlendFunction.Add, // function to combine colors
            ColorDestinationBlend = Blend.SourceAlpha, // multiplier of the destination color
            AlphaSourceBlend = Blend.One, // multiplier of the source alpha
            AlphaBlendFunction = BlendFunction.Add, // function to combine alpha
            AlphaDestinationBlend = Blend.Zero, // multiplier of the destination alpha
        };

        /// <summary>
        /// Constructor to generate a 'default' world.
        /// </summary>
        /// <param name="dungeonName"></param>
        /// <param name="game"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="layers"></param>
        public World(GenericGame game, string dungeonName, int width, int height)
        {
            this.game = game;
            this.dungeonName = dungeonName;
            this.renderTargetShadows = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetWorld = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetEntities = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetDynamicLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetStaticLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.shadowMask = game.GetContentManager().Load<Effect>("ShadowMask");
            this.lightingEffect = game.GetContentManager().Load<Effect>("Lighting");
            this.lightingTexture = game.GetContentManager().Load<Texture2D>("Textures/Lighting/Torch");
            this.sizeX = width;
            this.sizeY = height;

            // Creates new layers.

            this.layers = Enum.GetNames(typeof(Layers)).Length;
            this.tiles = new Layer[layers];
            for (int i = 0; i < this.tiles.Length; i++)
            {
                tiles[i] = new Layer(width, height, false, false);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        tiles[i].setTile(x, y, -1);
                    }
                }
            }
            this.tilesetTextureFile = "Textures/Tiles/default_floor_tileset";

            this.tileTextures = TextureUtils.createTextureArrayFromFile(game.Content, this.tilesetTextureFile, 16, 16);

            // Precalculates shadow indicices for efficiency.
/*            for (int i = 0; i < rays; i++)
            {
                ind[(i * 3)] = 0;
                ind[(i * 3) + 1] = i;
                ind[(i * 3) + 2] = i + 1;
            }*/
        }

        /// <summary>
        /// Constructor to generate the world from a file.
        /// </summary>
        /// <param name="dungeonName"></param>
        /// <param name="game"></param>
        /// <param name="contentManager"></param>
        public World(GenericGame game, string dungeonName)
        {
            WriteDefaultValues(dungeonName);
            this.game = game;
            this.dungeonName = dungeonName;
            this.renderTargetShadows = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetWorld = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetEntities = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetDynamicLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetStaticLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.renderTargetLighting = new RenderTarget2D(game.GraphicsDevice, game.GraphicsDevice.PresentationParameters.BackBufferWidth, game.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
            this.shadowMask = game.GetContentManager().Load<Effect>("ShadowMask");
            this.lightingEffect = game.GetContentManager().Load<Effect>("Lighting");
            this.lightingTexture = game.GetContentManager().Load<Texture2D>("Textures/Lighting/Torch");
            // Reads from a file representing the tile 
            if (File.Exists(dungeonName))
            {
                Console.WriteLine("File " + dungeonName + " found. Reading file...");
                // Opens a Binary Reader to prepare for reading the file.
                using (BinaryReader binaryReader = new BinaryReader((Stream)File.Open(dungeonName, FileMode.Open)))
                {
                    // Gets the size of the world, number of layers, and the tilemap used for map textures.
                    this.sizeX = binaryReader.ReadInt32();
                    this.sizeY = binaryReader.ReadInt32();
                    this.layers = binaryReader.ReadInt32();
                    this.tilesetTextureFile = binaryReader.ReadString();

                    // Creates new layers.
                    this.tiles = new Layer[layers];
                    for (int i = 0; i < layers; i++)
                        tiles[i] = new Layer(this.sizeX, this.sizeY, binaryReader.ReadBoolean(), binaryReader.ReadBoolean());

                    // Creates a texture array containing every texture used in the world.
                    this.tileTextures = TextureUtils.createTextureArrayFromFile(game.Content, this.tilesetTextureFile, 16, 16);

                    // Reads the file for the world data.
                    for (int layer = 0; layer < layers; layer++)
                    {
                        for (int y = 0; y < this.sizeY; ++y)
                        {
                            for (int x = 0; x < this.sizeX; ++x)
                            {
                                int texture = binaryReader.ReadInt32();
                                if (texture != -1) this.tiles[layer].setTile(x, y, texture);
                            }
                        }
                    }

                    // Continues reading the file until it reaches the end of the file. This section of the file contains any entities that need to be read.
                    while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                    {
                        switch (binaryReader.ReadString())
                        {
                            case "tree":
                                binaryReader.ReadInt32();
                                this.entityList.Add(new TreeEntity(this, new Vector3(binaryReader.ReadInt32(), binaryReader.ReadInt32(), 0.0f)));
                                break;
                            case "torch":
                                binaryReader.ReadInt32();
                                this.entityList.Add(new TorchEntity(this, new Vector3(binaryReader.ReadInt32(), binaryReader.ReadInt32(), 0.0f)));
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }

            // Precalculates shadow indicices for efficiency.
            for (int i = 0; i <= 360; i++)
            {
                ind[(i * 3)] = 0;
                ind[(i * 3) + 1] = i;
                ind[(i * 3) + 2] = i + 1;
            }

            // ** Hardcoded entities as I have not implemented a way to create items in the world editor.
            this.addEntity((Entity)new NPCDavid(this, new Vector3(72f, 38f, 0.0f)));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(72f, 100f, 0.0f), Game1.Items.GetItem("elesis_claymore")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(45f, 46f, 0.0f), Game1.Items.GetItem("elesis_claymore")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(125f, 150f, 0.0f), Game1.Items.GetItem("torch")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(150f, 150f, 0.0f), Game1.Items.GetItem("ruby")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(150f, 175f, 0.0f), Game1.Items.GetItem("ruby")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(150f, 200f, 0.0f), Game1.Items.GetItem("ruby_staff")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(175f, 150f, 0.0f), Game1.Items.GetItem("sapphire")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(175f, 175f, 0.0f), Game1.Items.GetItem("sapphire")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(175f, 200f, 0.0f), Game1.Items.GetItem("sapphire_staff")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(200f, 150f, 0.0f), Game1.Items.GetItem("emerald")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(200f, 175f, 0.0f), Game1.Items.GetItem("emerald")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(200f, 200f, 0.0f), Game1.Items.GetItem("emerald_staff")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(225f, 150f, 0.0f), Game1.Items.GetItem("diamond")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(225f, 175f, 0.0f), Game1.Items.GetItem("diamond")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(225f, 200f, 0.0f), Game1.Items.GetItem("diamond_staff")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 150f, 0.0f), Game1.Items.GetItem("copper_ingot")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 175f, 0.0f), Game1.Items.GetItem("copper_hatchet")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 200f, 0.0f), Game1.Items.GetItem("copper_scythe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 225f, 0.0f), Game1.Items.GetItem("copper_pickaxe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 250f, 0.0f), Game1.Items.GetItem("copper_sword")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(250f, 275f, 0.0f), Game1.Items.GetItem("copper_knife")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 150f, 0.0f), Game1.Items.GetItem("iron_ingot")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 175f, 0.0f), Game1.Items.GetItem("iron_hatchet")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 200f, 0.0f), Game1.Items.GetItem("iron_scythe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 225f, 0.0f), Game1.Items.GetItem("iron_pickaxe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 250f, 0.0f), Game1.Items.GetItem("iron_sword")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(275f, 275f, 0.0f), Game1.Items.GetItem("iron_knife")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 150f, 0.0f), Game1.Items.GetItem("tungsten_ingot")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 175f, 0.0f), Game1.Items.GetItem("tungsten_hatchet")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 200f, 0.0f), Game1.Items.GetItem("tungsten_scythe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 225f, 0.0f), Game1.Items.GetItem("tungsten_pickaxe")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 250f, 0.0f), Game1.Items.GetItem("tungsten_sword")));
            this.addEntity((Entity)new ItemEntity(this, new Vector3(300f, 275f, 0.0f), Game1.Items.GetItem("tungsten_knife")));
        }

        /// <summary>
        /// Saves the current world. TODO: Change to be specific to the World Editor GUI.
        /// </summary>
        public void Save()
        {
            // Opens a Binary Writer to prepare for writing to the file.
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(this.dungeonName, FileMode.Create)))
            {
                // Writes the necessary information regarding size, and saves every tile/texture into the file.
                binaryWriter.Write(this.sizeX);
                binaryWriter.Write(this.sizeY);
                binaryWriter.Write(this.layers);
                binaryWriter.Write(this.tilesetTextureFile);
                for (int layer = 0; layer < layers; layer++)
                {
                    for (int y = 0; y < this.sizeY; ++y)
                    {
                        for (int x = 0; x < this.sizeX; ++x)
                        {
                            int tile = this.tiles[layer].getTile(x, y);
                            binaryWriter.Write(tile);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Function 
        /// </summary>
        /// <returns></returns>
        public GenericGame GetCurrentGame()
        {
            return this.game;
        }

        /// <summary>
        /// Toggles the gridmap on or off.
        /// </summary>
        public void toggleGridmap()
        {
            this.gridmap = !this.gridmap;
        }

        public Texture2D[] getTileTextures() => this.tileTextures;

        public void FixedUpdate(GameTime gameTime)
        {

        }

        /// <summary>
        /// Update function that updates the world and all of the entities within.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            /*
             * Section of the update function to manage Time.
             */

            this.time = (float)((this.time + 2.0) % 24000.0);
            if (this.time >= 0.0 && this.time < 4000.0)
            {
                this.skyColor = new Color((int)(81.0 + 174.0 * (this.time / 4000.0)), (int)(81.0 + 174.0 * (this.time / 4000.0)), (int)(81.0 + 174.0 * (this.time / 4000.0)));
                this.skyAlpha = 255.0f;
            }
            else if (this.time >= 12000.0 && this.time < 18000.0)
            {
                this.skyColor = new Color((int)(255.0 - 255.0 * ((this.time - 12000.0) / 6000.0)), (int)(255.0 - 237 * ((this.time - 12000.0) / 6000.0)), (int)(255.0 - 220.0 * ((this.time - 12000.0) / 6000.0)));
                this.skyAlpha = (float)(255.0 - 255.0 * ((this.time - 12000.0) / 6000.0));
            }
            else if (this.time >= 22000.0 && this.time < 24000.0)
            {
                this.skyColor = new Color((int)(0.0 + 81.0 * ((this.time - 22000.0) / 2000.0)), (int)(18.0 + 81.0 * ((this.time - 22000.0) / 2000.0)), (int)(81.0f * ((this.time - 22000.0) / 2000.0)));
                this.skyAlpha = (float)(0.0 + 81.0 * ((this.time - 22000.0) / 2000.0));
            }

            Debug.WriteLine(this.time);

            // Sorts the entity list based on their proximity to the camera (Y Axis)
            this.entityList.Sort(new Comparison<Entity>(this.compareEntities));

            // Creates a list of entities to be removed (In the case of a despawn or death)
            List<Entity> removeList = new List<Entity>();


            // Otherwise, update every entity in the world and determine whether or not it needs to be removed.
            foreach (Entity entity in this.entityList.ToList())
            {
                entity.Update(gameTime, this);
                if (entity.removed)
                    removeList.Add(entity);
            }
            // Remove each entity that must be removed.
            foreach (Entity entity in removeList)
                this.entityList.Remove(entity);

        }

        /// <summary>
        /// Draws the World and entities on the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            // Tile Rendering variables.
            Vector2 entityPosAsTilePos = camera.GetEntityPosAsTilePos();
            int minX = (int)Math.Max(0.0f, entityPosAsTilePos.X - 16f);
            int maxX = (int)MathHelper.Min((float)this.sizeX, entityPosAsTilePos.X + 18f);
            int minY = (int)MathHelper.Max(0.0f, entityPosAsTilePos.Y - 8f);
            int maxY = (int)MathHelper.Min((float)this.sizeY, entityPosAsTilePos.Y + 9f);

            // Gets all of the collidable bounding tiles, such that light will render around it.
            List<Edge> boundingList = new List<Edge> { new Edge(0, 0, sizeX * 16, 0), new Edge(sizeX * 16, 0, sizeX * 16, sizeY * 16), new Edge(0, sizeY * 16, sizeX * 16, sizeY * 16), new Edge(0, 0, 0, sizeY * 16) };

            // Renders the entity "Shadow" layer.
            spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTargetShadows);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, transformMatrix: camera.GetTransform());
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            foreach (Entity entity in this.entityList)
            {
                if (camera.DistanceFromEntity(entity) <= 300.0)
                {
                    entity.RenderEntityShadow(spriteBatch);
                    if (entity is TerrainEntity)
                    {
                        if (entity.GetBoundingBox() == null) continue;
                        boundingList.Add(new Edge(entity.GetBoundingBox().X, entity.GetBoundingBox().Y, entity.GetBoundingBox().X + entity.GetBoundingBox().Width, entity.GetBoundingBox().Y));
                        boundingList.Add(new Edge(entity.GetBoundingBox().X + entity.GetBoundingBox().Width, entity.GetBoundingBox().Y, entity.GetBoundingBox().X + entity.GetBoundingBox().Width, entity.GetBoundingBox().Y + entity.GetBoundingBox().Height));
                        boundingList.Add(new Edge(entity.GetBoundingBox().X, entity.GetBoundingBox().Y + entity.GetBoundingBox().Height, entity.GetBoundingBox().X + entity.GetBoundingBox().Width, entity.GetBoundingBox().Y + entity.GetBoundingBox().Height));
                        boundingList.Add(new Edge(entity.GetBoundingBox().X, entity.GetBoundingBox().Y, entity.GetBoundingBox().X, entity.GetBoundingBox().Y + entity.GetBoundingBox().Height));
                    }

                }
            }
            spriteBatch.End();

            // Render the realtime shadows.
            /* spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTargetDynamicLighting);
             spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, transformMatrix: new Matrix?(camera.GetTransform()));
             spriteBatch.GraphicsDevice.Clear(Color.White);*/

            // TODO: Cleanup so that this is not created each draw call.
            /*for (int x = minX; x < maxX; x++)
            {
                for (int y = minX; y < maxX; y++)
                {
                    for (int layers = 0; layers < tiles.Length; layers++)
                    {
                        if (tiles[layers].getLightCollides())
                        {
                            if (tiles[1].getTile(x, y) != -1)
                            {
                                if (tiles[1].getTile(x - 1, y) == -1) boundingList.Add(new Edge((x * 16), (y * 16), (x * 16), (y * 16) + 16));
                                if (tiles[1].getTile(x, y - 1) == -1) boundingList.Add(new Edge((x * 16), (y * 16), (x * 16) + 16, (y * 16)));
                                if (tiles[1].getTile(x + 1, y) == -1) boundingList.Add(new Edge((x * 16) + 16, (y * 16), (x * 16) + 16, (y * 16) + 16));
                                if (tiles[1].getTile(x, y + 1) == -1) boundingList.Add(new Edge((x * 16), (y * 16) + 16, (x * 16) + 16, (y * 16) + 16));
                            }
                        }
                    }

                }
            }*/

            // Calculates each vertice for the realtime light rays using raytracing.
            /*var vertices = new VertexPosition[rays + 1];
            vertices[0].Position = new Vector3(camera.position.X, camera.position.Y, 0);
            for (int i = 1; i <= rays; i += 1)
            {
                Ray ray = new Ray(new Vector2(camera.position.X, camera.position.Y), (Math.PI / 180) * (i - 1));
                IntersectionPoint point = ray.simulate(boundingList);
                // If a point of intersection exists, create a new vertice.
                if (point != null)
                {
                    vertices[i].Position = new Vector3(point.X, point.Y, 0);
                }
            }
            spriteBatch.GraphicsDevice.DrawUserIndexedPrimitives<VertexPosition>(PrimitiveType.TriangleStrip, vertices, 0, rays, ind, 0, rays * 3);
            spriteBatch.End();*/


            // Render World
            spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTargetWorld);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, transformMatrix: new Matrix?(camera.GetTransform()));
            spriteBatch.GraphicsDevice.Clear(Color.SkyBlue);
            for (int layer = 0; layer < layers; layer++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        int tileTexture = this.tiles[layer].getTile(x, y);
                        if (tileTexture != -1)
                        {
                            spriteBatch.Draw(this.tileTextures[tileTexture], new Vector2((float)(x * 16), (float)(y * 16)), new Rectangle?(new Rectangle(0, 0, 16, 16)), Color.White);
                        }
                        if (gridmap)
                        {
                            VertexPosition[] _vertexPositionColors = new[]
                            {
                            new VertexPosition(new Vector3((x * 16), (y * 16), 1)),
                            new VertexPosition(new Vector3((x * 16) + 16, (y * 16), 1)),
                            new VertexPosition(new Vector3((x * 16) + 16, (y * 16) + 16, 1)),
                            new VertexPosition(new Vector3((x * 16), (y * 16) + 16, 1))
                            };
                            spriteBatch.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, _vertexPositionColors, 0, 4);
                        }
                    }
                }
            }
            spriteBatch.End();

            // Entity Rendering
            spriteBatch.GraphicsDevice.SetRenderTarget(this.renderTargetEntities);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, transformMatrix: new Matrix?(camera.GetTransform()));
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);

            Vector3[] lightPosition = new Vector3[32];
            Vector4[] lightColour = new Vector4[32];
            float[] lightIntensity = new float[32];
            float[] lightRadius = new float[32];
            int currentLightIndex = 0;

            foreach (Entity entity in this.entityList)
            {
                if (camera.EuclidianDistanceFromEntity(entity) <= 300.0)
                    entity.Render(spriteBatch);
                if (entity is ILightSource)
                {
                    if (currentLightIndex >= 32) continue;
                    lightPosition[currentLightIndex] = Vector3.Transform(entity.position, this.game.GetCamera().GetLightingTransform());
                    lightColour[currentLightIndex] = ((ILightSource)entity).LightColor.ToVector4();
                    lightIntensity[currentLightIndex] = ((ILightSource)entity).LightIntensity;
                    lightRadius[currentLightIndex] = ((ILightSource)entity).LightRadius;
                    currentLightIndex++;
                }
            }
            spriteBatch.End();

            for (int i = currentLightIndex; i < 32; i++)
            {
                lightPosition[currentLightIndex] = new Vector3(0, 0, 0);
                lightColour[currentLightIndex] = new Vector4(0, 0, 0, 0);
                lightIntensity[currentLightIndex] = 0.0f;
                lightRadius[currentLightIndex] = 0.0f;
            }

            // Final Drawing
            spriteBatch.GraphicsDevice.SetRenderTarget(null);

            // Sort Lighting to be 

            this.lightingEffect.Parameters["DiffuseLighting"].SetValue(this.skyColor.ToVector4());
            this.lightingEffect.Parameters["LightPosition"].SetValue(lightPosition);
            this.lightingEffect.Parameters["LightColour"].SetValue(lightColour);
            this.lightingEffect.Parameters["LightIntensity"].SetValue(lightIntensity);
            this.lightingEffect.Parameters["LightRadius"].SetValue(lightRadius);
            this.lightingEffect.Parameters["LightCount"].SetValue(currentLightIndex);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, effect: this.lightingEffect);
            spriteBatch.Draw(this.renderTargetWorld, new Rectangle(0, 0, game.ScreenWidth, game.ScreenHeight), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(this.renderTargetShadows, new Rectangle(0, 0, game.ScreenWidth, game.ScreenHeight), new Color(255, 255, 255, 80));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, effect: this.lightingEffect);
            spriteBatch.Draw(this.renderTargetEntities, new Rectangle(0, 0, game.ScreenWidth, game.ScreenHeight), Color.White);
            spriteBatch.End();
        }

        /// <summary>
        /// Adds an entity to the entity list.
        /// </summary>
        /// <param name="entity">Entity that will be added to the entity list.</param>
        public void addEntity(Entity entity) => this.entityList.Add(entity);

        /// <summary>
        /// Adds a player to the player list.
        /// </summary>
        /// <param name="entity">Player that will be added to the player list.</param>
        public void addPlayer(AbstractPlayer entity)
        {
            this.addEntity((Entity)entity);
            this.playerList.Add(entity);
        }

        /// <summary>
        /// Gets the list of entities in the world.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public List<Entity> getEntities() => this.entityList;

        /// <summary>
        /// Gets the list of players in the world.
        /// </summary>
        /// <returns>The list of players.</returns>
        public List<AbstractPlayer> getPlayers() => this.playerList;

        /// <summary>
        /// Returns the selected layer.
        /// </summary>
        /// <param name="layer">The index of the layer to be fetched.</param>
        /// <returns>The layer requested.</returns>
        public Layer getLayer(int layer)
        {
            return this.tiles[layer];
        }

        /// <summary>
        /// Returns a tile in a specific layer.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>The index (Texture) of a specific tile.</returns>
        public int getTile(int layer, int x, int y)
        {
            if ((x < 0 || x >= this.sizeX) || (y < 0 || y >= this.sizeY) || (layer < 0 || layer > layers))
            {
                return -1;
            }
            else
            {
                return this.tiles[layer].getTile(x, y);
            }
        }

        /// <summary>
        /// Sets a tile at a given position.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="id"></param>
        public void setTile(int layer, int x, int y, int id)
        {
            if (x >= this.sizeX || x < 0 || y >= this.sizeY || y < 0)
                return;
            this.tiles[layer].setTile(x, y, id);
        }

        /// <summary>
        /// Helper function to sort entities based of y axis.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int compareEntities(Entity x, Entity y)
        {
            if (x == null)
                return y == null ? 0 : -1;
            if (y == null)
                return 1;
            if ((double)x.position.Y < (double)y.position.Y)
                return -1;
            return (double)x.position.Y > (double)y.position.Y ? 1 : 0;
        }

        /// <summary>
        /// Basic function to 'autogenerate' a world if none is present.
        /// </summary>
        /// <param name="dungeonName"></param>
        public static void WriteDefaultValues(string dungeonName)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)File.Open(dungeonName, FileMode.Create)))
            {
                binaryWriter.Write(6);
                binaryWriter.Write(6);
                binaryWriter.Write(3);
                binaryWriter.Write("Textures/Tiles/default_floor_tileset");
                binaryWriter.Write(false);
                binaryWriter.Write(false);
                binaryWriter.Write(true);
                binaryWriter.Write(true);
                binaryWriter.Write(false);
                binaryWriter.Write(false);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(19);
                binaryWriter.Write(0);
                binaryWriter.Write(1);
                binaryWriter.Write(12);
                binaryWriter.Write(9);
                binaryWriter.Write(10);
                binaryWriter.Write(19);
                binaryWriter.Write(8);
                binaryWriter.Write(9);
                binaryWriter.Write(9);
                binaryWriter.Write(9);
                binaryWriter.Write(10);

                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(24);
                binaryWriter.Write(25);
                binaryWriter.Write(26);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(27);
                binaryWriter.Write(-1);
                binaryWriter.Write(28);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(29);
                binaryWriter.Write(-1);
                binaryWriter.Write(31);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(32);
                binaryWriter.Write(-1);
                binaryWriter.Write(34);
                binaryWriter.Write(-1);

                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(41);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(42);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write(-1);
                binaryWriter.Write("tree");
                binaryWriter.Write(0);
                binaryWriter.Write(32);
                binaryWriter.Write(25);
                binaryWriter.Write("tree");
                binaryWriter.Write(0);
                binaryWriter.Write(56);
                binaryWriter.Write(180);
                binaryWriter.Write("tree");
                binaryWriter.Write(0);
                binaryWriter.Write(15);
                binaryWriter.Write(75);
                binaryWriter.Write("torch");
                binaryWriter.Write(0);
                binaryWriter.Write(250);
                binaryWriter.Write(500);
            }
        }
    }
}
