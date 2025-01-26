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

        [SerializeField] private UltEvent onSuccess;
        [SerializeField] private UltEvent onFailure;
        [SerializeField] private UltEvent onFailureNotDone;

        public IObjectPool<HumanController> ObjectPool;

        public bool WantsToLeave = false;

        private void OnEnable()
        {
            WantsToLeave = false;
        }

        private void OnDisable()
        {
            WantsToLeave = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (WantsToLeave)
                return;

            if(other.TryGetComponent(out Ingredient ingredient))
                ReceiveIngredient(ingredient);
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
        }


        private void FeedSuccess(Ingredient ingredient)
        {
            onSuccess?.Invoke();
            WantsToLeave = true;
        }

        private void FeedFailure(Ingredient ingredient)
        {
            onFailure?.Invoke();
            WantsToLeave = true;
        }
        
        private void NotDoneFailure(Ingredient ingredient)
        {
            onFailureNotDone?.Invoke();
            WantsToLeave = true;
        }
    }
}
