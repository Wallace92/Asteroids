using UnityEngine;

namespace Asteroids
{
    public class AsteroidLifetime
    {
        public float Lifetime;
        public float LifetimeCounter;
    
        public bool IsAsteroidAlive => AsteroidLifetimeLoop();

        private bool AsteroidLifetimeLoop()
        {
            LifetimeCounter += Time.deltaTime;
        
            return LifetimeCounter >= Lifetime;
        }
    }
}