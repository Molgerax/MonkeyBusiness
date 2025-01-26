using System;
using MonkeyBusiness.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MonkeyBusiness.Gameplay.Components
{
    public class SpawnObject : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private GameObject prefab;
        [SerializeField] private Optional<Transform> parent;

        [SerializeField] private Vector2 timeInterval = new(8, 15);
        
        #endregion

        private GameObject _spawnedInstance;
        private float _timerCountdown;

        public void Update()
        {
            _timerCountdown -= Time.deltaTime;

            if (_timerCountdown < 0)
            {
                Spawn();
                _timerCountdown = Random.Range(timeInterval.x, timeInterval.y);
            }
        }

        public void Spawn()
        {
            _spawnedInstance = Instantiate(prefab, transform.position, transform.rotation, parent.Enabled ? parent : null);
            _spawnedInstance.SetActive(true);
        }
    }

}
