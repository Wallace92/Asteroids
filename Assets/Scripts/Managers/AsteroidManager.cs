using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Asteroids;
using UnityEngine;
using Utils;

namespace Managers
{
    public class AsteroidManager : Spawner<Asteroid>
    {
        [Header("Asteroid Manager")]
        [Range(2, 8)] 
        public int InitAsterNum;
        
        [SerializeField] 
        private int m_maxAsterNum = 10;
        [SerializeField] 
        private int m_minAsterNum = 5;
        
        private List<Asteroid> m_asteroids;
        private ScoreManager m_scoreManager;
        private GameManager m_gameManager;

        private Asteroid FirstDisabledAsteroid => m_asteroids
            .FirstOrDefault(aster => !aster.gameObject.activeInHierarchy);

        private int ActiveAsteroidsNumber => m_asteroids
            .Count(aster => aster.gameObject.activeInHierarchy);

        private bool SpawnAsteroidsCondition => ActiveAsteroidsNumber <= m_minAsterNum;
        private int SpawnAsteroidNum => m_maxAsterNum - ActiveAsteroidsNumber;
        
        private void Awake()
        {
            m_scoreManager = FindObjectOfType<ScoreManager>();
            m_gameManager = FindObjectOfType<GameManager>();

            m_gameManager.PropertyChange += OnPropertyChange;
        }

        private void Start() => SpawnAsteroids();

        private void SpawnAsteroids()
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

        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            switch (sender)
            {
                case GameManager gameManager:
                    
                    OnGameManagerChanged(e, gameManager);
                    break;
                
                case Asteroid asteroid:
                    
                    OnAsteroidChanged(e, asteroid);
                    break;
            }
        }

        private void OnAsteroidChanged(PropertyChangedEventArgs e, Asteroid asteroid)
        {
            if (e.PropertyName == nameof(asteroid.Destroyed) && asteroid.Destroyed) 
                m_scoreManager.Score++;

            if (!SpawnAsteroidsCondition)
                return;
            
            EnableAsteroids(Random.Range(1, SpawnAsteroidNum));
        }

        private void EnableAsteroids(int spawnAsteroidNum)
        {
            for (int i = 0; i < spawnAsteroidNum; i++)
            {
                var firstDisabledAsteroid = FirstDisabledAsteroid;
                if (firstDisabledAsteroid == null)
                    continue;

                firstDisabledAsteroid.transform.position = SetRandomPosition();
                firstDisabledAsteroid.gameObject.SetActive(true);
            }
        }

        private void OnGameManagerChanged(PropertyChangedEventArgs e, GameManager gameManager)
        {
            if (e.PropertyName != nameof(gameManager.GameOver))
                return;

            if (gameManager.GameOver)
                DestroyAsteroids();
            else
                SpawnAsteroids();
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
    }
}
