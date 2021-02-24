using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override bool ValidMove(int newX, int newZ)
    {
        // If  newX and newZ are off the board
        if (newX < 0 || newX > 7)
            return false;
        if (newZ < 0 || newZ > 7)
            return false;

        if (isWhite)
        {
            // Starting position: Move up 2
            if (PositionZ == 1)
                if (PositionX == newX && PositionZ + 2 == newZ)
                    return true;

            // Move up 1
            if (PositionX == newX && PositionZ + 1 == newZ)
                return true;
        }
        else
        {
            // Starting position: Move down 2
            if (PositionZ == 6)
                if (PositionX == newX && PositionZ - 2 == newZ)
                    return true;

            // Move down 1
            if (PositionX == newX && PositionZ - 1 == newZ)
                return true;
        }

        return false;
    }

    public override bool[,] ArrayOfValidMove()
    {
        bool[,] array = new bool[8,8];
        array[3,3] = true;

        return array;
    }
}
