using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public int PositionX { get; set; }
    public int PositionZ { get; set; }

    public bool isWhite;

    public void setPosition(int x, int z)
    {
        PositionX = x;
        PositionZ = z;
    }

    public virtual bool PossibleMove(int x, int z)
    {
        return true; //Just for implementation right now
    }
}
