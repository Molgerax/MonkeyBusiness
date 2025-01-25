using System.Collections.Generic;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Picking
{
    public class HotspringWater : MonoBehaviour
    {
        public static HotspringWater Instance;

        public List<Ingredient> cookingIngredients = new();

        public float WaterLevel => transform.position.y;
        
        private void Awake()
        {
            Instance = this;
        }

        public bool NearestIngredient(Vector3 pos, out Ingredient ingredient, out float distance)
        {
            ingredient = null;
            distance = 10000f;
            
            if (cookingIngredients.Count == 0)
                return false;

            foreach (Ingredient cookingIngredient in cookingIngredients)
            {
                float dist = Vector3.Distance(cookingIngredient.transform.position, pos);

                if (dist < distance)
                {
                    distance = dist;
                    ingredient = cookingIngredient;
                }
            }

            return true;
        }
    }
}
