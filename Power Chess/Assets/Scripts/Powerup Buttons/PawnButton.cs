using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnButton : ButtonScript
{
    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 1 || !isWhiteTurn && Coin.BlackCoins >= 1)
        {
            // spawn white pawn
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(5, x, z);
            // spawn black pawn
            else
                BoardManager.Instance.SpawnChessPiece(11, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 1);
        }
    }
}
