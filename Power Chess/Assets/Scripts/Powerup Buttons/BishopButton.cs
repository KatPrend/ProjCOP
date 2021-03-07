using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white bishop
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(3, x, z);
            // spawn black bishop
            else
                BoardManager.Instance.SpawnChessPiece(9, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
