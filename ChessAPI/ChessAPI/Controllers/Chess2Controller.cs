
using System.Web.Http;
using System.Web.Http.Description;
using ChessAPI.Models;

namespace ChessAPI.Controllers
{
    public class Chess2Controller : ApiController
    {
        // GET: api/Chess
        public Game GetCurrentGames()
        {
            Logic logic = new Logic();
            return logic.GetCurrentGame();

        }
        public Game GetGameById(int id)
        {
            Logic logic = new Logic();
            return logic.GetGame(id);

        }
        public Game GetMoves(int id, string move)
        {
            Logic logic = new Logic();
            if (move == "resign")
                return logic.ResignGame(id);
            else
                return logic.MakeMove(id, move);
        }
    }
}