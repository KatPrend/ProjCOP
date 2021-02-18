using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public bool isWhite;
}
