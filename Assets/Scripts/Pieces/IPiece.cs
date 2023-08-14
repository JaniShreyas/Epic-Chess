using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EC
{
    public interface IPiece
    {
        // What does a chess piece need?
        // A position (will be overridden by the child classes)
        // A color (will be overridden by the child classes)
        // A type (this will be overridden by the child classes)
        // A reference to the game manager
        // A reference to the board
        // A reference to the square it is on
        // A reference to the square it is moving to

        // So, lets simulate a situation
        // A pawn is on the board at position (0, 1) and it needs to move to (0, 2)
        // The pawn will check if the square it is moving to is empty
        // If it is empty, it will move to that square
        // If it is not empty, it will check if the square it is moving to has an enemy piece
        // If it has an enemy piece, it will capture that piece
        // If it does not have an enemy piece, the pawn will not move
        // We can also try to create a list of possible moves for the pawn or pieces in this case

        public int Position { get; set; }

        public enum Color { White, Black };
        public Color ColorField { get; set; }

        //public enum Type { Pawn, Rook, Knight, Bishop, Queen, King };
        //public Type TypeField { get; set; }

        public Transform Graphics { get; }

        public GameManager GameManager { get; }

        public void Create(int position, IPiece.Color color);

        public void Move(int position);
    }

}