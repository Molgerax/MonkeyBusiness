using MonkeyBusiness.Utility;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Components
{
    public class SpawnObject : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GameObject prefab;
        [SerializeField] private Optional<Transform> parent;
        
        #endregion

        private GameObject _spawnedInstance;
        
        public void Spawn()
        {
            _spawnedInstance = Instantiate(prefab, transform.position, transform.rotation, parent.Enabled ? parent : null);
            _spawnedInstance.SetActive(true);
        }
    }

}
