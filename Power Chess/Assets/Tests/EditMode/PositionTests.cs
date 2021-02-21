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

    }
}
