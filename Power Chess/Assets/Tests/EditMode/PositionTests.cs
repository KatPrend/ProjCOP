using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PositionTests
    {
        // Pawn Tests:


        //Rook Tests:
        [Test]
        public void TestRookWithValidMove()
        {
            var gameObject = new GameObject();
            var rook = gameObject.AddComponent<Rook>();

            rook.SetPosition(2, 2);
            Assert.AreEqual(rook.ValidMove(2, 4), true);
            Assert.AreEqual(rook.ValidMove(5, 2), true);
        }

        [Test]
        public void TestRookWithInvalidMove()
        {
            var gameObject = new GameObject();
            var rook = gameObject.AddComponent<Rook>();

            rook.SetPosition(2, 2);
            Assert.AreEqual(rook.ValidMove(3, 3), false);
        }

        [Test]
        public void TestRookWithMoveOffTheBoard()
        {
            var gameObject = new GameObject();
            var rook = gameObject.AddComponent<Rook>();

            rook.SetPosition(2, 2);
            Assert.AreEqual(rook.ValidMove(9, 0), false);
        }

        // Knight Tests:
        [Test]
        public void TestKnightWithValidMove()
        {
            var gameObject = new GameObject();
            var knight = gameObject.AddComponent<Knight>();

            knight.SetPosition(2, 2);
            Assert.AreEqual(knight.ValidMove(3, 4), true);
        }

        [Test]
        public void TestKnightWithInvalidMove()
        {
            var gameObject = new GameObject();
            var knight = gameObject.AddComponent<Knight>();

            knight.SetPosition(1, 0);
            Assert.AreEqual(knight.ValidMove(2, 4), false);
        }

        [Test]
        public void TestKnightWithMoveOffTheBoard()
        {
            var gameObject = new GameObject();
            var knight = gameObject.AddComponent<Knight>();

            knight.SetPosition(1, 0);
            Assert.AreEqual(knight.ValidMove(-1, 0), false);
        }

        // Bishop Tests


        // Queen Tests


        // King Tests

    }
}
