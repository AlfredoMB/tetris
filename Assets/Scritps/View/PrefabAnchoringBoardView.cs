using AlfredoMB.Tetris.Models;
using UnityEngine;

namespace AlfredoMB.Tetris.Views
{
    public class PrefabAnchoringBoardView : AbstractView
    {
        public GameObject BlockReference;

        public RectTransform Root;
        public RectTransform HorizontalReference;
        public RectTransform VerticalReference;

        public RectTransform[] Prefabs;

        private int _maxX;
        private int _maxY;

        private RectTransform[,] _blockViewSlots;
        private bool _shouldUpdateBoard;

        private TetrisBlockViewPool _blockViewPool;

        private void Start()
        {
            GameController.Board.OnBoardChanged += OnBoardChanged;
            _blockViewPool = new TetrisBlockViewPool(Prefabs);
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

            _blockViewSlots = new RectTransform[_maxX, _maxY];

            for (int i = 0; i < _maxY; i++)
            {
                for (int j = 0; j < _maxX; j++)
                {
                    var newBlockViewSlot = Instantiate(BlockReference, transform);
                    newBlockViewSlot.name = "block_" + j + "_" + i;
                    var rectTransform = newBlockViewSlot.GetComponent<RectTransform>();

                    rectTransform.anchorMin = Root.anchorMin + anchorMinHorizontalDistance * j + anchorMinVerticalDistance * i;
                    rectTransform.anchorMax = Root.anchorMax + anchorMaxHorizontalDistance * j + anchorMaxVerticalDistance * i;
                    rectTransform.anchoredPosition = Vector2.zero;

                    newBlockViewSlot.SetActive(true);
                    _blockViewSlots[j, _maxY - i - 1] = rectTransform;    // inverting just to avoid inverting the references
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
            if (_blockViewSlots == null)
            {
                BuildBoardView(GameController.Board);
            }

            _blockViewPool.RetrieveAll();

            TetrisBlock block;
            RectTransform blockViewSlot;
            RectTransform blockView;
            for (int i = 0; i < _maxY; i++)
            {
                for (int j = 0; j < _maxX; j++)
                {
                    block = GameController.Board.TetrisBlocks[j, i];
                    if (block != null)
                    {
                        blockViewSlot = _blockViewSlots[j, i];
                        blockView = _blockViewPool.GetBlockView(block.BlockType);

                        blockView.SetParent(blockViewSlot);
                        blockView.localScale = Vector3.one;

                        blockView.anchorMin = Vector2.one * 0.5f;
                        blockView.anchorMax = Vector2.one * 0.5f;
                        blockView.anchoredPosition = Vector2.zero;
                        blockView.sizeDelta = blockViewSlot.sizeDelta;
                    }
                }
            }
        }
    }
}
