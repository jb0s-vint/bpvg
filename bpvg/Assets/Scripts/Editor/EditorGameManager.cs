#if UNITY_EDITOR
using Jake.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jake.Editor
{
    public class EditorGameManager
    {
        [RuntimeInitializeOnLoadMethod]
        public static void SpawnGameManager()
        {
            // Spawn GameManager instance
            if (GameManager.Instance == null)
            {
                var gm = Resources.Load<GameManager>("GameManager");
                Object.Instantiate(gm, Vector3.zero, Quaternion.identity);
            }

            // Are we in the MainScene with no stage loaded?
            // This is only the case when entering playmode from UnityEditor.
            if (SceneManager.GetActiveScene().name == "MainScene" && !GameManager.Instance.IsStageLoaded())
                GameManager.Instance.DebugLoadDefaultStage();
        }
    }
}
#endif