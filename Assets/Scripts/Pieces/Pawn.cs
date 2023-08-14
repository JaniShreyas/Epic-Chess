using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace EC
{
    public class Pawn : MonoBehaviour, IPiece
    {
        private int position;
        private IPiece.Color color;

        private Transform graphics;
        private GameManager gameManager;

        public GameManager GameManager
        {
            get => gameManager;
        }

        public int Position
        {
            get => position;
            set => position = value;
        }

        public IPiece.Color ColorField
        {
            get => color;
            set => color = value;
        }

        public Transform Graphics 
        {
            get => graphics;
        }

        private void Awake()
        {
            graphics = transform.GetChild(0);   
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }


        public void Create(int position, IPiece.Color color)
        {
            Position = position;
            ColorField = color;

            Move(position);
        }

        public void Move(int position)
        {
            float offset = GameManager.SquareLength / 2;
            Vector3 targetSquare = GameManager.Board[position].transform.localPosition;

            transform.position = new Vector3(targetSquare.x + (offset - 0.25f), 0f, targetSquare.z - (offset - 0.25f));
            Position = position;
        }
    }
}