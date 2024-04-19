using UnityEngine;

namespace Jake.Stages
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Jake/Stage", order = 0)]
    public class Stage : ScriptableObject
    {
        public string Name;
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// Spawns the stage in the current scene.
        /// </summary>
        public void LoadStage()
            => Instantiate(_prefab, Vector3.zero, Quaternion.identity);
    }
}