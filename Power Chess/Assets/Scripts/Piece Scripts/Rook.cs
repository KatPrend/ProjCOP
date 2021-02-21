using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override bool ValidMove(int newX, int newZ)
    {
        // If  newX and newZ are off the board
        if (newX < 0 || newX > 7)
            return false;
        if (newZ < 0 || newZ > 7)
            return false;
        
        // If rank stays the same
        if (PositionX == newX)
            return true;
        
        // If file stays the same
        if (PositionZ == newZ)
            return true;

        // Otherwise it's an invalid move
        else
            return false;
    }
}
