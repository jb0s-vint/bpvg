using System;
using Jake.System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.UI
{
    public class InGameUI : MonoBehaviour
    {
        [Header("~ Components")]
        [SerializeField] private TMP_Text _orbCountDisplay;
        [SerializeField] private GameObject _gameOverPopup;
        [SerializeField] private TMP_Text _gameOverLabel;
        
        // Format constants
        private const string ORB_COUNTER_FORMAT = "{0}/{1}";
        private const string GAME_OVER_POPUP_FORMAT = "YOU {0}!";
        
        /// <summary>
        /// Set the value of the on-screen orb counter.
        /// </summary>
        /// <param name="count">The collected amount</param>
        /// <param name="max">The total amount</param>
        public void SetOrbCount(int count, int max)
            => _orbCountDisplay.text = String.Format(ORB_COUNTER_FORMAT, count, max);

        /// <summary>
        /// Displays the game over screen.
        /// </summary>
        /// <param name="text">The win or loss condition to display.</param>
        public void DisplayGameOverPopup(string text)
        {
            _gameOverPopup.SetActive(true);
            _gameOverLabel.text = String.Format(GAME_OVER_POPUP_FORMAT, text);
        }

        /// <summary>
        /// Hides the game over screen.
        /// </summary>
        public void HideGameOverPopup()
            => _gameOverPopup.gameObject.SetActive(false);

        /// <summary>
        /// Returns to the main menu.
        /// </summary>
        public void BackToMainMenu()
        {
            GameManager.Instance.MusicManager.Stop();
            SceneManager.LoadScene("TitleScene");
        }
    }
}