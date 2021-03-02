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

        [UnityTest]
        public IEnumerator TestPawnArrayOfValidMovesCapture()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray;

            yield return null;

            // Move white pawn above queen up two
            board.Pieces[3, 1].SetPosition(3, 3);
            board.Pieces[3, 3] = board.Pieces[3, 1];
            board.Pieces[3, 1] = null;

            // Move pawn below king down two
            board.Pieces[4, 6].SetPosition(4, 4);
            board.Pieces[4, 4] = board.Pieces[4, 6];
            board.Pieces[4, 6] = null;

            expectedArray = new bool[8, 8];
            expectedArray[3, 4] = true;
            expectedArray[4, 4] = true;
            Assert.AreEqual(expectedArray, board.Pieces[3, 3].ArrayOfValidMove()); // test white pawn

            expectedArray = new bool[8, 8];
            expectedArray[4, 3] = true;
            expectedArray[3, 3] = true;
            Assert.AreEqual(expectedArray, board.Pieces[4, 4].ArrayOfValidMove()); // test black pawn
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

        [UnityTest]
        public IEnumerator TestRookArrayOfValidMovesCanMove()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            yield return null;

            // Move left black pawn below roook down two.
            board.Pieces[0, 6].SetPosition(0, 4);
            board.Pieces[0, 4] = board.Pieces[0, 6];
            board.Pieces[0, 6] = null;

            expectedArray[0, 6] = true;
            expectedArray[0, 5] = true;

            Piece blackRook = board.Pieces[0, 7];
            Assert.AreEqual(expectedArray, blackRook.ArrayOfValidMove());
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

        [UnityTest]
        public IEnumerator TestKnightArrayOfValidMovesCanCapture()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            yield return null;

            // Move black queen in path of white knight.
            board.Pieces[3, 7].SetPosition(2, 2);
            board.Pieces[2, 2] = board.Pieces[3, 7];
            board.Pieces[3, 7] = null;

            expectedArray[0, 2] = true;
            expectedArray[2, 2] = true;

            Piece whiteKnight = board.Pieces[1, 0];
            Assert.AreEqual(expectedArray, whiteKnight.ArrayOfValidMove());
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

        [UnityTest]
        public IEnumerator TestBishopArrayOfValidMovesCanMove()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            yield return null;

            // Move down black pawn below right knight down one.
            board.Pieces[6, 6].SetPosition(6, 5);
            board.Pieces[6, 5] = board.Pieces[6, 6];
            board.Pieces[6, 6] = null;

            expectedArray[6, 6] = true;
            expectedArray[7, 5] = true;

            Piece blackBishop = board.Pieces[5, 7];
            Assert.AreEqual(expectedArray, blackBishop.ArrayOfValidMove());
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

        [UnityTest]
        public IEnumerator TestQueenArrayOfValidMovesCanCapture()
        {
            BoardManager board = BoardManager.Instance;
            bool[,] expectedArray = new bool[8, 8];

            yield return null;

            // Remove white pawn left of queen.
            board.Pieces[2, 1] = null;

            // Move black pawn down to diagonal path of queen.
            board.Pieces[1, 6].SetPosition(1, 2);
            board.Pieces[1, 2] = board.Pieces[1, 6];
            board.Pieces[1, 6] = null;

            expectedArray[2, 1] = true;
            expectedArray[1, 2] = true;

            Piece whiteQueen = board.Pieces[3, 0];
            Assert.AreEqual(expectedArray, whiteQueen.ArrayOfValidMove());
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
            board.Pieces[4, 2] = board.Pieces[4, 1];
            board.Pieces[4, 1] = null;

            expectedArray[4, 1] = true;

            Piece whiteKing = board.Pieces[4, 0];
            Assert.AreEqual(expectedArray, whiteKing.ArrayOfValidMove());
        }
    }
}
