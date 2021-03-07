using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white knight
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(4, x, z);
            // spawn black knight
            else
                BoardManager.Instance.SpawnChessPiece(10, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
