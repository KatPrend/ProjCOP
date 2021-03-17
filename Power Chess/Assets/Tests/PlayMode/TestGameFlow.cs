using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NSubstitute;

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

            Vector3 whiteKingPosition = board.Pieces[4, 0].transform.position;
            Assert.AreEqual(whiteKingPosition, new Vector3(4.5f, 0, 0.5f));
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

        [UnityTest]
        public IEnumerator TestStartingBoardSpawnsKings()
        {
            BoardManager board = BoardManager.Instance;

            yield return null;

            // White King
            Assert.IsInstanceOf(typeof(King), actual: board.Pieces[4, 0]);
            // Black King
            Assert.IsInstanceOf(typeof(King), actual: board.Pieces[4, 7]);
        }

        [UnityTest]
        public IEnumerator TestPlayersStartWithThreeCoins()
        {
            yield return null;

            Assert.AreEqual(3, Coin.WhiteCoins);
            Assert.AreEqual(3, Coin.BlackCoins);
        }

        [UnityTest]
        public IEnumerator TestWhitePlayerCannotPlaceInitialPiecesPastFirstTwoRows()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a2
            Assert.Null(board.Pieces[0, 2]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 2;

            button.SpawnAPiece();
            // Piece is NOT spawned in selected empty spot
            Assert.Null(board.Pieces[0, 2]);
        }

        [UnityTest]
        public IEnumerator TestBlackPlayerCannotPlaceInitialPiecesBelowLastTwoRows()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a2
            Assert.Null(board.Pieces[0, 5]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 5;

            board.isWhiteTurn = false;

            button.SpawnAPiece();
            // Piece is NOT spawned in selected empty spot
            Assert.Null(board.Pieces[0, 5]);
        }
    }
}
