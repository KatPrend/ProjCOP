using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
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

    int i;
    public void SpawnAPiece()
    {
        int x = BoardManager.Instance.emptySelectionX;
        int z = BoardManager.Instance.emptySelectionZ;
        if(BoardManager.Instance.Pieces[x,z] == null && i < 8 && x > -1 && z > -1)
        {
            BoardManager.Instance.SpawnChessPiece(i, x, z);
            i++;
            x = -1;
            z = -1;
        }
        else
            OnButtonPress();
    }
}