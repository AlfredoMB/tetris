using AlfredoMB.Tetris.Controllers;
using UnityEngine;

namespace AlfredoMB.Tetris.Views
{
    // Using abstract class instead of interface to take advantage of Unity Editor's referencing.
    public class AbstractView : MonoBehaviour
    {
        public TetrisGameController GameController;
    }
}