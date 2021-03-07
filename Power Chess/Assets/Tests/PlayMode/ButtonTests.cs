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
            ButtonScript button = go.AddComponent<ButtonScript>();

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
            ButtonScript button = go.AddComponent<ButtonScript>();

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
            ButtonScript button = go.AddComponent<ButtonScript>();

            BoardManager board = BoardManager.Instance;

            yield return null;

            // Choose a3
            Assert.Null(board.Pieces[0, 2]);
            board.emptySelectionX = 0;
            board.emptySelectionZ = 2;

            // It is white's turn and they have sufficient coins
            board.isWhiteTurn = true;
            Coin.WhiteCoins = 0;

            button.SpawnAPiece();
            Assert.Null(board.Pieces[0, 2]);
        }
    }
}
