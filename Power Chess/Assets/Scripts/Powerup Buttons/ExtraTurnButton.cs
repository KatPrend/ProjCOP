using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraTurnButton : MonoBehaviour
{
    public GameObject Quarter;
    public Button button;
    public int Cost;

    void Update()
    {
        BoardManager board = BoardManager.Instance;

        if (board.isWhiteTurn)
        {
            if (Coin.WhiteCoins < Cost)
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
            if (Coin.BlackCoins < Cost)
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

    public void PurchaseExtraTurn()
    {
        BoardManager board = BoardManager.Instance;

        //Check for coins
        if (board.isWhiteTurn && Coin.WhiteCoins >= Cost || !board.isWhiteTurn && Coin.BlackCoins >= Cost)
        {
            // Make coin visible
            Quarter.SetActive(true);

            // Implement coin flip
            //Quarter.GetComponent<CoinFlip>().TwoExtraTurns();

            // Deduct coins from purchase
            Coin.RemoveCoins(board.isWhiteTurn, Cost);
        }
    }
}
