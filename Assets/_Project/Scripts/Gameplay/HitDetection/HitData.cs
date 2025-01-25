using UnityEngine;

namespace MonkeyBusiness.Gameplay.HitDetection
{
    public struct HitData
    {
        public int Damage;
        public Vector3 HitPoint;
        public Vector3 HitNormal;

        public IHurtbox Hurtbox;
        public IHitbox Hitbox;

        public bool Validate()
        {
            if (Hurtbox == null) return false;
            if (Hurtbox.CheckHit(this) == false) return false;
            if (Hurtbox.HurtResponder != null && Hurtbox.HurtResponder.CheckHit(this) == false) return false;
            if (Hitbox.HitResponder == null) return true;
            
            return Hitbox.HitResponder.CheckHit(this);
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
