using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asteroids;
using UnityEngine;

namespace Managers
{
    public class AsteroidManager : Spawner<Asteroid>
    {
        
        [Header("Asteroid Manager")]
        [Range(2, 8)] 
        public int InitAsterNum;

        private int m_maxAsterNum = 10;
        private int m_minAsterNum = 5;
        
        private GameManager m_gameManager;
        private List<Asteroid> m_asteroids = new List<Asteroid>();

        private int m_activeAsteroidsNumber => m_asteroids
            .Count(aster => aster.isActiveAndEnabled);

        private Asteroid m_firstDisabledAsteroid => m_asteroids
            .FirstOrDefault(aster => !aster.isActiveAndEnabled);

        private void Awake() => m_gameManager = FindObjectOfType<GameManager>();

        private void Start() => SpawnAsteroidsOnStart();

        private void SpawnAsteroidsOnStart()
        {
            for (int i = 0; i < m_maxAsterNum; i++)
            {
                var aster = Spawn(SpawnedPrefab.gameObject);
                aster.PropertyChange += OnPropertyChange;
                
                m_asteroids.Add(aster);
                
                if (i > m_minAsterNum - 1)
                    aster.gameObject.SetActive(false);
            }
        }
    
        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender is not Asteroid asteroid)
                return;
            
            switch (e.PropertyName)
            {
                case nameof(asteroid.Destroyed):
                    m_gameManager.Score++;
                    break;
                case nameof(asteroid.Disabled):
                {
                    if (!asteroid.Disabled)
                        return;
                    
                    if (m_activeAsteroidsNumber < m_minAsterNum)
                        EnableAsteroids();
                    break;
                }
            }
        }
        
        private void EnableAsteroids()
        {
            for (int i = 0; i < m_maxAsterNum - m_activeAsteroidsNumber; i++)
            {
                m_firstDisabledAsteroid.gameObject.SetActive(transform);
            }
        }
    }
}
