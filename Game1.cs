using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using group_11_assignment5;
using project5;

namespace group_11_assignment5
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Spaceship _spaceship;
        private Model _spaceshipModel;
        private List<Planet> _planets;
        
        private List<Moon> _moons;

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
            
            Planet planet1 = new Planet(planet1Mesh, Vector3.Zero, 150f, 1f, MathHelper.TwoPi / 8f, 0f, 1.2f);   
            Planet planet2 = new Planet(planet2Mesh, Vector3.Zero, 300f, 0.8f, MathHelper.TwoPi / 12f, MathHelper.Pi / 2f, 5.0f); 
            Planet planet3 = new Planet(planet3Mesh, Vector3.Zero, 450f, 0.6f, MathHelper.TwoPi / 16f, MathHelper.Pi, 0.2f);    

            _planets.Add(planet1);
            _planets.Add(planet2);
            _planets.Add(planet3);

            _moons = new List<Moon>();
            
            Model Moon1Mesh = Content.Load<Model>("meshes/moon1");
            Model Moon2Mesh = Content.Load<Model>("meshes/moon2");
            
            _moons.Add(new Moon(Moon1Mesh, 20f, planet1, 1.5f, MathHelper.TwoPi / 4f));
            _moons.Add(new Moon(Moon2Mesh, 30f, planet2, 1.2f, MathHelper.TwoPi / 6f));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _spaceship.Update(gameTime);

            foreach (var planet in _planets)
                planet.Update(gameTime);

            foreach (var moon in _moons)
                moon.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 600, 1000),
                new Vector3(0, 0, 0),
                Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f),
                GraphicsDevice.Viewport.AspectRatio,
                1f,
                10000f);

            _spaceship.Draw(view, projection);

            foreach (var planet in _planets)
                planet.Draw(view, projection);
            
            foreach (var moon in _moons)
                moon.Draw(view, projection);

            base.Draw(gameTime);
        }
    }
}