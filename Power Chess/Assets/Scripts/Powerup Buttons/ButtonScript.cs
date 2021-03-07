using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour
{
    int n;

    public Button button;

    void Update()
    {
        BoardManager board = BoardManager.Instance;

        if (board.isWhiteTurn)
        {
            if(Coin.WhiteCoins < 1)
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
            if(Coin.BlackCoins < 1)
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
   
    public void OnButtonPress()
    {
        n++;
        Debug.Log("Button clicked " + n + " times.");
    }

    public void SpawnAPiece()
    {
        BoardManager board = BoardManager.Instance;
        int x = board.emptySelectionX;
        int z = board.emptySelectionZ;

        if (x > -1 && z > -1 && board.Pieces[x, z] == null)
            PurchasePiece(x, z, board.isWhiteTurn);
        else
            OnButtonPress();
    }

    protected abstract void PurchasePiece(int x, int z, bool isWhiteTurn);
}