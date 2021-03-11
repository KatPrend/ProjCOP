using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose a3
            Assert.Null(board.Pieces[0, 2]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 2;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 9;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.NotNull(board.Pieces[0, 2]);
        }

        [UnityTest]
        public IEnumerator TestButtonWillNotSpawnPieceInOccupiedSpot()
        {
            GameObject go = new GameObject();
            PawnButton button = go.AddComponent<PawnButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose e1
            Assert.NotNull(board.Pieces[4, 0]);
            board.emptySelectionX = 4;
            board.emptySelectionZ = 0;
            Piece original = board.Pieces[4, 0];

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 9;

            button.SpawnAPiece();
            // Piece is still original pawn and not a new spawned piece
            Assert.AreEqual(original, board.Pieces[4, 0]);
        }

        [UnityTest]
        public IEnumerator TestButtonWillNotSpawnWithoutEnoughCoins()
        {
            GameObject go = new GameObject();
            RookButton button = go.AddComponent<RookButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a2
            Assert.Null(board.Pieces[0, 1]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 1;

            // It is white's turn and they do not have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 0;

            button.SpawnAPiece();
            Assert.Null(board.Pieces[0, 1]);
        }

        [UnityTest]
        public IEnumerator TestCoinDedectionWithPurchase()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose d6
            Assert.Null(board.Pieces[5, 5]);
            board.emptySelectionX = 5;
            board.emptySelectionZ = 5;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = false;
            Coin.BlackCoins = 15;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.NotNull(board.Pieces[5, 5]);
            Assert.AreEqual(6, Coin.BlackCoins);
        }

        [UnityTest]
        public IEnumerator TestWhitePlayerCannotPlacePowerupPieceOnOppenentSide()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose a5
            Assert.Null(board.Pieces[0, 4]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 4;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 15;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.Null(board.Pieces[0, 4]);
        }

        [UnityTest]
        public IEnumerator TestWhitePlayerCanPlacePowerupPieceOnTheirSide()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose a4
            Assert.Null(board.Pieces[0, 3]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 3;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 15;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.NotNull(board.Pieces[0, 3]);
        }

        [UnityTest]
        public IEnumerator TestBlackPlayerCannotPlacePowerupPieceOnOppenentSide()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose a4
            Assert.Null(board.Pieces[0, 3]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 3;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = false;
            Coin.BlackCoins = 15;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.Null(board.Pieces[0, 3]);
        }

        [UnityTest]
        public IEnumerator TestBlackPlayerCanPlacePowerupPieceOnTheirSide()
        {
            GameObject go = new GameObject();
            PawnButton button = go.AddComponent<PawnButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            // Choose a5
            Assert.Null(board.Pieces[0, 4]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 4;

            // It is black's turn and they have sufficient coins
            board.isWhiteTurn = false;
            Coin.WhiteCoins = 15;

            button.SpawnAPiece();
            // Piece is spawned in selected empty spot
            Assert.NotNull(board.Pieces[0, 4]);
        }

        // Test Piece Buttons Spawn Correct Piece
        // Queen
        [UnityTest]
        public IEnumerator TestQueenButtonSpawnsQueen()
        {
            GameObject go = new GameObject();
            QueenButton button = go.AddComponent<QueenButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            Assert.Null(board.Pieces[3, 5]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 5;

            // It is black's turn and they have sufficient coins
            board.isWhiteTurn = false;
            Coin.BlackCoins = 9;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Queen), board.Pieces[3, 5]);
        }

        // Rook
        [UnityTest]
        public IEnumerator TestRookButtonSpawnsRook()
        {
            GameObject go = new GameObject();
            RookButton button = go.AddComponent<RookButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            Assert.Null(board.Pieces[3, 2]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 2;

            board.isWhiteTurn = true;
            Coin.WhiteCoins = 5;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Rook), board.Pieces[3, 2]);
        }

        // Knight
        [UnityTest]
        public IEnumerator TestKnightButtonSpawnsKnight()
        {
            GameObject go = new GameObject();
            KnightButton button = go.AddComponent<KnightButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            Assert.Null(board.Pieces[3, 5]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 5;

            board.isWhiteTurn = false;
            Coin.BlackCoins = 3;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Knight), board.Pieces[3, 5]);
        }

        // Bishop
        [UnityTest]
        public IEnumerator TestBishopButtonSpawnsBishop()
        {
            GameObject go = new GameObject();
            BishopButton button = go.AddComponent<BishopButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

            yield return null;

            Assert.Null(board.Pieces[3, 2]);
            board.emptySelectionX = 3;
            board.emptySelectionZ = 2;

            board.isWhiteTurn = true;
            Coin.WhiteCoins = 3;

            button.SpawnAPiece();
            Assert.IsInstanceOf(typeof(Bishop), board.Pieces[3, 2]);
        }

        // Pawn
        [UnityTest]
        public IEnumerator TestPawnButtonSpawnsPawn()
        {
            GameObject go = new GameObject();
            PawnButton button = go.AddComponent<PawnButton>();
            button.button = go.AddComponent<Button>();

            BoardManager board = BoardManager.Instance;
            board.isFirstMove = false;

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
