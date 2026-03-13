using System.Collections;
using Controllers;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Views.Game
{
    public class PuzzleButton : MonoBehaviour
    {
        [SerializeField] private Sprite bgImg;
        private Sprite itemImg;

        public Sprite ItemImg
        {
            get => itemImg;
        }

        private Button button;

        public void OnButtonClicked()
        {
            if(button.image.sprite == itemImg)
                return;
            StartCoroutine(ShowItem());
            EventsManager.Instance.OnButtonClicked.Invoke(this,null);
        }

        public void SetButtonData(int index, Sprite sprite)
        {
            name = index + "";
            itemImg = sprite;
            button = GetComponent<Button>();
            button.image.sprite = bgImg;
        }

        private IEnumerator ShowItem()
        {
            SoundPlayer.Instance.PlaySFX(SoundNames.OPEN);
            
            transform.rotation = Quaternion.Euler(0,180,0);
            for (int i = 180; i > 0 ; i-=10)
            {
                transform.rotation = Quaternion.Euler(0,i,0);
                if (i == 90)
                {
                    button.image.sprite = itemImg;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        public IEnumerator HideItem()
        {
            yield return new WaitForSeconds(1f);
            
            EventsManager.Instance.OnItemHideStart.Invoke(this,null);
            
            transform.rotation = Quaternion.Euler(0,0,0);
            for (int i = 0; i < 180 ; i+=10)
            {
                transform.rotation = Quaternion.Euler(0,i,0);
                if (i == 90)
                {
                    button.image.sprite = bgImg;
                }
                yield return new WaitForSeconds(0.01f);
            }
            
            //Setting Back to Original Rotate Value Else Click won't work
            transform.rotation = Quaternion.Euler(0,0,0);
        }

        public IEnumerator RemoveItem()
        {
            GameData.Instance.ButtonsData[int.Parse(name)].IsVisible = false;
            yield return new WaitForSeconds(1f);
            
            EventsManager.Instance.OnItemRemoveStart.Invoke(this,null);
            
            Color to = new Color(1, 1, 1, 0);
            DOTween.To(() => button.image.color, x => button.image.color = x, to, 0.3f).OnComplete(() => {
                button.interactable = false;
                EventsManager.Instance.OnItemRemoved.Invoke(this,null);
            });
        }

        public void MakeButtonInvisible()
        {
            button = GetComponent<Button>();
            button.image.sprite = bgImg;
            
            button.interactable = false;
            button.image.color = new Color(1, 1, 1, 0);
        }
        
    }
}
