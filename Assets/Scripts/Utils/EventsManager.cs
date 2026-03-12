using System;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Singleton Event Manager Class for ease of Use
    /// </summary>
    public class EventsManager : MonoBehaviour
    {
        public static EventsManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public EventHandler OnButtonClicked;
        public EventHandler OnItemHideStart;
        public EventHandler OnItemRemoveStart;
        public EventHandler OnItemRemoved;
        public EventHandler OnItemMatched;
        public EventHandler OnItemUnMatched;
        public EventHandler OnGameFinished;
        public EventHandler OnLoadingGameFromData;
    }
}
