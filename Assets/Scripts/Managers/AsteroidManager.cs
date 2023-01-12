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

        private void Start() => m_asteroids = SpawnAsteroids(SpawnedPrefab.gameObject, m_maxAsterNum, InitAsterNum);

        private List<Asteroid> SpawnAsteroids(GameObject spawnedPrefabGameObject, int maxAsterNum, int initAsterNum)
        {
            var asteroids = new List<Asteroid>();
            
            for (int i = 0; i < maxAsterNum; i++)
            {
                var aster = Spawn(spawnedPrefabGameObject, "aster_" + i);
                aster.PropertyChange += OnPropertyChange;
                
                asteroids.Add(aster);
                
                if (i > initAsterNum - 1)
                    aster.gameObject.SetActive(false);
            }

            return asteroids;
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
                DestroyAsteroids(m_asteroids);
            else
                m_asteroids = SpawnAsteroids(SpawnedPrefab.gameObject, m_maxAsterNum, InitAsterNum);;
        }

        private void DestroyAsteroids(List<Asteroid> asteroids)
        {
            foreach (var asteroid in asteroids)
            {
                asteroid.PropertyChange -= OnPropertyChange;
                Destroy(asteroid.gameObject);
            }

            asteroids.Clear();
        }
    }
}
