using UnityEngine;

namespace MonkeyBusiness.Gameplay.Animations
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public Animator Animator => animator;
    }
}
