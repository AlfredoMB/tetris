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
    private TetrisGravityController _gravity;

    private void Awake()
    {
        _board = new TetrisBoard(BoardSize.BoardSizeX, BoardSize.BoardSizeY);
        _boardController = new TetrisBoardController(_board);
        _blockGroupController = new TetrisBlockGroupController(_boardController);
        _spawner = new TetrisBlockGroupSpawner(_board, _boardController);
        _gravity = new TetrisGravityController(BoardSize.Gravity, BoardSize.GravityUpdateInterval, _boardController);
                
        InputView.SetTetrisBlockGroupController(_blockGroupController);
        BoardView.SetBoard(_board);
    }

    private void OnEnable()
    {
        _spawner.Spawn(BlockGroup);
    }

    private void Update()
    {
        _gravity.Update();
        if (!_gravity.HitBottom)
        {
            return;
        }

        _spawner.Spawn(BlockGroup);

        _boardController.ConsumeFullLines();
    }
}
