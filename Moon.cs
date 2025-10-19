using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace group_11_assignment5;

public class Moon
{

    private Model _mesh;
    private Vector3 _position;
    private float _rotationSpeed;
    private float _currentRotation;
    private Planet _planet;
    private float _orbitRadius;
    private float _orbitSpeed;

    public Moon(Model mesh, float orbitRadius, Planet planet, float rotationSpeed, float orbitSpeed)
    {
        _mesh = mesh;
        _position = planet.Position + new Vector3(orbitRadius, 0, 0);
        _planet = planet;
        _orbitRadius = orbitRadius;
        _rotationSpeed = rotationSpeed;
        _orbitSpeed = orbitSpeed;
        _currentRotation = 0f;
    }

    public void Update(GameTime gameTime)
    {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _currentRotation += _rotationSpeed * elapsedTime;
        _currentRotation %= MathHelper.TwoPi;
        Vector3 tposition = _planet.Position + new Vector3(
            _orbitRadius * (float)Math.Cos(_orbitSpeed * gameTime.TotalGameTime.TotalSeconds),
            0,
            _orbitRadius * (float)Math.Sin(_orbitSpeed * gameTime.TotalGameTime.TotalSeconds)
        );
        _position = Vector3.Lerp(_position, tposition, 0.1f);

    }

    public void Draw(Matrix view, Matrix projection)
    {
        Matrix world = Matrix.CreateScale(16f) *
            Matrix.CreateRotationY(_currentRotation) *
            Matrix.CreateTranslation(_position);

        foreach (var mesh in _mesh.Meshes)
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