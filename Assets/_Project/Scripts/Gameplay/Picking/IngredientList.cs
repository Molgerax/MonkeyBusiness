using UnityEngine;
using Random = UnityEngine.Random;

namespace MonkeyBusiness.Gameplay.Picking
{
    public class IngredientList : MonoBehaviour
    {
        public static IngredientList Instance;
        
        [SerializeField] public IngredientSO[] Ingredients;

        public IngredientSO GetRandom()
        {
            int index = Random.Range(0, Ingredients.Length);
            return Ingredients[index];
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
