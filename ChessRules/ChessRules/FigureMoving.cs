using System;
namespace ChessRules
{
    class FigureMoving
    {
        public Figure figure { get; private set; }
        public Square from { get; private set; }
        public Square to { get; private set; }
        public Figure promotion { get; private set;}
        public static FigureMoving none = new FigureMoving();
        FigureMoving()
        {
            figure = Figure.none;
            from = Square.none;
            to = Square.none;
            promotion = Figure.none;
        }
        public FigureMoving(FigureOnSquare fs, Square to, Figure promotion=Figure.none)
        {
            this.figure = fs.figure;
            this.from = fs.square;
            this.to = to;
            this.promotion = promotion;
            CheckPromotion();          
        }
        public FigureMoving(string move)//pe2e4
        {
            this.figure = (Figure)move[0];
            this.from = new Square(move.Substring(1, 2));
            this.to = new Square(move.Substring(3, 2));
            if (move.Length == 6)
                this.promotion = (Figure)move[5];
            else
                this.promotion = Figure.none;
            CheckPromotion();
        }
        private void CheckPromotion()
        {
            if (promotion != Figure.none) return;
           
            if (figure == Figure.whitePawn && to.y == 7)
                promotion = Figure.whiteQueen;
              
            if (figure == Figure.blackPawn && to.y == 0)
                promotion = Figure.blackQueen;            
        }
        public override string ToString()
        {
            return ((char)figure).ToString() + from.Name + to.Name+(promotion==Figure.none ? "":((char)promotion).ToString());
        }
        public int DeltaX { get { return to.x - from.x; } }
        public int DeltaY { get { return to.y - from.y; } }
        public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
        public int AbsDeltaY { get { return Math.Abs(DeltaY); } }
        public int SignX { get { return Math.Sign(DeltaX); } }
        public int SignY { get { return Math.Sign(DeltaY); } }
        public Figure PlacedFigure { get { return promotion == Figure.none ? figure : promotion; } }
    }
}
