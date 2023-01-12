using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryScreen : MonoBehaviour
    {
        public Button RepeatBtn;

        public void Start()
        {
            gameObject.SetActive(false);
            RepeatBtn.onClick.AddListener(Deactivate);
        }

        public void Activate() => gameObject.SetActive(true);
        private void Deactivate() => gameObject.SetActive(false);
    }
}