using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_11_assignment5;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    
    private Spaceship _spaceship;
    private Model _spaceshipModel;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _spaceshipModel = Content.Load<Model>("meshes/shuttle");

        Vector3 start = new Vector3(-300f, 0f, 0f);
        Vector3 end = new Vector3(300f, 0f, 0f);
        float travelTime = 5f;
        float pauseTime = 3f;
        
        _spaceship = new Spaceship(_spaceshipModel, start, end, travelTime, pauseTime);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _spaceship.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        Matrix view = Matrix.CreateLookAt(new Vector3(0, 50, 500),
            Vector3.Zero,
            Vector3.Up);
        
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.ToRadians(45f),
            GraphicsDevice.Viewport.AspectRatio,
            1f,
            10000f);
        
        _spaceship.Draw(view, projection);

        base.Draw(gameTime);
    }
}