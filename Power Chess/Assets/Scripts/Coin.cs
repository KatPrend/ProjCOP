using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Text playersCoins;

    public static int whiteCoins { get; set; }
    public static int blackCoins { get; set; }

    void Update()
    {
        playersCoins.text = "White Player's Coins: " + whiteCoins + "\nBlack Player's Coins: " + blackCoins;
    }

    public static void AddCoin(bool isWhiteTurn)
    {
        if (isWhiteTurn)
            whiteCoins++;
        else
            blackCoins++;
    }

    public static void RemoveCoins(bool isWhiteTurn, int numCoins)
    {
        if (isWhiteTurn)
            whiteCoins -= numCoins;
        else
            blackCoins -= numCoins;
    }
}
