using System.Collections;
using Jake.Guards;
using Jake.Stages;
using Jake.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.System
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of GameManager
        /// </summary>
        public static GameManager Instance { get; private set; }
        
        // Runtime variables
        private Stage _currentStage;
        private int _orbsCollected;
        private int _numOfOrbs;

        [Header("Components")] 
        [SerializeField] private InGameUI _inGameUI;
        
        [Header("Debug! No touchy!")] 
        [SerializeField] private Stage _defaultStage;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private IEnumerator StartStageCoroutine(Stage stage, bool loadScene = true)
        {
            // Load the scene if necessary
            if(loadScene)
                yield return SceneManager.LoadSceneAsync("MainScene");
            
            // Load the stage
            _currentStage = stage;
            _currentStage.LoadStage();
            
            // Determine how many orbs there are
            _numOfOrbs = FindObjectsOfType<OrbScript>().Length;
            _orbsCollected = 0;
            
            // Initialize InGameUI
            _inGameUI.HideGameOverPopup();
            _inGameUI.SetOrbCount(0, _numOfOrbs);
        }
        
        /// <summary>
        /// Starts a match in a specified stage.
        /// </summary>
        /// <param name="stage">Stage to load</param>
        /// <param name="loadScene">Do we load MainScene before starting the stage?</param>
        public void StartStage(Stage stage, bool loadScene = true)
            => StartCoroutine(StartStageCoroutine(stage, loadScene));

        /// <summary>
        /// Increases the level of Awareness on all Guards in the scene.
        /// </summary>
        /// <param name="percent">The percentage increase of Awareness</param>
        public void IncreaseGuardAwareness(float percent)
        {
            // Loop through all guards in the scene
            foreach (var guard in FindObjectsOfType<GuardScript>())
            {
                // Increase Awareness
                guard.Awareness += percent;
            }
        }

        /// <summary>
        /// Increases the Orb collection counter and increases guard awareness by 10%.
        /// </summary>
        public void OrbCollected()
        {
            _orbsCollected++;
            _inGameUI.SetOrbCount(_orbsCollected, _numOfOrbs);
            
            // Did we win the game
            if(_orbsCollected >= _numOfOrbs) EndGame(true);
            else IncreaseGuardAwareness(10.0f);
        }

        public void EndGame(bool won)
        {
            string text = won ? "WON" : "LOST";
            _inGameUI.DisplayGameOverPopup(text);
            
            // Halt everything
            foreach (var haltable in FindObjectsOfType<HaltableBehavior>())
                haltable.SetHalted(true);
        }
        
        /// <summary>
        /// DEBUG -- Loads the default testing stage.
        /// </summary>
        public void DebugLoadDefaultStage()
            => StartStage(_defaultStage, false);
        
        /// <summary>
        /// Is there currently a stage loaded?
        /// </summary>
        public bool IsStageLoaded()
            => _currentStage != null;
    }
}