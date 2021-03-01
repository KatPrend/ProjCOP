using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class TestPieceMoves
    {
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Chess");
        }

        [UnityTest]
        public IEnumerator TestKingArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8]; // all false

            yield return null;

            // King cannot move at start of game
            Piece whiteKing = board.Pieces[4, 0];
            Assert.AreEqual(expectedArray, whiteKing.ArrayOfValidMove());
        }

        [UnityTest]
        public IEnumerator TestKingArrayOfValidMovesCanMove()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            yield return null;

            // Move pawn above king up one. King can now move up
            board.Pieces[4, 1].SetPosition(4, 2);
            board.Pieces[4, 1] = null;

            expectedArray[4, 1] = true;

            Piece whiteKing = board.Pieces[4, 0];
            Assert.AreEqual(expectedArray, whiteKing.ArrayOfValidMove());
        }
    }
}
