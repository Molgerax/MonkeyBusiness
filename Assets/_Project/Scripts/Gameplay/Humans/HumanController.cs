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

        public IObjectPool<HumanController> ObjectPool;

        public void ReceiveIngredient(Ingredient ingredient)
        {
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
        }

        private void FeedFailure(Ingredient ingredient)
        {
            onFailure?.Invoke();
        }
    }
}
