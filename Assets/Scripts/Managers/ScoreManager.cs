using System.ComponentModel;
using Observer;
using TMPro;
using UI;

namespace Managers
{
    public class ScoreManager : View
    {
        public VictoryScreen VictoryScreen;
        public TMP_Text ScoreTMP;
        
        private GameManager m_gameManager;

        private int m_score;
        public int Score
        {
            get => m_score; 
            set => SetValue(value, ref m_score);
        }

        private void Awake() => m_gameManager = FindObjectOfType<GameManager>();

        private void Start()
        {
            PropertyChange += OnPropertyChange;
            VictoryScreen.RepeatBtn.onClick.AddListener(Restart);
        }

        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Score))
                return;

            if (Score == m_gameManager.GameEndScore)
            {
                SetVisibilityOfScoreTMP(false);
                ActivateVictoryScreen();
                SetGameLoop(true);
            }
            
            ScoreTMP.text = $"Punkty: [{Score}]";
        }

        private void Restart()
        {
            SetVisibilityOfScoreTMP(true);
            RestartScore();
            SetGameLoop(false);
        }

        private void SetVisibilityOfScoreTMP(bool visibility) =>  ScoreTMP.gameObject.SetActive(visibility);
        private void ActivateVictoryScreen() => VictoryScreen.Activate();
        private void SetGameLoop(bool isGameOver) =>  m_gameManager.GameOver = isGameOver;
        private void RestartScore() => Score = 0;
    }
}
