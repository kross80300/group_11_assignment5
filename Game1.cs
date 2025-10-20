using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace project5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Spaceship _spaceship;
        private Model _spaceshipModel;
        private List<Planet> _planets;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spaceshipModel = Content.Load<Model>("meshes/shuttle");

            Vector3 start = new Vector3(800f, 100f, 0f);
            Vector3 end = new Vector3(-800f, 100f, 300f);
            float travelTime = 8f;
            float pauseTime = 4f;

            _spaceship = new Spaceship(_spaceshipModel, start, end, travelTime, pauseTime);

            _planets = new List<Planet>();

            Model planet1Mesh = Content.Load<Model>("meshes/planet1");
            Model planet2Mesh = Content.Load<Model>("meshes/planet2");
            Model planet3Mesh = Content.Load<Model>("meshes/planet3");

            _planets.Add(new Planet(planet1Mesh, Vector3.Zero, 200f, 1f, MathHelper.TwoPi / 6f));
            _planets.Add(new Planet(planet2Mesh, Vector3.Zero, 350f, 0.8f, MathHelper.TwoPi / 12f));
            _planets.Add(new Planet(planet3Mesh, Vector3.Zero, 500f, 0.6f, MathHelper.TwoPi / 18f));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _spaceship.Update(gameTime);

            foreach (var planet in _planets)
                planet.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Matrix view = Matrix.CreateLookAt(new Vector3(0, 300, 800),
                Vector3.Zero,
                Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                GraphicsDevice.Viewport.AspectRatio,
                1f,
                10000f);

            _spaceship.Draw(view, projection);

            foreach (var planet in _planets)
                planet.Draw(view, projection);

            base.Draw(gameTime);
        }
    }
}
