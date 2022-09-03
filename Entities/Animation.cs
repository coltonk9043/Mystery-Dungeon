// Colton K
// A class representing an active animation. This is not a sequence of events.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonGame.Entities
{
    public class Animation
    {
        // Variables
        private Texture2D[] frames;
        private int currentFrame = 0;
        private float frameInterval;
        private float currentFrameTime;
        private int numFrames;
        private bool repeating;
        private bool active;

        /// <summary>
        /// Constructor representing a generic animation.
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="timePerFrame"></param>
        /// <param name="repeating"></param>
        /// <param name="activeImmediately"></param>
        public Animation(Texture2D[] frames, float timePerFrame, bool repeating, bool activeImmediately)
        {
            this.frames = frames;
            this.frameInterval = timePerFrame;
            this.currentFrameTime = 0.0f;
            this.repeating = repeating;
            this.active = activeImmediately;
            this.numFrames = frames.Length;
            this.currentFrame = 0;
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void Start()
        {
            if (this.active)
                return;
            this.active = true;
        }

        /// <summary>
        /// Pauses the animation on the current frame.
        /// </summary>
        public void Pause()
        {
            if (!this.active)
                return;
            this.active = false;
        }

        /// <summary>
        /// Stops the animation and resets the frame to the start.
        /// </summary>
        public void Stop()
        {
            if (!this.active)
                return;
            this.active = false;
            this.currentFrame = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // If the animation is not active, do not perform any action.
            if (!this.active)
                return;
            // Count up the current frame time.
            this.currentFrameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            // If the total frame time is above the internal needed per frame, cycle the animation frames.
            if ((double)this.currentFrameTime > (double)this.frameInterval)
            {
                // If the animation is a repeating animation, loop the current frames.
                if (this.repeating)
                {
                    this.currentFrame = (this.currentFrame + 1) % this.numFrames;
                }
                else
                // Otherwise, go until the animation is complete.
                {
                    ++this.currentFrame;
                    if (this.currentFrame == this.numFrames)
                        this.Stop();
                }
                // Set the current frame time to 0 to continue iterating.
                this.currentFrameTime = 0.0f;
            }
        }

        public Texture2D getCurrentTexture() => this.frames[this.currentFrame];

        public bool isActive() => this.active;
    }
}
