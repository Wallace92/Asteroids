using System.ComponentModel;
using TMPro;

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

        private void Restart()
        {
            ScoreTMP.gameObject.SetActive(true);
            Score = 0;
            m_gameManager.GameOver = false;
        }

        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Score))
                return;

            if (m_score == m_gameManager.GameEndScore)
            {
                ScoreTMP.gameObject.SetActive(false);
                VictoryScreen.Activate();
                m_gameManager.GameOver = true;
            }
            
            ScoreTMP.text = $"Punkty: [{m_score}]";
        }
    }
}
