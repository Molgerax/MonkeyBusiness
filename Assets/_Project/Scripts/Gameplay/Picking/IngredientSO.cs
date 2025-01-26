using UnityEngine;

namespace MonkeyBusiness.Gameplay.Picking
{
    [CreateAssetMenu(menuName = "MonkeyBusiness/Ingredient", fileName = "NewIngredient")]
    public class IngredientSO : ScriptableObject
    {
        public Ingredient prefab;
        
        
    }
}
