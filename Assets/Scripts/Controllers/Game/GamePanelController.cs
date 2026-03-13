using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Controllers.Game
{
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanelRectTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        private void Awake()
        {
            SetUpGridLayout();
        }

        private void SetUpGridLayout()
        {
            var rect = gamePanelRectTransform.rect;
            float buttonWidth = (rect.width - 70) / GlobalData.columnCount;
            float buttonHeight = (rect.height - 70) / GlobalData.rowCount;
            float minSize = Mathf.Min(buttonWidth, buttonHeight);

            gridLayoutGroup.cellSize = new Vector2(minSize,minSize);
            gridLayoutGroup.spacing = new Vector2(10,10);
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = GlobalData.columnCount;
        }
    }
}
