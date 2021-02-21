using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override bool ValidMove(int newX, int newZ)
    {
      if (newX < 0 || newX > 7)
        return false;
      if (newZ < 0 || newZ > 7)
        return false;

      // move up 1
      if (PositionX == newX && PositionZ + 1 == newZ)
          return true;
      // move down 1
      if (PositionX == newX && PositionZ - 1 == newZ)
          return true;
      // move left one
      if (PositionX == newX - 1 && PositionZ == newZ)
          return true;
      // move right one
      if (PositionX == newX + 1 && PositionZ == newZ)
          return true;
      // move diagonally upwards to the left
      if (PositionX == newX - 1 && PositionZ == newZ + 1)
          return true;
      // move diagonally upwards to the right
      if (PositionX == newX + 1 && PositionZ == newZ + 1)
          return true;
      // move diagonally downards torwards the left
      if (PositionX == newX - 1 && PositionZ == newZ - 1)
          return true;
      // move diagonally downwards torwards the right
      if (PositionX == newX + 1 && PositionZ == newZ - 1)
          return true;

      // if they wanted to be slick and move a king where no king shall go.
      else
        return false;
    }
}
