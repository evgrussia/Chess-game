using System;
using System.Collections.Generic;
namespace ChessRules
{
    public class Chess
    {        
        public string fen
        { get{return board.fen;} }
        public bool IsCheck { get; private set; }
        public bool IsCheckMate { get; private set; }
        public bool IsStaleMate { get; private set; }
        public bool IsDraw { get; private set; }
        public string NowColor { get; private set; }
        Board board;
        Moves moves;
        public Chess(string fen= "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {           
            board = new Board(fen);
            moves = new Moves(board);
            SetCheckFlags();
        }
        Chess(Board board)
        {
            this.board = board;
            moves = new Moves(board);
        }
        void SetCheckFlags()
        {
            NowColor = board.moveColor.ToString();
            IsCheck = board.IsCheck();
            IsCheckMate = false;
            IsStaleMate = false;
            IsDrawSet();
            foreach (string moves in YieldValidMoves())
                return;
            if (IsCheck)
                IsCheckMate = true;
            else
                IsStaleMate = true;
        }
        public bool IsDrawSet()
        {
            int countFigures = 0;
            IsDraw = false;
            foreach (Square square in Square.YieldBoardSquares())
            {
                if (countFigures > 2)
                    return false;
                Figure figure = board.GetFigureAt(square);
                if (figure != Figure.none)
                    countFigures++;
            }
            if (countFigures == 2)
            {
                IsDraw = true;
                return true;
            }                
            return false;
        }
        public bool IsValidMove(string move)
        {
            if (move.Length < 5) return false;
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return false;
            if (board.IsCheckAfter(fm))
                return false;
            return true;
        }
        public Chess Move(string move)
        {
            if (!IsValidMove(move))
            {                
                return this;
            }             
            FigureMoving fm = new FigureMoving(move);
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);           
            return nextChess;
        }
        public char GetFigureAt (int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = board.GetFigureAt(square);
            return figure == Figure.none ? '.' : (char)figure;           
        }
        public char GetFigureAt(string xy)
        {
            Square square = new Square(xy);
            Figure figure = board.GetFigureAt(square);
            return figure == Figure.none ? '.' : (char)figure;
        }
        public IEnumerable<string> YieldValidMoves()
        {
            foreach (FigureOnSquare fs in board.YieldMyFiguresOnSquares())
                foreach (Square to in Square.YieldBoardSquares())
                    foreach(Figure promotion in fs.figure.YieldPromotions(to))
                    {
                        FigureMoving fm = new FigureMoving(fs, to, promotion);
                        if(moves.CanMove(fm))
                            if(!board.IsCheckAfter(fm))
                            yield return fm.ToString();
                    }                                              
        }      
    }
}
