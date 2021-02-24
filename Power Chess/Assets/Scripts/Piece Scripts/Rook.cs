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

    public override bool[,] ArrayOfValidMove()
    {
        bool[,] array = new bool[8,8];

        Piece otherPiece;

        int i = 0;

        //Right
        for(i = PositionX + 1; i < 8; i++)
        {
            otherPiece = BoardManager.Instance.Pieces[i, PositionZ];
            if (otherPiece == null)
                array[i, PositionZ] = true;
            else   
            {
                if(otherPiece.isWhite != isWhite)
                    array[i, PositionZ] = true;
                break;
            }
        }

        //Left
        for(i = PositionX - 1; i >= 0; i--)
        {
            otherPiece = BoardManager.Instance.Pieces[i, PositionZ];
            if (otherPiece == null)
                array[i, PositionZ] = true;
            else   
            {
                if(otherPiece.isWhite != isWhite)
                    array[i, PositionZ] = true;
                break;
            }
        }

        //Up
        for(i = PositionZ + 1; i < 8; i++)
        {
            otherPiece = BoardManager.Instance.Pieces[PositionX, i];
            if (otherPiece == null)
                array[PositionX, i] = true;
            else   
            {
                if(otherPiece.isWhite != isWhite)
                    array[PositionX, i] = true;
                break;
            }
        }

         //Up
        for(i = PositionZ - 1; i >= 0; i--)
        {
            otherPiece = BoardManager.Instance.Pieces[PositionX, i];
            if (otherPiece == null)
                array[PositionX, i] = true;
            else   
            {
                if(otherPiece.isWhite != isWhite)
                    array[PositionX, i] = true;
                break;
            }
        }

        return array;
    }
}
