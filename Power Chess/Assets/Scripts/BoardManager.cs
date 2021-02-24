using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }

    private bool[,] allowedRelativeMoves{ set; get; }

    public Piece[,] Pieces { set; get; }
    private Piece selectedPiece;

    private const float SQUARE_SIZE  = 1.0F; //Square is 1 meter by 1 meter
    private const float SQUARE_OFFSET  = 0.5F; //Offset to center a piece

    private int selectionX = -1;
    private int selectionZ = -1;

    public List<GameObject> chessPiecesPrefabs;

    private List<GameObject> activeChessPieces;

    public bool isWhiteTurn = true;

    private void Start()
    {
        Instance = this;

        //Spawn all pieces
        SpawnAllChessPieces();
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();

        if(Input.GetMouseButtonDown(0)) //If left click
        {
            if(selectionX >= 0 && selectionZ >= 0) //If clicking on board
            {
                if (selectedPiece == null) //If clicking on a piece
                {
                    SelectPiece(selectionX, selectionZ);
                }
                else
                {
                    TakeTurn(selectionX, selectionZ);
                }
            }
        }
    }

    //Given cursor location pick piece at that location if it exists
    private void SelectPiece(int x, int z)
    {
        if (Pieces[x,z] == null)
            return;
        if (Pieces[x,z].isWhite != isWhiteTurn)
            return;

        allowedRelativeMoves = Pieces[x, z].ArrayOfValidMove(); 
        selectedPiece = Pieces[x,z];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedRelativeMoves);
    }

    private void TakeTurn(int x, int z)
    {
        MoveAPiece(x, z);

        // Update player's coins
        Coin.AddCoin(isWhiteTurn);

        isWhiteTurn = !isWhiteTurn;
    }

    private void MoveAPiece(int x, int z)
    {
        Vector3 newSquare = GetSquareCenter(x, z);

        //If a move is valid move the piece
        if (selectedPiece.ValidMove(x,z))
        {
            Piece otherPiece = Pieces[x,z];

            if(otherPiece != null && otherPiece.isWhite != isWhiteTurn)
            {
                //Capture a piece

                if(otherPiece.GetType() == typeof(King))
                {
                    //End the game
                    return;
                }

                activeChessPieces.Remove(otherPiece.gameObject);
                Destroy (otherPiece.gameObject);
            }

            Pieces[selectedPiece.PositionX, selectedPiece.PositionZ] = null;

            selectedPiece.transform.position = newSquare;
            selectedPiece.SetPosition((int)newSquare.x, (int)newSquare.z);
            Pieces[x, z] = selectedPiece;
        }

        //And remove the board highlight
        BoardHighlights.Instance.HideHighlights();
        selectedPiece = null;
    }

    private void UpdateSelection()
    {
        //If there is no camera
        if(!Camera.main)
            return;

        RaycastHit hit;

        //If mouse is over the board update selection variables to current position
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Chess Plane")))
        {
            Debug.Log(hit.point); //Prints position to console for testing

            //Store X and Z values
            selectionX = (int) hit.point.x;
            selectionZ = (int) hit.point.z;
        }

        //If not over the board default to -1, -1
        else
        {
            selectionX = -1;
            selectionZ = -1;
        }
    }

    //Given an index in the ChessPiecesPrefab list spawn that pieces at position
    private void SpawnChessPiece(int index, int x, int z)
    {
        GameObject go = Instantiate(chessPiecesPrefabs[index], GetSquareCenter(x,z), FixRotation(index)) as GameObject; //Create it as a game object
        go.transform.SetParent(transform);

        Pieces[x,z] = go.GetComponent<Piece>();
        Pieces[x,z].SetPosition(x,z);
        activeChessPieces.Add(go);
    }

    //Given an index in the chessPiecesPrefab return an orientation for that piece
    private Quaternion FixRotation(int index)
    {
        Quaternion orientation;
        if (index < 6)
            orientation = Quaternion.Euler(-90, -90, 0);
        else
            orientation = Quaternion.Euler(-90, 90, 0);
        return orientation;
    }

    private void SpawnAllChessPieces()
    {
        activeChessPieces = new List<GameObject>();
        Pieces = new Piece[8,8];

        //Spawn Kings
        SpawnChessPiece(0, 4, 0);
        SpawnChessPiece(6, 4, 7);

        //Spawns Queen
        SpawnChessPiece(1, 3, 0);
        SpawnChessPiece(7, 3, 7);

        //Spawn Rooks
        SpawnChessPiece(2, 0, 0);
        SpawnChessPiece(2, 7, 0);
        SpawnChessPiece(8, 0, 7);
        SpawnChessPiece(8, 7, 7);

        //Spawn Bishops
        SpawnChessPiece(3, 2, 0);
        SpawnChessPiece(3, 5, 0);
        SpawnChessPiece(9, 2, 7);
        SpawnChessPiece(9, 5, 7);

        //Spawn Knights
        SpawnChessPiece(4, 1, 0);
        SpawnChessPiece(4, 6, 0);
        SpawnChessPiece(10, 1, 7);
        SpawnChessPiece(10, 6, 7);

        //Spawn Pawns
        for(int i = 0; i < 8; i++)
        {
            SpawnChessPiece(5, i, 1);
            SpawnChessPiece(11, i, 6);
        }
    }

    //Takes in squares position and returns a Vector3 with that squares center
    private Vector3 GetSquareCenter(int x, int z)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (SQUARE_SIZE * x) + SQUARE_OFFSET;
        origin.y = 0;
        origin.z += (SQUARE_SIZE * z) + SQUARE_OFFSET;
        return origin;
    }

    //Draws out a grid on chess board
    //Make sure you have gizmos on to see it
    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        //Loops thru 8 x 8 and draws a line
        for(int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            for(int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        //Draw the and X over the square the mouse hovers over if its hovering over the board
        if (selectionX >= 0 && selectionZ >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionZ + Vector3.right * selectionX, Vector3.forward * (selectionZ + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * (selectionZ + 1) + Vector3.right * selectionX, Vector3.forward * selectionZ + Vector3.right * (selectionX + 1));
        }
    }
}
