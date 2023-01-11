using UnityEngine;

namespace Asteroids
{
    public class AsteroidLifetime
    {
        public float m_lifetime;
        public float m_lifetimeCounter;
    
        public bool IsAsteroidAlive => AsteroidLifetimeLoop();

        private bool AsteroidLifetimeLoop()
        {
            m_lifetimeCounter += Time.deltaTime;
        
            return m_lifetimeCounter >= m_lifetime;
        }
    }
}