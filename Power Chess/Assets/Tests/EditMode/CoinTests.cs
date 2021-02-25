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

            // each player starts with 0 coins
            Assert.AreEqual(Coin.WhiteCoins, 0); 
            Assert.AreEqual(Coin.BlackCoins, 0);

            Coin.AddCoin(true);
            
            // Only white gaines a coin
            Assert.AreEqual(Coin.WhiteCoins, 1);
            Assert.AreEqual(Coin.BlackCoins, 0);
        }

        [Test]
        public void TestRemoveCoinMethod()
        {
            Coin.WhiteCoins = 12;
            Coin.BlackCoins = 12;

            // isWhiteTurn = false
            Coin.RemoveCoins(false, 6);

            // Only black has 6 coins removed
            Assert.AreEqual(Coin.WhiteCoins, 12);
            Assert.AreEqual(Coin.BlackCoins, 6);
        }
    }
}
