using UnityEngine;

public class Initializer : MonoBehaviour
{
    public InputView InputView;
    public AbstractBoardView BoardView;

    public TetrisBoardConfig BoardSize;
    public TetrisBlockGroup BlockGroup;
    
    private TetrisBoard _board;
    private TetrisBoardController _boardController;
    private TetrisBlockGroupController _blockGroupController;
    private TetrisBlockGroupSpawner _spawner;

    private void Awake()
    {
        _board = new TetrisBoard(BoardSize.BoardSizeX, BoardSize.BoardSizeY);
        _boardController = new TetrisBoardController(_board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawner(_board, _boardController);
        
        InputView.SetTetrisBlockGroupController(_blockGroupController);
        BoardView.SetBoard(_board);
    }

    private void OnEnable()
    {
        _spawner.Spawn(BlockGroup);
    }
    /*
    public void UpdateBoard()
    {
        if (MoveCurrentBlockGroup(0, -1))
        {
            return;
        }
    }*/


}
