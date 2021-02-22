using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override bool ValidMove(int newX, int newZ)
    {
        // Check for diagonal movement
        int CompareX = 0, CompareZ = 0;

        // Return false for out-of-bounds
        if (newX < 0 || newX > 7)
            return false;
        if (newZ < 0 || newZ > 7)
            return false;

        // Find X comparison value
        CompareX = PositionX - newX;

        // Find Z comparison value
        CompareZ = PositionZ - newZ;

        // Check for valid diagonal movement
        if ((CompareX == CompareZ) || (CompareX == -CompareZ))
            return true;

        return false;
    }
}
