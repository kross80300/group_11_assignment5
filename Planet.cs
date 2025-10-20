using System;
using System.Collections.Generic;
using group_11_assignment5;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace project5
{
    public class Planet
    {
        private Model _mesh;
        private Vector3 _center;
        private Vector3 _position;
        private float _orbitRadius;
        private float _orbitSpeed;
        private float _rotationSpeed;
        private float _currentRotation;
        private List<Moon> _moons;
        public Planet(Model mesh, Vector3 center, float orbitRadius, float rotationSpeed, float orbitSpeed)
        {
            _mesh = mesh;
            _center = center;
            _orbitRadius = orbitRadius;
            _rotationSpeed = rotationSpeed;
            _orbitSpeed = orbitSpeed;
            _position = center + new Vector3(orbitRadius, 0, 0);
            _currentRotation = 0f;
            _moons = new List<Moon>();
        }
        public Vector3 GetPosition()
        {
            return _position;
        }
        public void AddMoon(Moon moon)
        {
            if (moon != null)
                _moons.Add(moon);
        }
        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float totalTime = (float)gameTime.TotalGameTime.TotalSeconds;
            _currentRotation += _rotationSpeed * elapsedTime;
            _currentRotation %= MathHelper.TwoPi;

            float orbitPos = _orbitSpeed * totalTime;
            Vector3 idealPos = _center + new Vector3(_orbitRadius * (float)Math.Cos(orbitPos), 0f,
            _orbitRadius * (float)Math.Sin(orbitPos)
            );
            _position = Vector3.Lerp(_position, idealPos, 0.1f);

            foreach (var moon in _moons)
                moon.Update(gameTime);
        }
        public void Draw(Matrix view, Matrix projection)
        {
            Matrix world = Matrix.CreateRotationY(_currentRotation) *
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
            foreach (var moon in _moons)
                moon.Draw(view, projection);
        }
    }
}