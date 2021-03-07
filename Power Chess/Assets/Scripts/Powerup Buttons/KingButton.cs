using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white king
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(0, x, z);
            // spawn black king
            else
                BoardManager.Instance.SpawnChessPiece(6, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
