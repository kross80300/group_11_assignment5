using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_11_assignment5;

public class Spaceship
{
    
    private Model _mesh;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private float _totalTime;
    private float _currentTime;
    private bool _movingForward;

    private float _pauseTime;
    private float _currentPause;
    private bool _isPaused;

    private float _tiltAngle;
    
    public Spaceship(Model mesh, Vector3 _startPoint, Vector3 _endPoint, float _totalTime, float _pauseTime)
    {
        
        this._mesh = mesh;
        this._startPoint = _startPoint;
        this._endPoint = _endPoint;
        this._totalTime = _totalTime;
        this._currentTime = 0f;
        this._movingForward = true;
        this._pauseTime = _pauseTime;
        this._currentPause = 0f;
        this._isPaused = false;
        this._tiltAngle = 0f;
    }

    public void Update(GameTime gameTime)
    {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_isPaused)
        {
            _currentPause += elapsedTime;
            if (_currentPause >= _pauseTime)
            {
                _currentPause = 0f;
                _isPaused = false;
                _currentTime = 0f;
                _movingForward = true;
            }

            return;
        }

        if (_movingForward)
        {
            _currentTime += elapsedTime;
            _tiltAngle = MathHelper.Lerp(0f, 3.0f, _currentTime/_totalTime);
        }
        else
        {
            _currentTime -= elapsedTime;
            _tiltAngle = MathHelper.Lerp(0f, -3.0f, _currentTime/_totalTime);
        }

        if (_currentTime >= _totalTime)
        {
            _currentTime = _totalTime;
            _isPaused = true;
        }
        else if (_currentTime <= 0)
        {
            _currentTime = 0;
            _isPaused = true;
        }
    }

    public void Draw(Matrix view, Matrix projection)
    {
        if (_isPaused && (_currentTime == _totalTime || _currentTime == 0))
        {
            return;
        }
        float progress = _currentTime / _totalTime;
        if (progress < 0f)
        {
            progress = 0f;
        }
        else if (progress > 1f)
        {
            progress = 1f;
        }
        Vector3 position = Vector3.Lerp(_startPoint, _endPoint, progress);
        
        Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(_tiltAngle));
        Matrix world = rotation * Matrix.CreateTranslation(position);

        foreach (ModelMesh mesh in _mesh.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.World = world;
                effect.View = view;
                effect.Projection = projection;
                effect.EnableDefaultLighting();
            }
            mesh.Draw();
        }
    }
}