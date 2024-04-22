using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.UI
{
    public class InGameUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TMP_Text _orbCountDisplay;
        [SerializeField] private GameObject _gameOverPopup;
        [SerializeField] private TMP_Text _gameOverLabel;
        
        // Format constants
        private const string ORB_COUNTER_FORMAT = "{0}/{1}";
        private const string GAME_OVER_POPUP_FORMAT = "YOU {0}!";
        
        public void SetOrbCount(int count, int max)
            => _orbCountDisplay.text = String.Format(ORB_COUNTER_FORMAT, count, max);

        public void DisplayGameOverPopup(string text)
        {
            _gameOverPopup.SetActive(true);
            _gameOverLabel.text = String.Format(GAME_OVER_POPUP_FORMAT, text);
        }

        public void HideGameOverPopup()
            => _gameOverPopup.gameObject.SetActive(false);

        public void BackToMainMenu()
            => SceneManager.LoadScene("TitleScene");
    }
}