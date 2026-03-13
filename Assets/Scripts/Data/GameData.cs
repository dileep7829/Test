using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class GameData
    {
        [JsonProperty("ButtonsData")] public List<ButtonData> ButtonsData  = new List<ButtonData>();
        [JsonProperty("TotalButtonsCount")] public int TotalButtonsCount;
        [JsonProperty("Score")] public  int Score;
        [JsonProperty("TurnCount")] public  int TurnCount;
        [JsonProperty("MatchCount")] public  int MatchCount;
        [JsonProperty("MatchSteak")] public  int MatchSteak = 0;
        [JsonProperty("UnMatchSteak")] public  int UnMatchSteak = 0;
        
        private static GameData _instance = null;
       
        private GameData()
        {
        }
    
        public static GameData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameData();
                }
                return _instance;
            }
            set => _instance = value;
        }
    }
}
