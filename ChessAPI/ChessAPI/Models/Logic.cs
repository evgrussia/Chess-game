using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChessRules;
using System.Data.Entity;

namespace ChessAPI.Models
{
    public class Logic
    {
        private ChessModelDB db;
        public Logic()
        {
            db = new ChessModelDB();
        }
        public Game GetCurrentGame()
        {
            Game game;
            var currentGames = db.Games.Where(g => g.Status == "play");
            if (currentGames.Count() > 0)
                return currentGames.FirstOrDefault();
            else
                game = NewGame();
            return NewGame();
        }
        private Game NewGame()
        {
            Chess chess = new Chess();
            Game game = new Game();
            game.FEN = chess.fen;
            game.Status = "play";
            db.Games.Add(game);
            db.SaveChanges();
            return game;
        }
        

        public Game ResignGame(int id)
        {
            Game game = GetGame(id);

            if (game == null)
                return game;
            if (game.Status != "play")
                return game;
            game.Status = "done";
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return game;
        }

        public Game GetGame(int id)
        {
            Game game = db.Games.Find(id);
            return game;
        }

        public Game MakeMove(int id, string move)
        {
            Game game = GetGame(id);
            if (move == "newgame")
            {
                game = CloseGame(id);
                return CloseGame(id);
            }
                 
            if (game == null)
                return game;
            if (game.Status != "play")
                return game;
            Chess chess = new Chess(game.FEN);
            if (!chess.IsValidMove(move))
                return game;
            chess = chess.Move(move);
            game.FEN = chess.fen;
            if (chess.IsCheckMate || chess.IsStaleMate)
                game.Status = "done";
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return game;
        }

        private Game CloseGame(int id)
        {
            Game game = GetGame(id);
            if (game == null)
                return game;
            if (game.Status == "play")
                game.Status = "done";            
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return game;
        }
    }
}