using UnityEngine;

namespace Managers
{
    public class GameManager : View
    {
        [SerializeField] public int GameEndScore;

        private bool m_gameOver;
        public bool GameOver
        {
            get => m_gameOver; 
            set => SetValue(value, ref m_gameOver);
        }
    }
}