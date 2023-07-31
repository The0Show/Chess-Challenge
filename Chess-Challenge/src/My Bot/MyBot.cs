using System;
using ChessChallenge.API;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Random rand = new();
        Move[] moves = board.GetLegalMoves();
        Move move = moves[rand.Next(moves.Length)];

        for (int i = 0; i < moves.Length; i++)
        {
            if (moves[i].IsCapture || MoveTest(board, moves[i], timer)) move = moves[i];
        }

        return move;
    }

    bool MoveTest(Board board, Move move, Timer timer)
    {
        if (timer.MillisecondsRemaining <= 10000) return true; // lmao

        board.MakeMove(move);

        bool isABadMove;
        isABadMove = board.IsInCheckmate();
        //if (!isABadMove) isABadMove = board.IsInCheck();
        if (!isABadMove) isABadMove = board.IsInsufficientMaterial();

        board.UndoMove(move);

        return isABadMove;
    }
}