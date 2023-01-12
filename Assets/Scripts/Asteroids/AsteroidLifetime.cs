using UnityEngine;

namespace Asteroids
{
    public class AsteroidLifetime
    {
        private float m_lifetime;
        private float m_lifetimeCounter;
    
        public bool IsAsteroidAlive => AsteroidLifetimeLoop();

        private bool AsteroidLifetimeLoop()
        {
            m_lifetimeCounter += Time.deltaTime;
        
            return m_lifetimeCounter >= m_lifetime;
        }

        public void SetAsteroidLifetime(float lifetime) => m_lifetime = lifetime;
        
        public void ResetAsteroidLifetimeCounter() => m_lifetimeCounter = 0;
    }
}