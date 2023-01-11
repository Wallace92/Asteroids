using System.ComponentModel;
using UnityEngine;
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
        
        private bool m_disabled;
        public bool Disabled
        {
            get => m_disabled; 
            set => SetValue(value, ref m_disabled);
        }
        
        private MouseClicks m_mouseClicks = new MouseClicks();
        private Renderer m_renderer;

        private float m_lifetime;
        private float m_lifetimeCounter;

        private int m_healthPoints;

        private void Awake()
        {
            m_renderer = GetComponent<Renderer>();
            SetInitialAsteroidValues();
        }
    
        private void OnEnable() => SetInitialAsteroidValues();

        private void SetInitialAsteroidValues()
        {
            m_lifetime = Random.Range(AsteroidScriptableObject.LifetimeMin, AsteroidScriptableObject.LifetimeMax);
            m_healthPoints = AsteroidScriptableObject.HealthPoints;
            
            Disabled = false;
            Destroyed = false;
            SetColor();
        }

        private void Update()
        {
            m_lifetimeCounter += Time.deltaTime;
        
            if (m_lifetimeCounter >= m_lifetime)
                DeactivateAsteroid();
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
                DeactivateAsteroid();
                return;
            }

            SetColor();
        }
    
        private void SetColor() => m_renderer.material.color = AsteroidHelper.SetColor(m_healthPoints);
        
        private void  DeactivateAsteroid()
        {
            gameObject.SetActive(false);
            Disabled = true;
            m_lifetimeCounter = 0;
        }

        public void ActivateAsteroid(Vector3 pos)
        {
            transform.position = pos;
            gameObject.SetActive(true);
        }
    }
}