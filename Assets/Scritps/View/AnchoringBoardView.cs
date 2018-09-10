using AlfredoMB.Tetris.Models;
using UnityEngine;
using UnityEngine.UI;

namespace AlfredoMB.Tetris.Views
{
    public class AnchoringBoardView : AbstractView
    {
        public GameObject Block;

        public RectTransform Root;
        public RectTransform HorizontalReference;
        public RectTransform VerticalReference;

        public Sprite[] Sprites;

        private int _maxX;
        private int _maxY;

        private Image[,] _blockViews;
        private bool _shouldUpdateBoard;

        private void Start()
        {
            GameController.Board.OnBoardChanged += OnBoardChanged;
        }

        private void OnDisable()
        {
            GameController.Board.OnBoardChanged -= OnBoardChanged;
        }

        private void BuildBoardView(TetrisBoard board)
        {
            _maxX = board.TetrisBlocks.GetLength(0);
            _maxY = board.TetrisBlocks.GetLength(1);

            var anchorMinHorizontalDistance = HorizontalReference.anchorMin - Root.anchorMin;
            var anchorMaxHorizontalDistance = HorizontalReference.anchorMax - Root.anchorMax;

            var anchorMinVerticalDistance = VerticalReference.anchorMin - Root.anchorMin;
            var anchorMaxVerticalDistance = VerticalReference.anchorMax - Root.anchorMax;

            _blockViews = new Image[_maxX, _maxY];

            for (int i = 0; i < _maxY; i++)
            {
                for (int j = 0; j < _maxX; j++)
                {
                    var newBlock = Instantiate(Block, transform);
                    newBlock.name = "block_" + j + "_" + i;
                    var rectTransform = newBlock.GetComponent<RectTransform>();

                    rectTransform.anchorMin = Root.anchorMin + anchorMinHorizontalDistance * j + anchorMinVerticalDistance * i;
                    rectTransform.anchorMax = Root.anchorMax + anchorMaxHorizontalDistance * j + anchorMaxVerticalDistance * i;
                    rectTransform.anchoredPosition = Vector2.zero;

                    newBlock.SetActive(true);
                    var newView = newBlock.GetComponent<Image>();
                    newView.sprite = null;
                    newView.enabled = false;
                    _blockViews[j, _maxY - i - 1] = newView;    // inverting just to avoid inverting the references
                }
            }
        }

        private void OnBoardChanged()
        {
            _shouldUpdateBoard = true;
        }

        private void LateUpdate()
        {
            if (!_shouldUpdateBoard)
            {
                return;
            }
            _shouldUpdateBoard = false;
            UpdateBoard();
        }

        private void UpdateBoard()
        {
            if (_blockViews == null)
            {
                BuildBoardView(GameController.Board);
            }

            TetrisBlock block;
            for (int i = 0; i < _maxY; i++)
            {
                for (int j = 0; j < _maxX; j++)
                {
                    block = GameController.Board.TetrisBlocks[j, i];
                    if (block != null)
                    {
                        _blockViews[j, i].enabled = true;
                        _blockViews[j, i].sprite = Sprites[(int)block.BlockType];
                    }
                    else
                    {
                        _blockViews[j, i].enabled = false;
                    }
                }
            }
        }
    }
}
