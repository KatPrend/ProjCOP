using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
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

        int compareX = PositionX - newX;
        int compareZ = PositionZ - newZ;

        // Check for diagonal movement
        if ((compareX == compareZ) || (compareX == -compareZ))
            return true;

        return false;
    }

    public override bool[,] ArrayOfValidMove()
    {
      bool [,] array = new bool[8,8];
      for(int i = 0; i < 8; i++)
      {
          for(int j = 0; j < 8; j++)
          {
              QueenMove(i, j, ref array);
          }
      }
      return array;
    }

    //Given an x, z, and array of booleans check to if that move is possible
    public void QueenMove(int x, int z, ref bool[,] array)
    {
        Piece otherPiece;
        if (ValidMove(x, z))
        {
            otherPiece = BoardManager.Instance.Pieces[x,z];
            if(otherPiece == null)
            {
                array[x, z] = true;
            }
            else if(isWhite != otherPiece.isWhite)
            {
                array[x, z] = true;
            }
        }
    }
}
