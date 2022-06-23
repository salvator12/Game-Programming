using CookDash.Model;
using UnityEngine;

namespace CookDash.Data
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "IngredientData")]
    public class IngredientsData : ScriptableObject
    {
        public IngredientsType type;
        public Sprite Icon;
    }
}