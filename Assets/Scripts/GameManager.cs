using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace EC
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject square;
        [SerializeField] private GameObject boardPrefab;
        [SerializeField] private int gridSize = 8;

        [SerializeField] private Pawn pawnPrefab_w;
        [SerializeField] private King kingPrefab_w;
        [SerializeField] private Queen queenPrefab_w;
        [SerializeField] private Bishop bishopPrefab_w;
        [SerializeField] private Knight knightPrefab_w;
        [SerializeField] private Rook rookPrefab_w;

        [SerializeField] private Pawn pawnPrefab_b;
        [SerializeField] private King kingPrefab_b;
        [SerializeField] private Queen queenPrefab_b;
        [SerializeField] private Bishop bishopPrefab_b;
        [SerializeField] private Knight knightPrefab_b;
        [SerializeField] private Rook rookPrefab_b;

        //Create a 2d int array of size gridsize
        GameObject[] grid;

        Dictionary<char, Piece> pieces = new Dictionary<char, Piece>();

        private float squareLength;

        public GameObject[] Board
        {
            get => grid;
        }

        public float SquareLength
        {
            get => squareLength;
        }

        private void OnEnable()
        {
            squareLength = square.transform.localScale.x;
            grid = new GameObject[gridSize * gridSize];
            CreateSquareGrid();
            SetupPiecesDict();
        }


        private void Start()
        {
            Bishop bishop = Instantiate(bishopPrefab_w);
            bishop.Create(22, Piece.Color.Black);

            Fen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR");
        }

        private void CreateSquareGrid()
        {
            //Instantiate parent board object
            GameObject squaresParent = GameObject.Instantiate(boardPrefab, Vector3.zero, Quaternion.identity);

            //Initialize x and z axes for the positions in the grid by offsetting by 2.5
            for (int z = 0; z < gridSize; z++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    //Every block should be made squareLength units apart
                    Vector3 position = new Vector3(x * squareLength, 0, z * squareLength);
                    GameObject newSquare = GameObject.Instantiate(square, position, Quaternion.identity);
                    
                    int gridPosition = z * gridSize + x;
                    newSquare.GetComponent<Square>().Position = gridPosition;
                    grid[gridPosition] = newSquare;
                    newSquare.transform.parent = squaresParent.transform;
                }
            }
        }

        private void SetupPiecesDict()
        {
            pieces.Add('K', kingPrefab_w);
            pieces.Add('Q', queenPrefab_w);
            pieces.Add('B', bishopPrefab_w);
            pieces.Add('N', knightPrefab_w);
            pieces.Add('R', rookPrefab_w);
            pieces.Add('P', pawnPrefab_w);
            
            pieces.Add('k', kingPrefab_b);
            pieces.Add('q', queenPrefab_b);
            pieces.Add('b', bishopPrefab_b);
            pieces.Add('n', knightPrefab_b);
            pieces.Add('r', rookPrefab_b);
            pieces.Add('p', pawnPrefab_b);
        }

        Piece pieceToMove = null;

        public void PieceMovement()
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
            {
                Piece piece = hit.collider.GetComponent<Piece>();
                Square square = hit.collider.GetComponent<Square>();

                pieceToMove ??= piece;

                if(square != null)
                {
                    //Note: Later check if the square is a valid move and is empty
                    pieceToMove?.Move(square.Position);
                    pieceToMove = null;
                }
            }
        }
        
        public void Fen(string fenSequence)
        {
            int file = 0, rank = 7;
            foreach(char symbol in fenSequence)
            {
                if (symbol == '/')
                {
                    file = 0;
                    rank--;
                }
                else
                {
                    if (char.IsDigit(symbol))
                    {
                        file += (int)char.GetNumericValue(symbol);
                    }
                    else
                    {
                        Piece.Color pieceColor = char.IsUpper(symbol) ? Piece.Color.White : Piece.Color.Black;
                        Piece piece = pieces[symbol];
                        Piece pieceToInstantiate = Instantiate(piece);
                        pieceToInstantiate.Create(rank * gridSize + file, pieceColor);
                        file++;
                    }   
                }
            }
        }

    }
}