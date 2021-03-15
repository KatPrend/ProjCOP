using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class GameAI : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


public class GameAI 
{

    private Piece pieceToMove;
    public Piece[,] Pieces { set; get; }
    public bool[,] bishopallowedMoves { get; set; } 

    //create a valid moves array
    public List<bool> MoveArray;

    public int depth = 4;
    public int maxEval, minEval, eval;


	// int maxDepth = 4; //Alpha Beta looking 4 moves ahead

	public void AlphaBeta(int x,int z)
	{

		// Debug.Log("AB  " + x);
		// Debug.Log("AB  " + z);

		Moves();

	}

	public void alg()
	{

	}

	public int minimax(int positionX, int positionZ, int depth, int alpha, int beta, bool maxPlayer)
	{
		if(depth == 0)
			return 0;

		if(maxPlayer)
		{
			maxEval = -100000;

			foreach(bool child in MoveArray)
			{
				//child - get positon X and Z
				// eval = minimax(.PositionX, .PositionZ, alpha, beta, false);
				if(maxEval < eval)
				{
					maxEval = eval;
				}
				if(alpha < eval)
				{
					alpha = eval;
				}
				if(beta <= alpha)
					break;
			} //end of foreach
			return maxEval;

		}
		else
		{
			minEval = 100000;

			foreach(bool child in MoveArray)
			{
				//child - get positon X and Z
				// eval = minimax(.PositionX, .PositionZ, alpha, beta, false);
				if(minEval > eval)
				{
					minEval = eval;
				}
				if(beta > eval)
				{
					beta = eval;
				}
				if(beta >= alpha)
					break;
			} //end of foreach
			return minEval;


		}
	}



	public void Moves()
	{
		//put all of the valid moves in an array
		//where on the board that that certain piece can move, true or false

		//valid moves for Bishop
		// public bool[,] bishopallowedMoves
		// {
		// 	// leftwhite
		// 	// {
		// 	// 	bishopallowedMoves = Pieces[2,0].ArrayOfValidMove(); //white 

		// 	// }
		// }
		// allowedMoves.Add(Pieces[5,0].ArrayOfValidMove()); //white 
		// allowedMoves.Add(Pieces[2,7].ArrayOfValidMove()); //black
		// allowedMoves.Add(Pieces[5,7].ArrayOfValidMove()); //black 


	}

	
}