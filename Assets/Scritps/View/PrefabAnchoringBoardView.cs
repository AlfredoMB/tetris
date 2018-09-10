using UnityEngine;

public class PrefabAnchoringBoardView : AbstractView
{
    public GameObject BlockReference;

    public RectTransform Root;
    public RectTransform HorizontalReference;
    public RectTransform VerticalReference;

    public RectTransform[] Prefabs;

    private int _maxX;
    private int _maxY;

    private RectTransform[,] _blockViews;
    private bool _shouldUpdateBoard;

    private ObjectPool _blockPool;

    private void Start()
    {
        GameController.Board.OnBoardChanged += OnBoardChanged;
        _blockPool = new ObjectPool(Prefabs);
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

        _blockViews = new RectTransform[_maxX, _maxY];

        for (int i = 0; i < _maxY; i++)
        {
            for (int j = 0; j < _maxX; j++)
            {
                var newBlock = Instantiate(BlockReference, transform);
                newBlock.name = "block_" + j + "_" + i;
                var rectTransform = newBlock.GetComponent<RectTransform>();

                rectTransform.anchorMin = Root.anchorMin + anchorMinHorizontalDistance * j + anchorMinVerticalDistance * i;
                rectTransform.anchorMax = Root.anchorMax + anchorMaxHorizontalDistance * j + anchorMaxVerticalDistance * i;
                rectTransform.anchoredPosition = Vector2.zero;

                newBlock.SetActive(true);
                _blockViews[j, _maxY - i - 1] = rectTransform;    // inverting just to avoid inverting the references
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

        _blockPool.RetrieveAll();

        TetrisBlock block;
        for (int i = 0; i < _maxY; i++)
        {
            for (int j = 0; j < _maxX; j++)
            {
                block = GameController.Board.TetrisBlocks[j, i];
                if (block != null)
                {
                    var blockView = _blockViews[j, i];
                    var blockInstance = _blockPool.GetBlockView(block.BlockType);
                    blockInstance.SetParent(blockView);
                    blockInstance.localScale = Vector3.one;

                    blockInstance.anchorMin = Vector2.one * 0.5f;
                    blockInstance.anchorMax = Vector2.one * 0.5f;
                    blockInstance.anchoredPosition = Vector2.zero;
                    blockInstance.sizeDelta = blockView.sizeDelta;
                }
            }
        }
    }
}
