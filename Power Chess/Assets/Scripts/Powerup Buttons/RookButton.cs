using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white rook
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(2, x, z);
            // spawn black rook
            else
                BoardManager.Instance.SpawnChessPiece(8, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
