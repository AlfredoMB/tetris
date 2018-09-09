using UnityEngine;
using UnityEngine.UI;

public class ImageBoardView : AbstractView
{
    public GameObject Object;

    public Sprite[] Sprites;

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
                var newObject = Instantiate(Object, transform); // new GameObject("blockView_" + j + "_" + i).AddComponent<Image>();
                //newView.transform.SetParent(transform, false);
                newObject.SetActive(true);
                var newView = newObject.GetComponentInChildren<Image>();
                newView.sprite = Sprites[0];
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