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

    int i = 0;
    public void SpawnAPiece()
    {
        int x = BoardManager.Instance.emptySelectionX;
        int z = BoardManager.Instance.emptySelectionZ;
        if(x > -1 && z > -1 && BoardManager.Instance.Pieces[x,z] == null)
        {
            BoardManager.Instance.SpawnChessPiece(i%12, x, z);
            i++;
        }
        else
            OnButtonPress();
    }
}