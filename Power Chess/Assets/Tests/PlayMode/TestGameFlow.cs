using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestGameFlow
    {
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Chess");
        }
        
        [UnityTest]
        public IEnumerator TestGetSquareCenter()
        {
            BoardManager board = BoardManager.Instance;

            yield return null;

            Vector3 leftWhiteRookPosition = board.Pieces[0, 0].transform.position;
            Assert.AreEqual(leftWhiteRookPosition, new Vector3(0.5f, 0, 0.5f));
        }

        [UnityTest]
        public IEnumerator TestSpawnAllChessPiecesPutsPiecesInCorrectSquares()
        {
            BoardManager board = BoardManager.Instance;

            yield return null;

            // White pawns should be in indices (0-7, 1)
            for (int i = 0; i < 8; i++)
                Assert.IsInstanceOf(typeof(Pawn), actual: board.Pieces[i, 1]);
            // Black pawns should be in indices (0-7, 6)
            for (int i = 0; i < 8; i++)
                Assert.IsInstanceOf(typeof(Pawn), actual: board.Pieces[i, 6]);

            // White Rooks
            Assert.IsInstanceOf(typeof(Rook), actual: board.Pieces[0, 0]);
            Assert.IsInstanceOf(typeof(Rook), actual: board.Pieces[7, 0]);
            // Black Rooks
            Assert.IsInstanceOf(typeof(Rook), actual: board.Pieces[0, 7]);
            Assert.IsInstanceOf(typeof(Rook), actual: board.Pieces[7, 7]);

            // White Knights
            Assert.IsInstanceOf(typeof(Knight), actual: board.Pieces[1, 0]);
            Assert.IsInstanceOf(typeof(Knight), actual: board.Pieces[6, 0]);
            // Black Knights
            Assert.IsInstanceOf(typeof(Knight), actual: board.Pieces[1, 7]);
            Assert.IsInstanceOf(typeof(Knight), actual: board.Pieces[6, 7]);

            // White Bishops
            Assert.IsInstanceOf(typeof(Bishop), actual: board.Pieces[2, 0]);
            Assert.IsInstanceOf(typeof(Bishop), actual: board.Pieces[5, 0]);
            // Black Bishops
            Assert.IsInstanceOf(typeof(Bishop), actual: board.Pieces[2, 7]);
            Assert.IsInstanceOf(typeof(Bishop), actual: board.Pieces[5, 7]);

            // White Queen
            Assert.IsInstanceOf(typeof(Queen), actual: board.Pieces[3, 0]);
            // Black Queen
            Assert.IsInstanceOf(typeof(Queen), actual: board.Pieces[3, 7]);

            // White King
            Assert.IsInstanceOf(typeof(King), actual: board.Pieces[4, 0]);
            // Black King
            Assert.IsInstanceOf(typeof(King), actual: board.Pieces[4, 7]);
        }

        [UnityTest]
        public IEnumerator TestStartingSelectionPosition()
        {
            BoardManager board = BoardManager.Instance;

            yield return null;

            // (SelectionX, SelectionZ) starts at (-1,-1)
            Assert.AreEqual(-1, board.selectionX);
            Assert.AreEqual(-1, board.selectionZ);
            
        }

    }
}
