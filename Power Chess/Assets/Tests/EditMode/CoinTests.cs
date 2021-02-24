using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CoinTests
    {
        [Test]
        public void TestAddCoinMethod()
        {
            int expectedValue;

            // each player starts with 0 coins
            Assert.AreEqual(Coin.WhiteCoins, expectedValue = 0); 
            Assert.AreEqual(Coin.BlackCoins, expectedValue = 0);

            Coin.AddCoin(true);
            
            // Only white gaines a coin
            Assert.AreEqual(Coin.WhiteCoins, expectedValue = 1);
            Assert.AreEqual(Coin.BlackCoins, expectedValue = 0);
        }

        [Test]
        public void TestRemoveCoinMethod()
        {
            bool isWhiteTurn;
            int expectedValue;

            Coin.WhiteCoins = 12;
            Coin.BlackCoins = 12;

            Coin.RemoveCoins(isWhiteTurn = false, 6);

            // Only black has 6 coins removed
            Assert.AreEqual(Coin.WhiteCoins, expectedValue = 12);
            Assert.AreEqual(Coin.BlackCoins, expectedValue = 6);
        }
    }
}
