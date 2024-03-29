﻿using System.Collections.Generic;
using System.Drawing;

namespace ChessGame.Model;
public enum Team { White, Black }

// TODO: Team should have the following properties
// KingLocation

public abstract class Piece
{
    public Team Team;
    public Image Image;

    protected Piece(Team team, string file)
    {
        Team = team;
        Image = Image.FromFile($"Asset/{file}_{team}.png");
    }

    public abstract IList<Point?> GetMoves(int x, int y);

    /// <summary>
    /// Checks whether the coordinate on the board is empty
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="piece">Piece being checked</param>
    /// <returns>Returns true if piece is empty. Returns false if piece is not empty and is enemy team. Returns null if it is not empty and it is your team.</returns>
    public bool? IsEmpty(int x, int y, Piece piece)
    {
        if (x is < 0 or > 7 || y is < 0 or > 7)
            return null;

        if (Coordinates.Board[x, y].Piece is null)
            return true;

        if (Coordinates.Board[x, y].Piece.Team != piece.Team)
            return false;

        return null;
    }

    public bool IsFriendlyRook(int x, int y, Piece piece)
    {
        if (x is < 0 or > 7 || y is < 0 or > 7)
            return false;

        if (Coordinates.Board[x, y].Piece is null)
            return false;

        return Coordinates.Board[x, y].Piece is Rook
               && Coordinates.Board[x, y].Piece.Team == piece.Team;
    }
}