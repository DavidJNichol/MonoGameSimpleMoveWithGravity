using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameSimpleMoveWithGravity;
using System;

namespace SimpleMovementWGravity
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Character edward;
        Character sylvester;          

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromTicks(333333);

            edward = new Character();
            sylvester = new Character();

        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //textures
            edward.texture = Content.Load<Texture2D>("edward");
            sylvester.texture = Content.Load<Texture2D>("sylvester");
            //locs
            edward.location = new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            sylvester.location = new Vector2(GraphicsDevice.Viewport.Width / 1.5f,
               GraphicsDevice.Viewport.Height / 1.5f);
            //direction
            edward.direction = new Vector2(1, 0);
            sylvester.direction = new Vector2(1, 0);
            //speed
            edward.speed = 10;
            sylvester.speed = 10;
            //gravity
            edward.gravityDirection = new Vector2(1, 0);
            edward.gravityAccel = 1.8f;

            sylvester.gravityDirection = new Vector2(1, 0);
            sylvester.gravityAccel = 1.8f;

            font = Content.Load<SpriteFont>("Arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;           

            //Main movement
            edward.location = edward.location + ((edward.direction * edward.speed) * (time / 1000));
            sylvester.location = sylvester.location + ((sylvester.direction * sylvester.speed) * (time / 1000));

            UpdateGravity();

            KeepCharacterInBounds();

            UpdateKeyboardInput();

            base.Update(gameTime);
        }

        private void UpdateGravity()
        {
            edward.direction = edward.direction + (edward.gravityDirection * edward.gravityAccel);
            sylvester.direction = sylvester.direction + (sylvester.gravityDirection * sylvester.gravityAccel);
        }

        private void KeepCharacterInBounds()
        {
            if (edward.location.X > GraphicsDevice.Viewport.Width - edward.texture.Width)
            {
                edward.direction = edward.direction * new Vector2(-1, 1);
                edward.location.X = GraphicsDevice.Viewport.Width - edward.texture.Width;
            }
            if (sylvester.location.X > GraphicsDevice.Viewport.Width - sylvester.texture.Width)
            {
                sylvester.direction = sylvester.direction * new Vector2(-1, 1);
                sylvester.location.X = GraphicsDevice.Viewport.Width - sylvester.texture.Width;
            }

            if (edward.location.X < 0)
            {
                edward.direction = edward.direction * new Vector2(-1, 1);
                edward.location.X = 0;
            }
            if (sylvester.location.X < 0)
            {
                sylvester.direction = sylvester.direction * new Vector2(-1, 1);
                sylvester.location.X = 0;
            }

            if (edward.location.Y > GraphicsDevice.Viewport.Height - edward.texture.Height)
            {
                edward.direction = edward.direction * new Vector2(1, -1);
                edward.location.Y = GraphicsDevice.Viewport.Height - edward.texture.Height;
            }

            if (sylvester.location.Y > GraphicsDevice.Viewport.Height - sylvester.texture.Height)
            {
                sylvester.direction = sylvester.direction * new Vector2(1, -1);
                sylvester.location.Y = GraphicsDevice.Viewport.Height - sylvester.texture.Height;
            }

            if (edward.location.Y < 0)
            {
                edward.direction = edward.direction * new Vector2(1, -1);
                edward.location.Y = 0;
            }
            if (sylvester.location.Y < 0)
            {
                sylvester.direction = sylvester.direction * new Vector2(1, -1);
                sylvester.location.Y = 0;
            }
        }
        //tricky
        private void UpdateSpeed()
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0) 
            {
                edward.speed = 200;
                sylvester.speed = 200;
            }
            else
            {
                edward.speed = 0;
                sylvester.speed = 0;
            }

        }

        private void UpdateKeyboardInput()
        {
            #region Keyboard Movement
            //Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                edward.gravityDirection = new Vector2(0, 1);
                sylvester.gravityDirection = new Vector2(0,1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                edward.gravityDirection = new Vector2(0, 1);
            }
            //Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                edward.gravityDirection = new Vector2(0, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                sylvester.gravityDirection = new Vector2(0, -1);
            }
            //Right
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                edward.gravityDirection = new Vector2(1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                sylvester.gravityDirection = new Vector2(1, 0);
            }
            //Left
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                edward.gravityDirection = new Vector2(-1, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                sylvester.gravityDirection = new Vector2(-1, 0);
            }
            #endregion
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);

            //Edward Textures
            spriteBatch.Begin();
            spriteBatch.Draw(edward.texture, edward.location, Color.White);
            spriteBatch.DrawString(font,
                string.Format("Edward Speed:{0}\n Edward Dir:{1}\nEdward GravityDir:{2}\n Edward GravtyAccel:{3}",
                edward.speed, edward.direction, edward.gravityDirection, edward.gravityAccel),
                new Vector2(10, 10),
                Color.GreenYellow);

            spriteBatch.DrawString(font, "Edward and Sylvester's wacky gravity adventure", new Vector2(250,0), Color.White);
            spriteBatch.DrawString(font, "Edward: Arrow Keys", new Vector2(270, 15), Color.GreenYellow);
            spriteBatch.DrawString(font, "Sylvester: WASD", new Vector2(435, 16), Color.Orange);

            //Sylvester textures
            spriteBatch.DrawString(font,
                string.Format("Sylvester Speed:{0}\n Sylvester Dir:{1}\nSylvester GravityDir:{2}\n Sylvester GravtyAccel:{3}",
                sylvester.speed, sylvester.direction, sylvester.gravityDirection, sylvester.gravityAccel),
                new Vector2(10, 80),
                Color.Orange);

            spriteBatch.Draw(sylvester.texture, sylvester.location, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}