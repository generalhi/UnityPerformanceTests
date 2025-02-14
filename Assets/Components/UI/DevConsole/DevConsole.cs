using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.UI.DevConsole
{
    public class DevConsole : MonoBehaviour
    {
        private TextMeshProUGUI _textContent;
        private Scrollbar _scrollbar;
        private int _updateCount;
        private static DevConsole _instance;

        private void Awake()
        {
            _instance = this;
            _textContent = GetComponentInChildren<TextMeshProUGUI>();
            _scrollbar = GetComponentInChildren<Scrollbar>();
        }

        private void LateUpdate()
        {
            if (_updateCount > 0 && 
                _scrollbar.value > 0)
            {
                _scrollbar.value = 0;
                _updateCount--;
            }
        }

        public static void WriteLine(string text)
        {
            _instance._textContent.text += text + "\n";
            _instance._updateCount = 2;
        }
    }
}
