using System;
using System.Text;
using ChessRules;
namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {           
            Client client = new Client();
            string fen = client.GetFenFromServer();
            Console.WriteLine(client.GameID);          
            Chess chess = new Chess(fen);
            while (true)
            {                
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                foreach (string moves in chess.YieldValidMoves())
                    Console.WriteLine(moves);               
                string move = Console.ReadLine();
                if(move =="") break;
                if (move == "s")
                {
                    fen = client.GetFenFromServer();
                    chess = new Chess(fen);
                    continue;
                }                  
                if (!chess.IsValidMove(move))
                    continue;
                fen = client.SendMove(move);
                chess = new Chess(fen);
            }
        }
        static string ChessToAscii(Chess chess)
        {            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" +--------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append("|");
                for (int x = 0; x < 8; x++)
                    sb.Append(chess.GetFigureAt(x, y) + " ");
                sb.AppendLine();
            }
            sb.AppendLine(" +---------------+");
            sb.AppendLine("  a b c d e f g h  ");
            if (chess.IsCheck) sb.AppendLine("IS CHECK");
            if (chess.IsCheckMate) sb.AppendLine("IS CHECKMATE");
            if (chess.IsStaleMate) sb.AppendLine("IS STALEMATE");
            if (chess.IsDraw) sb.AppendLine("IS DRAW");
            sb.AppendLine($"Now Color IS:"+chess.NowColor);
            return sb.ToString();
        }
        static void Print (string text)
        {
            ConsoleColor old = Console.ForegroundColor;
            foreach (char x in text)
            {              
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = old;
        }
    }
}
