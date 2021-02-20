using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PositionTests
    {
        [Test]
        public void TestKnightWithValidMove()
        {
            Piece knight = new Knight();
            knight.SetPosition(1, 0);

            Assert.AreEqual(knight.ValidMove(2,2), true);
        }

        [Test]
        public void TestKnightWithInvalidMove()
        {
            Piece knight = new Knight();
            knight.SetPosition(1, 0);

            Assert.AreEqual(knight.ValidMove(2,4), false);
        }

        [Test]
        public void TestKnightWithMoveOffTheBoard()
        {
            Piece knight = new Knight();
            knight.SetPosition(1, 0);

            Assert.AreEqual(knight.ValidMove(-1,0), false);
        }
    }
}
