using AlfredoMB.Tetris.Models;
using UnityEngine;

namespace AlfredoMB.Tetris.Controllers
{
    public class TetrisBlockGroupSpawnController
    {
        private TetrisBoardController _boardController;
        private TetrisStageConfig _stageConfig;
        private readonly int _spawnX;
        private readonly int _spawnY;

        public TetrisBlockGroupSpawnController(TetrisBoard board, TetrisBoardController boardController, TetrisStageConfig stageConfig)
        {
            _spawnX = board.TetrisBlocks.GetLength(0) / 2;
            _spawnY = board.TetrisBlocks.GetLength(1);
            _boardController = boardController;
            _stageConfig = stageConfig;
        }

        public bool Spawn()
        {
            var randomBlockIndex = Random.Range(0, _stageConfig.AvailableBlockGroups.Length);

            var instance = Object.Instantiate(_stageConfig.AvailableBlockGroups[randomBlockIndex]);
            instance.Initialize();
            _boardController.SetCurrentBlockGroup(instance);

            // for pivots on the first line
            if (_boardController.MoveCurrentBlockGroup(_spawnX, _spawnY - 1))
            {
                return true;
            }

            // for pivots on the second line
            if (_boardController.MoveCurrentBlockGroup(_spawnX, _spawnY - 2))
            {
                return true;
            }
            return false;
        }
    }
}
