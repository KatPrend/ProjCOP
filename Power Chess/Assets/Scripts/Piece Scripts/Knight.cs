using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    // Should I account for a position that is occupied, or leave that to
    // BoardManager?

    // Knight is as (PositionX, PositionZ) and would like to move to (x,z)
    public override bool ValidMove(int x, int z)
    {
      // If x and z are off the board
      if (x < 0 || x > 7)
        return false;
      if (z < 0 || z > 7)
        return false;

      // left 1, up 2
      if (PositionX - 1 == x && PositionZ + 2 == z)
        return true;

      // right 1, up 2
      if (PositionX + 1 == x && PositionZ + 2 == z)
        return true;

      // right 2, up 1
      if (PositionX + 2 == x && PositionZ + 1 == z)
        return true;

      // right 2, down 1
      if (PositionX + 2 == x && PositionZ - 1 == z)
        return true;

      // right 1, down 2
      if (PositionX + 1 == x && PositionZ - 2 == z)
        return true;

      // left 1, down 2
      if (PositionX - 1 == x && PositionZ - 2 == z)
        return true;

      // left 2, down 1
      if (PositionX - 2 == x && PositionZ - 1 == z)
        return true;

      // left 2, up 1
      if (PositionX - 2 == x && PositionZ + 1 == z)
        return true;

      return false;
    }
}
