using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightButton : ButtonScript
{
    public Button button;

    void Update()
    {
        BoardManager board = BoardManager.Instance;

        if (board.isWhiteTurn)
        {
            if(Coin.WhiteCoins < 3)
            {
                ColorBlock colors = button.colors;
                colors.normalColor = Color.red;
                colors.highlightedColor = new Color32(255, 100, 100, 255);
                button.colors = colors;
            }
            else   
            {
                ColorBlock colors = button.colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = new Color32(60, 200, 50, 255);
                button.colors = colors;
            }
        }
        else
        {
            if(Coin.BlackCoins < 3)
            {
                ColorBlock colors = button.colors;
                colors.normalColor = Color.red;
                colors.highlightedColor = new Color32(255, 100, 100, 255);
                button.colors = colors;
            }
            else   
            {
                ColorBlock colors = button.colors;
                colors.normalColor = Color.green;
                colors.highlightedColor = new Color32(60, 200, 50, 255);
                button.colors = colors;
            }
        }
    }

    protected override void PurchasePiece(int x, int z, bool isWhiteTurn)
    {
        // Check for coins
        if (isWhiteTurn && Coin.WhiteCoins >= 3 || !isWhiteTurn && Coin.BlackCoins >= 3)
        {
            // spawn white knight
            if (isWhiteTurn)
                BoardManager.Instance.SpawnChessPiece(4, x, z);
            // spawn black knight
            else
                BoardManager.Instance.SpawnChessPiece(10, x, z);

            // deduct coins from purchase
            Coin.RemoveCoins(isWhiteTurn, 3);
        }
    }
}
