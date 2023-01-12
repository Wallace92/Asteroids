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
        
        private List<Asteroid> m_asteroids;
        private ScoreManager m_scoreManager;
        private GameManager m_gameManager;

        private int m_maxAsterNum = 10;
        private int m_minAsterNum = 5;

        private int m_activeAsteroidsNumber => m_asteroids
            .Count(aster => aster.gameObject.activeInHierarchy);

        private Asteroid m_firstDisabledAsteroid
        {
            get
            {
                return m_asteroids
                    .FirstOrDefault(aster => !aster.gameObject.activeInHierarchy);
            }
        }

        private void Awake()
        {
            m_scoreManager = FindObjectOfType<ScoreManager>();
            m_gameManager = FindObjectOfType<GameManager>();

            m_gameManager.PropertyChange += OnPropertyChange;
        }

        private void Start() => SpawnAsteroidsOnStart();

        private void SpawnAsteroidsOnStart()
        {
            m_asteroids = new List<Asteroid>();
            
            for (int i = 0; i < m_maxAsterNum; i++)
            {
                var aster = Spawn(SpawnedPrefab.gameObject, "aster_" + i);
                aster.PropertyChange += OnPropertyChange;
                
                m_asteroids.Add(aster);
                
                if (i > InitAsterNum - 1)
                    aster.gameObject.SetActive(false);
            }
        }

        private void DestroyAsteroids()
        {
            foreach (var asteroid in m_asteroids)
            {
                asteroid.PropertyChange -= OnPropertyChange;
                Destroy(asteroid.gameObject);
            }

            m_asteroids.Clear();
        }
    
        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (sender is GameManager gameManager)
            {
                OnGameManagerPropertyChanged(e, gameManager);
                return;
            }
            
            if (sender is not Asteroid asteroid)
                return;
            
            switch (e.PropertyName)
            {
                case nameof(asteroid.Destroyed):
                    if (asteroid.Destroyed)
                        m_scoreManager.Score++;

                    if (m_activeAsteroidsNumber <= m_minAsterNum)
                        EnableAsteroids();
                    
                    break;
                case nameof(asteroid.Enabled):
                {
                    if (asteroid.Enabled)
                        return;
                    
                    if (m_activeAsteroidsNumber <= m_minAsterNum)
                        EnableAsteroids();
                    
                    break;
                }
            }
        }

        private void OnGameManagerPropertyChanged(PropertyChangedEventArgs e, GameManager gameManager)
        {
            if (e.PropertyName != nameof(gameManager.GameOver))
                return;

            if (gameManager.GameOver)
                DestroyAsteroids();
            else
                SpawnAsteroidsOnStart();

            return;
        }

        private void EnableAsteroids()
        {
            for (int i = 0; i < m_maxAsterNum - m_activeAsteroidsNumber; i++)
            {
                
                var firstDisabledAsteroid = m_firstDisabledAsteroid;
                
                if (m_firstDisabledAsteroid == null)
                    continue;
                
                var pos = SetRandomPosition();
                firstDisabledAsteroid.transform.position = pos;
                firstDisabledAsteroid.gameObject.SetActive(true);
            }
        }
    }
}
