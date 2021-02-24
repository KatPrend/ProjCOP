using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override bool ValidMove(int newX, int newZ)
    {
      // If  newX and newZ are off the board
      if (newX < 0 || newX > 7)
        return false;
      if (newZ < 0 || newZ > 7)
        return false;

      // left 1, up 2
      if (PositionX - 1 == newX && PositionZ + 2 == newZ)
        return true;

      // right 1, up 2
      if (PositionX + 1 == newX && PositionZ + 2 == newZ)
        return true;

      // right 2, up 1
      if (PositionX + 2 == newX && PositionZ + 1 == newZ)
        return true;

      // right 2, down 1
      if (PositionX + 2 == newX && PositionZ - 1 == newZ)
        return true;

      // right 1, down 2
      if (PositionX + 1 == newX && PositionZ - 2 == newZ)
        return true;

      // left 1, down 2
      if (PositionX - 1 == newX && PositionZ - 2 == newZ)
        return true;

      // left 2, down 1
      if (PositionX - 2 == newX && PositionZ - 1 == newZ)
        return true;

      // left 2, up 1
      if (PositionX - 2 == newX && PositionZ + 1 == newZ)
        return true;

      return false;
    }

    public override bool[,] ArrayOfValidMove()
    {
        return new bool[8,8];
    }
}
