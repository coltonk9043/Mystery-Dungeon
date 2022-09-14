using DungeonGame.Levels;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon.Levels
{
    public class WorldGenerator
    {
        private int width;
        private int height;

        private World w;
        private Random random = new Random();


        public WorldGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public World Generate()
        {
            Container container = new Container(0, 0, width, height);
            RoomTree map = this.GenerateBSP(container, 5);

            Layer[] tiles = new Layer[4];
            tiles[0] = new Layer(width, height, true, false);
            tiles[1] = new Layer(width, height, false, false);
            tiles[2] = new Layer(width, height, true, false);
            tiles[3] = new Layer(width, height, true, false);
            Container[] leafs = map.GetLeafs();

            // Generates rooms based off of this Binary Space Tree
            for (int i = 0; i < leafs.Length; i++)
            {
                Container c = leafs[i];
                int x = c.x + random.Next(0, (int)Math.Floor((double)(c.width / 3)));
                int y = c.y + random.Next(0, (int)Math.Floor((double)(c.height / 3)));
                int w = c.width - (x - c.x);
                int h = c.height - (y - c.y);
                w -= random.Next(0, w / 3);
                h -= random.Next(0, w / 3);

                for(int x1 = x; x1 <= w; x1++)
                {
                    for (int y1 = y; y1 <= h; y1++)
                    {
                        tiles[1].setTile(x1, y1, 2);
                    }
                }
            }

            // Generates the 

            w = new World(width, height, 4, tiles);
            return w;
        }

        private RoomTree GenerateBSP(Container container, int iterations)
        {
            RoomTree root = new RoomTree(container);
            if (iterations == 0)
            {
                return root;
            }
            else
            {
                Container[] split = SplitContainers(container);
                root.SetLeftChild(GenerateBSP(split[0], iterations - 1));
                root.SetRightChild(GenerateBSP(split[1], iterations - 1));
            }
            return root;
        }

        private Container[] SplitContainers(Container container)
        {
            Container[] containers = new Container[2];
            if(random.Next(0,1) == 0)
            {
                float ratio1 = 0;
                float ratio2 = 0;

                while(ratio1 < 0.45f || ratio2 < 0.45f)
                {
                    containers[0] = new Container(container.x, container.y, random.Next(1, container.width), container.height);
                    containers[1] = new Container(container.x + containers[0].width, container.y, container.width - containers[0].width, container.height);

                    ratio1 = containers[0].width / containers[0].height;
                    ratio2 = containers[1].width / containers[1].height;
                }
            }
            else
            {
                containers[0] = new Container(container.x, container.y, container.width , random.Next(1, container.height));
                containers[1] = new Container(container.x , container.y + containers[0].height, container.width, container.height - containers[0].height);
                //float ratio1 = containers[0].height / containers[0].width;
                //float ratio2 = containers[1].height / containers[1].width;
                //if (ratio1 < 0.45 || ratio2 < 0.45)
                //{
                //    return SplitContainers(container);
                //}
            }
            return containers;
        }
    }

    public class RoomTree
    {
        Container Leaf;
        RoomTree LeftChild;
        RoomTree RightChild;

        public RoomTree(Container leaf)
        {
            this.Leaf = leaf;
        }

        public Container[] GetLeafs()
        {
            if(this.LeftChild == null && this.RightChild == null)
            {
                return new Container[1] {this.Leaf}; ;
            }
            else
            {
                Container[] leftLeafs = this.LeftChild.GetLeafs();
                Container[] rightLeafs = this.RightChild.GetLeafs();
                Container[] combined = new Container[leftLeafs.Length + rightLeafs.Length];
                Array.Copy(leftLeafs, combined, leftLeafs.Length);
                Array.Copy(rightLeafs, 0, combined, leftLeafs.Length, rightLeafs.Length);
                return combined;
            }
            
        }

        public void SetLeftChild(RoomTree roomTree)
        {
            this.LeftChild = roomTree;
        }

        public void SetRightChild(RoomTree roomTree)
        {
            this.RightChild = roomTree;
        }
    }

    public class Container
    {
        public int x;
        public int y;
        public int width;
        public int height;

        Vector2 center;

        public Container(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.width = Math.Max(w, 2);
            this.height = Math.Max(h, 2);
            this.center = new Vector2((this.x + this.width / 2), (this.y + this.height / 2));
        }
    }
}
