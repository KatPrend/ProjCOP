using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }

    public IHandleInput InputAction;

    private bool[,] allowedRelativeMoves{ set; get; }

    public Piece[,] Pieces { set; get; }
    private Piece selectedPiece;

    //Public Camera Objects
    public Camera WhiteCamera;
    public Camera BlackCamera;

    private const float SQUARE_SIZE  = 1.0F; //Square is 1 meter by 1 meter
    private const float SQUARE_OFFSET  = 0.5F; //Offset to center a piece

    public int selectionX = -1;
    public int selectionZ = -1;

    public int emptySelectionX = -1;
    public int emptySelectionZ = -1;

    public List<GameObject> chessPiecesPrefabs;

    private List<GameObject> activeChessPieces;

    public bool isWhiteTurn = true;

    private void Start()
    {
        Instance = this;

        if (InputAction == null)
            InputAction = new HandleInput();

        //Spawn all pieces
        SpawnAllChessPieces();
        WhiteCamera.enabled = true;
        BlackCamera.enabled = false;
    }

    private void Update()
    {
        UpdateSelection();
        DrawChessBoard();

        if(InputAction.GetMouseButtonDown(0)) //If left click
        {
            if(selectionX >= 0 && selectionZ >= 0) //If clicking on board
            {
                if (selectedPiece == null) //If clicking on a piece
                {
                    BoardHighlights.Instance.HideHighlights();
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
        {
            emptySelectionX = x;
            emptySelectionZ = z;
            bool[,] array = new bool[8,8];
            array[x,z] = true;
            BoardHighlights.Instance.HighlightAllowedMoves(array);
            return;
        }
        if (Pieces[x,z].isWhite != isWhiteTurn)
        {
            emptySelectionX = -1;
            emptySelectionZ = -1;
            return;
        }            

        allowedRelativeMoves = Pieces[x, z].ArrayOfValidMove();
        selectedPiece = Pieces[x,z];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedRelativeMoves);
        emptySelectionX = -1;
        emptySelectionZ = -1;
    }

    private void TakeTurn(int x, int z)
    {
        if (allowedRelativeMoves[x, z])
            MoveAPiece(x, z);
        // If move is not allowed, unselect piece
        else
        {
            selectedPiece = null;
            BoardHighlights.Instance.HideHighlights();
            return;
        }

        // Update player's coins
        Coin.AddCoin(isWhiteTurn);

        // Pass turn and swap cameras
        isWhiteTurn = !isWhiteTurn;
        WhiteCamera.enabled = !WhiteCamera.enabled;
        BlackCamera.enabled = !BlackCamera.enabled;
    }

    private void MoveAPiece(int x, int z)
    {
        Vector3 newSquare = GetSquareCenter(x, z);
        Piece otherPiece = Pieces[x,z];

        if (otherPiece != null && otherPiece.isWhite != isWhiteTurn)
            CapturePiece(otherPiece);

        Pieces[selectedPiece.PositionX, selectedPiece.PositionZ] = null;

        selectedPiece.transform.position = newSquare;
        selectedPiece.SetPosition((int)newSquare.x, (int)newSquare.z);
        Pieces[x, z] = selectedPiece;

        //And remove the board highlight
        BoardHighlights.Instance.HideHighlights();
        selectedPiece = null;
    }

    private void CapturePiece(Piece capturedPiece)
    {
        activeChessPieces.Remove(capturedPiece.gameObject);

        Destroy(capturedPiece.gameObject);

        if (capturedPiece.GetType() == typeof(King))
        {
            //End the game
            Application.Quit();
        }

        // Add additional coin for capture
        Coin.AddCoin(isWhiteTurn);
    }

    private void UpdateSelection()
    {
        //If there is no camera
        if(!Camera.main)
            return;

        RaycastHit hit;

        //If mouse is over the board update selection variables to current position
        if(Physics.Raycast(Camera.main.ScreenPointToRay(InputAction.MousePosition()), out hit, 25.0f, LayerMask.GetMask("Chess Plane")))
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
    public void SpawnChessPiece(int index, int x, int z)
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
