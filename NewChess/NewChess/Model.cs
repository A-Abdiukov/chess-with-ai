﻿using System;
using System.Drawing;

namespace Chess
{

    public class Model : CommandHandler<ModelCommand>
    {
        private const int BoardRows = 8;
        private const int BoardColumns = 8;
        private CommandHandler<ViewCommand> commandHandler;
        private Square[,] board = new Square[BoardRows, BoardColumns];

        public Model(CommandHandler<ViewCommand> commandHandler)
        {
            this.commandHandler = commandHandler;
            for (int i = 0; i < BoardRows; i++)
            {
                for (int j = 0; j < BoardColumns; j++)
                {
                    board[i, j] = new Square();
                }
            }
        }

        public void Handle(ModelCommand command) { command.execute(this); }

        public void Start()
        {
            InitPawns(6, Team.White);
            InitBackRow(7, Team.White);
            InitPawns(1, Team.Black);
            InitBackRow(0, Team.Black);
            UpdateAll();
        }

        private void UpdateAll()
        {
            Console.WriteLine("Updating all;");
            for (int i = 0; i < BoardRows; i++)
            {
                for (int j = 0; j < BoardColumns; j++)
                {
                    Update(new Point(i, j));
                }
            }
        }


        private void InitBackRow(int row, Team team)
        {
            //changed the row a bit, now it works
            Place(row, 0, new Rook(team));
            Place(row, 1, new Knight(team));
            Place(row, 2, new Bishop(team));
            Place(row, 3, new Queen(team));
            Place(row, 4, new King(team));
            Place(row, 5, new Bishop(team));
            Place(row, 6, new Knight(team));
            Place(row, 7, new Rook(team));
        }

        private void InitPawns(int row, Team team)
        {
            for (int i = 0; i < 8; i++)
            {
                Place(row, i, new Pawn(team));
            }
        }

        private void Place(int col, int row, Piece piece)
        {
            board[row, col].piece = piece;
            Update(new Point(row, col));
        }

        private void Update(Point coord)
        {
            Piece piece = board[coord.X, coord.Y].piece;
            commandHandler.Handle(new DrawSquareCommand(coord, piece));
        }
    }
}
