using System.Collections.Generic;
using UnityEngine;

namespace CookDash.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "LevelData")]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public List<OrderData> orders;
        
        public int durationTime;
        
        public int star1Score;
        public int star2Score;
        public int star3Score;
    }
}