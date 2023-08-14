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

        //Create a 2d int array of size gridsize
        GameObject[] grid;

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
        }

        private void Start()
        {
            Bishop bishop = Instantiate(bishopPrefab_w);
            bishop.Create(22, IPiece.Color.Black);
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

        IPiece pieceToMove = null;

        public void PieceMovement()
        {
            print("Hi there");
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit))
            {
                IPiece piece = hit.collider.GetComponent<IPiece>();
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

    }
}