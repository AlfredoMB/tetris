using UnityEngine;
using UnityEngine.UI;

public class PrefabBoardView : AbstractView
{/*
    public GameObject[] BlockPrefabs;

    private TetrisBoard _board;
    private int _maxX;
    private int _maxY;

    private Image[,] _blockViews;
    
    private void BuildBoardView(TetrisBoard board)
    {
        _maxX = board.TetrisBlocks.GetLength(0);
        _maxY = board.TetrisBlocks.GetLength(1);

        _blockViews = new Image[_maxX, _maxY];

        for (int i = 0; i < _maxY; i++)
        {
            for (int j = 0; j < _maxX; j++)
            {
                var newView = new GameObject("blockView_" + j + "_" + i).AddComponent<Image>();
                foreach (var prefab in BlockPrefabs)
                {
                    var newView = Instantiate(PrefabBoardView, transform)
                }
                newView.transform.SetParent(transform, false);
                newView.sprite = BlockPrefabs[0];
                newView.enabled = false;
                _blockViews[j, i] = newView;
            }
        }
    }

    private void Update()
    {
        if (_blockViews == null)
        {
            BuildBoardView(GameController.Board);
        }

        for (int i = 0; i < _maxY; i++)
        {
            for (int j = 0; j < _maxX; j++)
            {
                _blockViews[j, i].enabled = (GameController.Board.TetrisBlocks[j, i] != null);
            }
        }
    }*/
}