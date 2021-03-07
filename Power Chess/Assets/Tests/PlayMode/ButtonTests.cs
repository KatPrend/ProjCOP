using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ButtonTests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Chess");
        }


        [UnityTest]
        public IEnumerator TestButtonCanSpawnPieceInEmptySpot()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<KingButton>();

            BoardManager board = BoardManager.Instance;
            
            yield return null;

            // Choose a3
            Assert.Null(board.Pieces[0, 2]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 2;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 1;

            button.SpawnAPiece();
            Assert.NotNull(board.Pieces[0, 2]);
        }

        [UnityTest]
        public IEnumerator TestButtonWillNotSpawnPieceInOccupiedSpot()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<QueenButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a2
            Assert.NotNull(board.Pieces[0, 1]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 1;
            Piece original = board.Pieces[0, 1];

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 1;

            button.SpawnAPiece();
            // Piece is still original pawn and not a new spawned piece
            Assert.AreEqual(original, board.Pieces[0, 1]);
        }

        [UnityTest]
        public IEnumerator TestButtonWillNotSpawnWithoutEnoughCoins()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<RookButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a3
            Assert.Null(board.Pieces[0, 2]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 2;

            // It is white's turn and they do not have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 0;

            button.SpawnAPiece();
            Assert.Null(board.Pieces[0, 2]);
        }

        // Test Piece Buttons Spawn Correct Piece
        // King
        [UnityTest]
        public IEnumerator TestKingButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<KingButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            Assert.Null(board.Pieces[3, 2]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 2;

            board.isWhiteTurn = true;
            Coin.WhiteCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(King), board.Pieces[3, 2]);
        }

        // Queen
        [UnityTest]
        public IEnumerator TestQueenButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<QueenButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a3
            Assert.Null(board.Pieces[3, 5]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 5;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = false;
            Coin.BlackCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Queen), board.Pieces[3, 5]);
        }

        // Rook
        [UnityTest]
        public IEnumerator TestRookButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<RookButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            Assert.Null(board.Pieces[3, 2]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 2;

            board.isWhiteTurn = true;
            Coin.WhiteCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Rook), board.Pieces[3, 2]);
        }

        // Knight
        [UnityTest]
        public IEnumerator TestKnightButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<KnightButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            Assert.Null(board.Pieces[3, 5]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 5;

            board.isWhiteTurn = false;
            Coin.BlackCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Knight), board.Pieces[3, 5]);
        }

        // Bishop
        [UnityTest]
        public IEnumerator TestBishopButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<BishopButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            Assert.Null(board.Pieces[3, 2]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 2;

            board.isWhiteTurn = true;
            Coin.WhiteCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Bishop), board.Pieces[3, 2]);
        }

        // Pawn
        [UnityTest]
        public IEnumerator TestPawnButtonSpawnsKing()
        {
            GameObject go = new GameObject();
            ButtonScript button = go.AddComponent<PawnButton>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            Assert.Null(board.Pieces[3, 5]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 5;

            board.isWhiteTurn = false;
            Coin.BlackCoins = 1;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Pawn), board.Pieces[3, 5]);
        }
    }
}
