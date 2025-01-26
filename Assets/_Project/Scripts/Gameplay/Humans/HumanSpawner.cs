using System;
using MonkeyBusiness.Gameplay.Picking;
using UnityEngine;
using UnityEngine.Pool;

namespace MonkeyBusiness.Gameplay.Humans
{
    public class HumanSpawner : MonoBehaviour
    {
        [SerializeField] private HumanController humanPrefab;

        [SerializeField] private float spawnRange = 20f;
        [SerializeField] private int _maxCount = 3;

        [SerializeField] private float distanceBack = 5f;
        
        private IObjectPool<HumanController> _humanPool;


        private void Awake()
        {
            _humanPool = new ObjectPool<HumanController>(CreateHuman, OnGetFromPool, OnReleaseToPool,
                OnDestroyPooledObject, true, 32, 1024);
        }

        private Vector3 SpawnPosition(int index)
        {
            float steps = spawnRange / (_maxCount + 1);
            return transform.position + transform.right * ((index + 1) * steps - spawnRange * 0.5f);
        }

        public void Update()
        {
            
        }


        #region Pooling Functions
        
        private HumanController CreateHuman()
        {
            HumanController human = Instantiate(humanPrefab);
            human.ObjectPool = _humanPool;
            return human;
        }

        private void OnGetFromPool(HumanController pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
            pooledObject.Ingredient = IngredientList.Instance.GetRandom();
        }
        
        private void OnReleaseToPool(HumanController pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }
        
        private void OnDestroyPooledObject(HumanController pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        #endregion


        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < _maxCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(SpawnPosition(i), 2f);
            }
        }
    }
}
