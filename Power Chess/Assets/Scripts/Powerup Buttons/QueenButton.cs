using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white queen
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(1, x, z);
            // spawn black queen
            else
                BoardManager.Instance.SpawnChessPiece(7, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
