using MonkeyBusiness.Gameplay.HitDetection;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Picking
{
    public class PickupHolder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private Hitbox hitbox;
        
        
        public Pickup CurrentPickup = null;
        public bool HasPickup => CurrentPickup != null;


        public void BeginPicking()
        {
            hitbox.HitResponder = this;
            hitbox.StartCollisionCheck();
        }

        public void EndPicking()
        {
            hitbox.HitResponder = null;
            hitbox.StopCollisionCheck();
        }

        public void DropPickup()
        {
            if (!CurrentPickup)
                return;
            
            CurrentPickup.EndHold();
            CurrentPickup = null;
        }

        public int Damage => 0;
        public bool CheckHit(HitData data)
        {
            if (CurrentPickup)
                return false;
            
            return data.Hurtbox.GameObject.TryGetComponent(out Pickup pickup);
        }

        public void Response(HitData data)
        {
            if (!data.Hurtbox.GameObject.TryGetComponent(out Pickup pickup))
                return;

            CurrentPickup = pickup;
            pickup.BeginHold(this);
        }
    }
}
