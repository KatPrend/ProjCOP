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
        [Test]
        public void TestKingWithValidMove()
        {
            var gameObject = new GameObject();
            var king = gameObject.AddComponent<King>();

            king.SetPosition(1, 1);
            Assert.AreEqual(king.ValidMove(1, 2), true);
        }

        [Test]
        public void TestKingWithInvalidMove()
        {
            var gameObject = new GameObject();
            var king = gameObject.AddComponent<King>();

            king.SetPosition(1, 1);
            Assert.AreEqual(king.ValidMove(4, 1), false);
        }

        [Test]
        public void TestKingWithMoveOffTheBoard()
        {
            var gameObject = new GameObject();
            var king = gameObject.AddComponent<King>();

            king.SetPosition(1, 0);
            Assert.AreEqual(king.ValidMove(1, -0), false);
        }
    }
}
