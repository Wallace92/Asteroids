using System.ComponentModel;
using TMPro;

namespace Managers
{
    public class GameManager : View
    {
        public TMP_Text ScoreTMP;
    
        private int m_score;
        public int Score
        {
            get => m_score; 
            set => SetValue(value, ref m_score);
        }

        private void Start()
        {
            PropertyChange += OnPropertyChange;
        }

        private void OnPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Score))
                ScoreTMP.text = $"Punkty: [{m_score}]";
        }
    }
}
