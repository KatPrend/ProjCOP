using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour
{
    int n;

    public void ButtonPress(string input)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = input;
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