using Observer;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class Asteroid : View
    {
        public AsteroidScriptableObject AsteroidScriptableObject;
        
        private bool m_destroyed;
        public bool Destroyed
        {
            get => m_destroyed; 
            set => SetValue(value, ref m_destroyed);
        }
        
        private bool m_enabled;
        private bool Enabled
        {
            get => m_enabled; 
            set => SetValue(value, ref m_enabled);
        }
        
        private MouseClicks m_mouseClicks = new MouseClicks();
        private AsteroidLifetime m_asteroidLifetime = new AsteroidLifetime();
        private Renderer m_renderer;

        private int m_healthPoints;

        private void Awake() => m_renderer = GetComponent<Renderer>();
        private void OnEnable() => ResetAsteroidValues();
        private void Start() => SetInitialAsteroidValues(AsteroidScriptableObject);
        
        private void ResetAsteroidValues()
        {
            SetInitialAsteroidValues(AsteroidScriptableObject);
            ResetAsteroidProperties();
            SetColor();
        }

        private void SetInitialAsteroidValues(AsteroidScriptableObject asterScripObj)
        {
            m_asteroidLifetime.SetAsteroidLifetime(Random.Range(asterScripObj.LifetimeMin, asterScripObj.LifetimeMax));
            m_healthPoints = asterScripObj.HealthPoints;
        }

        private void ResetAsteroidProperties()
        {
            Enabled = true;
            Destroyed = false;
        }

        private void SetColor() => m_renderer.material.color = AsteroidHelper.SetColor(m_healthPoints);

        private void Update()
        {
            if (!m_asteroidLifetime.IsAsteroidAlive) 
                return;
            
            Enabled = false;
            gameObject.SetActive(false);
        }

        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) 
                return;

            if (!m_mouseClicks.DoubleMouseClicked) 
                return;
        
            m_healthPoints--;

            if (m_healthPoints == 0)
            {
                Destroyed = true;
                gameObject.SetActive(false);
                return;
            }

            SetColor();
        }

        private void OnDisable() => m_asteroidLifetime.ResetAsteroidLifetimeCounter();
    }
}