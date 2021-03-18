using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public int opponentX = 0, opponentZ = 0;

    public List<GameObject> chessPiecesPrefabs;

    private List<GameObject> activeChessPieces;

    public bool isWhiteTurn = true;

    public bool AIturn = false;
    GameAI ai = new GameAI();

    public Text gameOverText;
    public bool isGameActive;
    public Button restartButton;

    
    public AudioSource noise1;
    public AudioSource noise2;
    public AudioSource noise3;

    private void Start()
    {
        noise1.Play();
        isGameActive = true;
        Instance = this;

        if (InputAction == null)
            InputAction = new HandleInput();

        //gameOverText.gameObject.SetActive(false);

        //Spawn all pieces
        StartingBoard();
        WhiteCamera.enabled = true;
        BlackCamera.enabled = false;
    }

    private void Update()
    {
        if(isGameActive)
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

                        opponentZ = selectionZ;
                        opponentX = selectionX;
                        AIturn = true;
                    }
                }
            }
        }

        if(AIturn && !isWhiteTurn)
        {
            if (selectedPiece == null) 
            {
                // SelectPiece(7, 6);//start post
            }
            else
            {
                // TakeTurn(7,5);//end pos move
            }

            // ai.AlphaBeta(opponentX, opponentZ); //send latest opponet moves
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

        // Check for extra white turns
        if (CoinFlip.extraWhiteTurn != 0)
        {
            CoinFlip.extraWhiteTurn--;
        }

        // Check for extra black turns
        else if (CoinFlip.extraBlackTurn != 0)
        {
            CoinFlip.extraBlackTurn--;
        }

        // Pass turn and swap cameras
        else
        {
            isWhiteTurn = !isWhiteTurn;
            WhiteCamera.enabled = !WhiteCamera.enabled;
            BlackCamera.enabled = !BlackCamera.enabled;
        }
        
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
        noise2.Play();
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
            //End the game
            GameOver();

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
        noise2.Play();
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

    private void StartingBoard()
    {
        activeChessPieces = new List<GameObject>();
        Pieces = new Piece[8,8];

        //Spawn Kings
        SpawnChessPiece(0, 4, 0);
        SpawnChessPiece(6, 4, 7);

        Coin.WhiteCoins = 3;
        Coin.BlackCoins = 3;
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

    public void GameOver() //When called shows game over text and sets gameActive to false
    {
        noise1.Stop();
        noise3.Play();
        if(isWhiteTurn)
        {
            gameOverText.text = "Game Over White Won";
        }
        else
        {
            gameOverText.text = "Game Over Black Won";
        }
        
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    private void ClearBoard() //When called Clears the board of pieces
    {
        for(int x = 0; x < 8; x++)
        {
            for(int z = 0; z < 8; z++)
            {
                if (Pieces[x,z] != null)
                    CapturePiece(Pieces[x,z]);
            }
        }
    }

    public void RestartGame() //Restarts the game
    {
        ClearBoard();
        isGameActive = true;
        StartingBoard();
        
        if(noise3.isPlaying)
            noise3.Stop();
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        noise1.Play();
    }
}
