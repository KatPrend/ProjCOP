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
        if(BoardManager.Instance.Pieces[i, 4] == null && i < 8)
        {
            BoardManager.Instance.SpawnChessPiece(i, i, 4);
            i++;
        }
        else
            OnButtonPress();
    }
}