using System;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Picking
{
    public class Ingredient : MonoBehaviour
    {
        public enum IngredientType
        {
            Beetle, Onion, Pear, Radish
        }

        public IngredientType Type = IngredientType.Beetle;
        
        [SerializeField] private float cookDuration = 10;
        [SerializeField] private UltEvent onCookBegin;
        [SerializeField] private UltEvent onCookInterrupt;
        [SerializeField] private UltEvent onCookDone;

        [SerializeField] private float buoyancy = 10;
        
        private float _cookTime = 0;

        public bool IsDone => _cookTime >= cookDuration;

        private Rigidbody _rb;

        public HotspringWater Water;
        public bool IsCooking => Water != null;
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CookTicking(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            if (!Water)
                return;

            float waterDist = Mathf.Max(0, Water.WaterLevel - transform.position.y);
            //waterDist = waterDist > 0 ? 1 : 0;
            waterDist = Mathf.Clamp01(waterDist);

            Vector3 force = new Vector3(0, waterDist * buoyancy, 0);
            _rb.AddForce(force);

            Vector3 counterForce = _rb.linearVelocity * -0.5f;
            _rb.AddForce(counterForce);
        }

        private void CookTicking(float deltaTime)
        {
            if (!IsCooking)
                return;

            if (IsDone)
                return;

            _cookTime += deltaTime;

            if (IsDone)
            {
                onCookDone?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out HotspringWater water))
                return;

            BeginCook(water);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out HotspringWater water))
                return;

            EndCook(water);
        }

        private void BeginCook(HotspringWater water)
        {
            Water = water;
            water.cookingIngredients.Add(this);
            
            if (!IsDone)
                onCookBegin?.Invoke();
        }

        private void EndCook(HotspringWater water)
        {
            water.cookingIngredients.Remove(this);
            Water = null;
            
            if (!IsDone)
                onCookInterrupt?.Invoke();
        }
    }
}
