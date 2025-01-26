using System;
using MonkeyBusiness.Gameplay.Picking;
using UltEvents;
using UnityEngine;
using UnityEngine.Pool;

namespace MonkeyBusiness.Gameplay.Humans
{
    public class HumanController : MonoBehaviour
    {
        [SerializeField] public IngredientSO Ingredient;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private UltEvent onSuccess;
        [SerializeField] private UltEvent onFailure;
        [SerializeField] private UltEvent onFailureNotDone;

        [SerializeField] private GameObject thoughtBubble;
        [SerializeField] private MeshRenderer iconRenderer;
        
        public IObjectPool<HumanController> ObjectPool;

        public bool wantsToLeave = false;
        public bool isActive = false;
        
        private void OnEnable()
        {
            wantsToLeave = false;
            isActive = false;
            thoughtBubble.SetActive(false);
        }

        private void OnDisable()
        {
            wantsToLeave = false;
            isActive = false;
            thoughtBubble.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (wantsToLeave)
                return;

            if (!isActive)
                return;
            
            if(other.TryGetComponent(out Ingredient ingredient))
                ReceiveIngredient(ingredient);
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.sharedMaterial = material;
        }

        public void SetIngredient(IngredientSO ingredient)
        {
            Ingredient = ingredient;
            iconRenderer.sharedMaterial = ingredient.iconMaterial;
        }

        public void ReceiveIngredient(Ingredient ingredient)
        {
            if (!ingredient.IsDone)
            {
                NotDoneFailure(ingredient);
                return;
            }
            
            
            if (ingredient.Asset == Ingredient)
            {
                FeedSuccess(ingredient);
            }
            else
            {
                FeedFailure(ingredient);
            }

            isActive = false;
            thoughtBubble.SetActive(false);
        }


        private void FeedSuccess(Ingredient ingredient)
        {
            onSuccess?.Invoke();
            wantsToLeave = true;
            FMODUnity.RuntimeManager.PlayOneShot("A_SFX_SoupFinish");
            FMODUnity.RuntimeManager.PlayOneShot("A_MNKY_HappyMonkey");
        }

        public void Activate()
        {
            isActive = true;
            thoughtBubble.SetActive(true);
        }
        
        private void FeedFailure(Ingredient ingredient)
        {
            onFailure?.Invoke();
            wantsToLeave = true;
            FMODUnity.RuntimeManager.PlayOneShot("A_SFX_SoupFinish");
            FMODUnity.RuntimeManager.PlayOneShot("A_MNKY_MonkeyHit");

        }
        
        private void NotDoneFailure(Ingredient ingredient)
        {
            onFailureNotDone?.Invoke();
            wantsToLeave = true;
        }
    }
}
