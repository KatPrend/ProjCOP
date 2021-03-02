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
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("Chess");
        }

        // Pawn
        [UnityTest]
        public IEnumerator TestWhitePawnArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray;

            yield return null;

            Piece whitePawn;
            for (int i = 0; i < 8; i++)
            {
                expectedArray = new bool[8, 8];
                expectedArray[i, 2] = true;
                expectedArray[i, 3] = true;

                whitePawn = board.Pieces[i, 1];
                Assert.AreEqual(expectedArray, whitePawn.ArrayOfValidMove());
            }
        }

        [UnityTest]
        public IEnumerator TestBlackPawnArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray;

            yield return null;

            Piece blackPawn;
            for (int i = 0; i < 8; i++)
            {
                expectedArray = new bool[8, 8];
                expectedArray[i, 5] = true;
                expectedArray[i, 4] = true;

                blackPawn = board.Pieces[i, 6];
                Assert.AreEqual(expectedArray, blackPawn.ArrayOfValidMove());
            }
        }

        // Rook
        [UnityTest]
        public IEnumerator TestRookArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8]; // all false

            yield return null;

            // Rook cannot move at start of game
            Piece whiteRook = board.Pieces[0, 0];
            Assert.AreEqual(expectedArray, whiteRook.ArrayOfValidMove());
        }

        // Knight
        [UnityTest]
        public IEnumerator TestKnightArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            expectedArray[5, 5] = true;
            expectedArray[7, 5] = true;

            yield return null;

            Piece blackKnight = board.Pieces[6, 7];
            Assert.AreEqual(expectedArray, blackKnight.ArrayOfValidMove());
        }

        // Bishop
        [UnityTest]
        public IEnumerator TestBishopArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8]; // all false

            yield return null;

            // Bishop cannot move at start of game
            Piece whiteBishop = board.Pieces[2, 0];
            Assert.AreEqual(expectedArray, whiteBishop.ArrayOfValidMove());
        }

        // Queen
        [UnityTest]
        public IEnumerator TestQueenArrayOfValidMovesAtStartingPosition()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8]; // all false

            yield return null;

            // Queen cannot move at start of game
            Piece blackQueen = board.Pieces[3, 7];
            Assert.AreEqual(expectedArray, blackQueen.ArrayOfValidMove());
        }

        // King
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
