using System.Collections;
using Jake.Stages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.System
{
    public class GameManager : MonoBehaviour
    {
        // Runtime variables
        private Stage _currentStage;
        
        [Header("Debug! No touchy!")] 
        [SerializeField] private Stage _defaultStage;
     
        /// <summary>
        /// Singleton instance of GameManager
        /// </summary>
        public static GameManager Instance { get; private set; }

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
            
            // Spawn guards
            
            // Spawn orbs
        }
        
        /// <summary>
        /// Starts a match in a specified stage.
        /// </summary>
        /// <param name="stage">Stage to load</param>
        /// <param name="loadScene">Do we load MainScene before starting the stage?</param>
        public void StartStage(Stage stage, bool loadScene = true)
            => StartCoroutine(StartStageCoroutine(stage, loadScene));
        
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