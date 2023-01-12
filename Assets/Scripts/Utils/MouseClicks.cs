using UnityEngine;

namespace Utils
{
    public class MouseClicks
    {
        private const float m_doubleClickTime = 0.25f;
        private float m_lastClickTime;

        public bool DoubleMouseClicked => DoubleMouseClick();

        private bool DoubleMouseClick()
        {
            float timeSinceLastClick = Time.time - m_lastClickTime;

            if (timeSinceLastClick <= m_doubleClickTime)
                return true;
        
            m_lastClickTime = Time.time;
        
            return false;
        }
    }
}