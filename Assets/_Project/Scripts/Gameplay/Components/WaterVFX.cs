using System;
using MonkeyBusiness.Gameplay.Picking;
using UltEvents;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Components
{
    public class WaterVFX : MonoBehaviour
    {
        [SerializeField] private GameObject waterVFX;
        [SerializeField] private UltEvent onEnterWater;
        [SerializeField] private UltEvent onExitWater;

        private bool _inWater = false;

        private void Update()
        {
            Vector3 pos = waterVFX.transform.position;
            pos.y = HotspringWater.Instance.WaterLevel;
            waterVFX.transform.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HotspringWater water))
            {
                _inWater = true;
                onEnterWater?.Invoke();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out HotspringWater water))
            {
                _inWater = false;
                onExitWater?.Invoke();
            }
        }
    }
}
