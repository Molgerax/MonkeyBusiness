using UnityEngine;

namespace MonkeyBusiness.Gameplay.HitDetection
{
    public struct HitData
    {
        public int damage;
        public Vector3 hitPoint;
        public Vector3 hitNormal;

        public IHurtbox hurtbox;
        public IHitbox hitbox;

        public bool Validate()
        {
            if (hurtbox == null) return false;
            if (hurtbox.CheckHit(this) == false) return false;
            if (hurtbox.HurtResponder != null && hurtbox.HurtResponder.CheckHit(this) == false) return false;
            if (hitbox.HitResponder == null) return true;
            
            return hitbox.HitResponder.CheckHit(this);
        }
    }



    public interface IHitbox
    {
        public IHitResponder HitResponder { get; set; }
        public bool CheckHit();
        
        public GameObject GameObject { get; }
    }

    public interface IHurtbox
    {
        public bool Active { get;}
        public GameObject GameObject { get; }
        public Transform Transform { get; }
        
        public IHurtResponder HurtResponder { get; set; }
        public bool CheckHit(HitData data);
    }
}
