using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class ButtonData
    {
        [JsonProperty("Id")] public int Id;
        [JsonProperty("IsVisible")] public bool IsVisible;
        [JsonProperty("SpriteName")] public string SpriteName;

        public ButtonData(int id, bool isVisible, string spriteName)
        {
            Id = id;
            IsVisible = isVisible;
            SpriteName = spriteName;
        }
    }
}
