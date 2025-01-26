using System;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using MonkeyBusiness.Gameplay.Picking;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace MonkeyBusiness.Gameplay.Humans
{
    public class HumanSpawner : MonoBehaviour
    {
        [SerializeField] private HumanController humanPrefab;
        [SerializeField] private Material[] humanMaterials;

        [SerializeField] private float spawnRange = 20f;
        [SerializeField] private int activeHumanCount = 3;
        [SerializeField] private int queuedHumanCount = 10;

        [SerializeField] private float humanRadius = 2;
        
        [SerializeField] private Transform queueStart;
        [SerializeField] private float queueMoveTime = 1;
        [SerializeField] private Transform leaveStart;
        [SerializeField] private float leaveDistance = 40;

        [SerializeField] private float timeToProgress = 20;

        [SerializeField] private Transform objectParent;
        public List<HumanController> activeHumans = new(8);
        public Queue<HumanController> queuedHumans = new(16);

        private IObjectPool<HumanController> _humanPool;

        private float _refillTimer = 0;
        private int _oldCount;

        private float _progressTimer = 0;

        public int ActiveHumanCount => activeHumanCount;
        
        private void Awake()
        {
            _humanPool = new ObjectPool<HumanController>(CreateHuman, OnGetFromPool, OnReleaseToPool,
                OnDestroyPooledObject, true, 32, 1024);
            
            FillListWithEmpty();
            InitQueue();
            _oldCount = activeHumanCount;
        }

        private void FillListWithEmpty()
        {
            while (activeHumans.Count < activeHumanCount)
            {
                activeHumans.Add(null);
            }
        }

        private void InitQueue()
        {
            while (queuedHumans.Count < queuedHumanCount)
            {
                queuedHumans.Enqueue(_humanPool.Get());
            }
            MoveQueuedHumans(0);
        }

        public void Update()
        {
            CheckForLeave();
            FillListWithEmpty();
            ResortActiveHumans();
            FillEmptySlots();
            
            _progressTimer += Time.deltaTime;
            if (_progressTimer > timeToProgress)
            {
                _progressTimer = 0;
                activeHumanCount++;

                StudioGlobalParameterTrigger trigger = GetComponent<StudioGlobalParameterTrigger>();
                if (trigger)
                {
                    trigger.Value = activeHumanCount;
                    trigger.TriggerParameters();
                }
            }
        }

        private Vector3 SpawnPosition(int index)
        {
            float steps = spawnRange / (activeHumanCount + 1);
            return transform.position + transform.right * ((index + 1) * steps - spawnRange * 0.5f);
        }

        private Vector3 QueuePosition(int index, float radius)
        {
            return queueStart.position - queueStart.forward * (index * radius);
        }

        private void FillEmptySlots()
        {
            if (_refillTimer > 0)
                _refillTimer -= Time.deltaTime;
            
            if (_refillTimer > 0)
                return;
            
            for (int i = 0; i < activeHumans.Count; i++)
            {
                HumanController slot = activeHumans[i];

                if (slot == null)
                {
                    slot = queuedHumans.Dequeue();
                    Vector3 targetPos = SpawnPosition(i);
                    slot.transform.DOMove(targetPos, 2);
                    slot.Activate();
                    _refillTimer = queueMoveTime;

                    activeHumans[i] = slot;
                    
                    HumanController newHuman = _humanPool.Get();
                    newHuman.transform.position = QueuePosition(queuedHumanCount, humanRadius);
                    queuedHumans.Enqueue(newHuman);
                    MoveQueuedHumans(queueMoveTime);
                    break;
                }
            }
        }

        private void ResortActiveHumans()
        {
            if (_oldCount == activeHumanCount) 
                return;

            _oldCount = activeHumanCount;
            
            for (int i = 0; i < activeHumans.Count; i++)
            {
                HumanController human = activeHumans[i];
                if (human == null)
                    continue;
                Vector3 targetPos = SpawnPosition(i);
                human.transform.DOMove(targetPos, queueMoveTime);
            }
        }
        
        private void MoveQueuedHumans(float duration = 1)
        {
            int i = 0;
            foreach (HumanController human in queuedHumans)
            {
                if (human == null)
                    continue;
                Vector3 targetPos = QueuePosition(i, humanRadius);
                human.transform.DOMove(targetPos, duration);
                i++;
            }
        }

        private void CheckForLeave()
        {
            for (int i = 0; i < activeHumans.Count; i++)
            {
                HumanController human = activeHumans[i];
                if (human == null)
                    continue;
                
                if (!human.wantsToLeave)
                    continue;

                activeHumans[i] = null;
                
                Vector3 targetPos = leaveStart.position;
                Vector3 finalPos = leaveStart.position + leaveStart.forward * leaveDistance;
                var sequence = DOTween.Sequence();
                sequence.Append(human.transform.DOMove(targetPos, 2));
                sequence.Append(human.transform.DOMove(finalPos, 5).OnComplete((()=>human.ObjectPool.Release(human))));
                sequence.Play();
            }
        }

        #region Pooling Functions
        
        private HumanController CreateHuman()
        {
            HumanController human = Instantiate(humanPrefab, objectParent);
            human.ObjectPool = _humanPool;
            return human;
        }

        private void OnGetFromPool(HumanController pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
            pooledObject.SetIngredient(IngredientList.Instance.GetRandom());
            pooledObject.SetMaterial(GetRandomMaterial());
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


        private Material GetRandomMaterial()
        {
            int index = Random.Range(0, humanMaterials.Length);
            return humanMaterials[index];
        }
        
        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < activeHumanCount; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(SpawnPosition(i), 2f);
            }
        }
    }
}
