using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameName1
{
    class Fighter
    {
        private float speed = 1.8f;
        private float jump = -5.0f;
        private float drag = 0.8f;
        public Vector2 velocity;
        private Texture2D image;
        public Vector2 position;
        public Vector2 gravity = new Vector2(0, 2);
        bool onground = true;

        // the elapsed amount of time the frame has been shown for
        float time;
        // duration of time to show each frame
        float frameTime = 0.5f;
        // an index of the current frame being shown
        int frameIndex;
        // total number of frames in our spritesheet
        int totalFrames = 2;
        // define the size of our animation frame
        int frameHeight = 88;
        int frameWidth = 80;
        int beginFrame;
        string currentAnimation;

        public Fighter(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("chief_ss");
        }

        public void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            // Process elapsed time
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
    
            while (time > frameTime)
            {
                if (beginFrame != totalFrames)
                {
                    // Play the next frame in the SpriteSheet
                    frameIndex++;
                }
                // reset elapsed time
                time = 0f;
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(frameIndex * frameWidth,

                                               0, frameWidth, frameHeight);

            // Calculate position and origin to draw in the center of the screen
            //Vector2 position = new Vector2();
            Vector2 origin = new Vector2(frameWidth / 2.0f, frameHeight);

            spritebatch.Begin();
            spritebatch.Draw(image, position, source, Color.White, 0.0f,
            origin, 1.0f, SpriteEffects.None, 0.0f);
            spritebatch.End();

            if (frameIndex > totalFrames) frameIndex = beginFrame;

        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                SetAnimation("PUNCH");
                velocity.X -= speed / 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                SetAnimation("PUNCH");
                velocity.X += speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (onground == true)
                {
                    velocity.Y += jump;
                    onground = false;
                }
            }
            else
            {
                SetAnimation("IDLE");
            }

            if (onground == false)
            {
                velocity += gravity;
            }

            velocity *= drag;
            position += velocity;
            if (position.Y >= 200)
                {
                    onground = true;
                }
        }
        private void SetAnimation(string animation){
            if (animation != currentAnimation)
            {
                switch (animation)
                {
                    case "IDLE":
                        SetFrames(0, 0);
                        break;
                    case "PUNCH":
                        SetFrames(1, 1);
                        break;
                }
            }
            currentAnimation = animation;
        }
        private void SetFrames(int _beginFrame, int lenght){
            this.beginFrame = _beginFrame;
            frameIndex = _beginFrame;
            totalFrames = _beginFrame+lenght;
        }
    }
}
