using Jake.Stages;
using Jake.System;
using UnityEngine;

namespace Jake.UI
{
    public class TitleScreenScript : MonoBehaviour
    {
        /// <summary>
        /// Title Screen button function to load a stage
        /// </summary>
        /// <param name="stage">Stage to load</param>
        public void LoadStage(Stage stage)
            => GameManager.Instance.StartStage(stage);
    }
}