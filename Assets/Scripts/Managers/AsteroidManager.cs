using System.ComponentModel;
using Asteroids;

namespace Managers
{
    public class AsteroidManager : Spawner<Asteroid>
    {
        private GameManager m_gameManager;

        private void Awake() => m_gameManager = FindObjectOfType<GameManager>();

        private void Start()
        {
            var aster1 = Spawn(SpawnedPrefab.gameObject);
            aster1.PropertyChange += OnPropertyChange;
            var aster2 =Spawn(SpawnedPrefab.gameObject);
            aster2.PropertyChange += OnPropertyChange;
        }
    
        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Destroyed")
                m_gameManager.Score++;
        }
    }
}
