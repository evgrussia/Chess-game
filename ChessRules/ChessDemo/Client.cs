using System;
using System.Net;

namespace ChessDemo
{
    class Client
    {
        public const string address = "http://95.31.2.137/api/chess2/";
        WebClient web;
        public int GameID { get; protected set; }
        public Client()
        {
            web = new WebClient();
        }
        public string GetFenFromServer()
        {
            string json = web.DownloadString(address);
            GameID = GetIDFromFSON(json);
            string fen = GetFenFromJSON(json);
            return fen;
        }
        public string SendMove(string move)
        {
            string json = web.DownloadString(address+GameID.ToString()+"/"+move);
            string fen = GetFenFromJSON(json);
            return fen;
        }
        int GetIDFromFSON(string json)
        {
            int x = json.IndexOf("\"ID\"");
            int y = json.IndexOf(":", x) + 1;
            int z = json.IndexOf(",", y);
            string id = json.Substring(y, z - y);
            return Convert.ToInt32(id);
        }
        string GetFenFromJSON(string json)
        {
            int x = json.IndexOf("\"FEN\"");
            int y = json.IndexOf(":\"", x) + 2;
            int z = json.IndexOf("\"", y);
            string fen = json.Substring(y, z - y);
            return fen;
        }
    }
}
