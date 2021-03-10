using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour
{
    int n;

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
            if (CheckSideOfBoard(x, z, board.isWhiteTurn))
                PurchasePiece(x, z, board.isWhiteTurn);
        else
            OnButtonPress();
    }

    private bool CheckSideOfBoard(int x, int z, bool isWhiteTurn)
    {
        // Place piece on player's side of the board
        if (isWhiteTurn && z >= 4)
            return false;
        if (!isWhiteTurn && z < 4)
            return false;

        return true;
    }

    protected abstract void PurchasePiece(int x, int z, bool isWhiteTurn);
}