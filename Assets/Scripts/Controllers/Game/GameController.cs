using System;
using System.Collections.Generic;
using Data;
using ScriptableObjects;
using UnityEngine;
using Utils;
using Views.Game;

namespace Controllers.Game
{
    [SelectionBase]
    public class GameController : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanel;
        [SerializeField] private PuzzleButton puzzleButton;
        [SerializeField] private  SpriteHolder spriteHolder;
        [SerializeField] private  GameObject gameOverPopup;
        
        private List<Sprite> _puzzleSprites = new List<Sprite>();
        private PuzzleButton _button1;
        private int _totalButtonsCount;
        private bool _isGameFinished;
        
        void Start()
        {
            GameData gameData = StorageHandler.Instance.GetGameData();
            if(PlayerPrefs.GetInt("IsGameRunning")==1 && gameData!=null)
            {
                LoadGameFromData(gameData);
            }
            else
            {
                _totalButtonsCount = GlobalData.rowCount * GlobalData.columnCount;
                GlobalData.targetCount = _totalButtonsCount / 2;

                Randomizer.RandomizeArray(ref spriteHolder.Sprites);
                CreatePuzzleButtons();
                
                GameData.Instance.TotalButtonsCount = _totalButtonsCount;
            }
            _isGameFinished = false;
            PlayerPrefs.SetInt("IsGameRunning",1);
        }

        private void CreatePuzzleButtons()
        {
            CreatePuzzleSprites();
            GameData.Instance.ButtonsData.Clear();
            for (int i = 0; i < _totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                btn.SetButtonData(i, _puzzleSprites[i]);
                GameData.Instance.ButtonsData.Add(new ButtonData(i,true, _puzzleSprites[i].name));
            }
        }

        private void CreatePuzzleSprites()
        {
            for (int i = 0; i < _totalButtonsCount; i++)
            {
                _puzzleSprites.Add(spriteHolder.Sprites[i % (_totalButtonsCount/2)]);
            }
            Randomizer.RandomizeList(ref _puzzleSprites);
        }

        private void OnButtonClickedEvent(object sender, EventArgs e)
        {
            PuzzleButton currentButton = (PuzzleButton)sender;
            if (_button1 == null)
            {
                _button1 = currentButton;
            }
            else
            {
                if (_button1.ItemImg == currentButton.ItemImg)
                {
                    StartCoroutine(currentButton.RemoveItem());
                    StartCoroutine(_button1.RemoveItem());
                    EventsManager.Instance.OnItemMatched.Invoke(this,null);
                }
                else
                {
                    StartCoroutine(currentButton.HideItem());
                    StartCoroutine(_button1.HideItem());
                    EventsManager.Instance.OnItemUnMatched.Invoke(this,null);
                }
                _button1 = null;
            }
        }
        
        private void OnItemHideStartEvent(object sender, EventArgs e)
        {
            SoundPlayer.Instance.PlaySFX(SoundNames.INCORRECT);
        }
        
        private void OnItemRemoveStartEvent(object sender, EventArgs e)
        {
            SoundPlayer.Instance.PlaySFX(SoundNames.CORRECT);
        }
        
        private void OnGameFinishedEvent(object sender, EventArgs e)
        {
            _isGameFinished = true;
            PlayerPrefs.SetInt("IsGameRunning",0);
            StorageHandler.Instance.WriteToFile("");
        }

        private void OnItemRemovedEvent(object sender, EventArgs e)
        {
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_isGameFinished)
            {
                gameOverPopup.SetActive(true);
                SoundPlayer.Instance.PlaySFX(SoundNames.GAME_END);
                StorageHandler.Instance.WriteToFile("");
            }
        }

        private void OnEnable()
        {
            EventsManager.Instance.OnButtonClicked += OnButtonClickedEvent;
            EventsManager.Instance.OnItemHideStart += OnItemHideStartEvent;
            EventsManager.Instance.OnItemRemoveStart += OnItemRemoveStartEvent;
            EventsManager.Instance.OnItemRemoved += OnItemRemovedEvent;
            EventsManager.Instance.OnGameFinished += OnGameFinishedEvent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnButtonClicked -= OnButtonClickedEvent;
            EventsManager.Instance.OnItemHideStart -= OnItemHideStartEvent;
            EventsManager.Instance.OnItemRemoveStart -= OnItemRemoveStartEvent;
            EventsManager.Instance.OnItemRemoved -= OnItemRemovedEvent;
            EventsManager.Instance.OnGameFinished -= OnGameFinishedEvent;
        }

        private void LoadGameFromData(GameData gameData)
        {
            GameData.Instance = gameData;
            _totalButtonsCount = gameData.TotalButtonsCount;
            GlobalData.targetCount = _totalButtonsCount / 2;
            CreatePuzzleButtonsFromData();
            EventsManager.Instance.OnLoadingGameFromData.Invoke(this,null);
        }

        private void CreatePuzzleButtonsFromData()
        {
            for (int i = 0; i < _totalButtonsCount; i++)
            {
                PuzzleButton btn = Instantiate(puzzleButton, gamePanel, false);
                ButtonData buttonData = GameData.Instance.ButtonsData[i];
                if (buttonData.IsVisible)
                {
                    btn.SetButtonData(buttonData.Id, GetSprite(buttonData.SpriteName));
                }
                else
                {
                    btn.MakeButtonInvisible();
                }
            }
        }

        private Sprite GetSprite(string spriteName)
        {
            foreach (var sprite in spriteHolder.Sprites)
            {
                if (sprite.name.Equals(spriteName))
                {
                    return sprite;
                }
            }

            return spriteHolder.Sprites[0];
        }
    }
}
