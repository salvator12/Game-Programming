using System.Collections.Generic;
using UnityEngine;

namespace CookDash.Data
{
    [CreateAssetMenu(fileName = "OrderData", menuName = "OrderData")]
    public class OrderData : ScriptableObject
    {
        public string orderName;
        public Sprite Icon;
        public float cookTime;
        public List<IngredientsData> ingredients;
        public Color color;
    }
}