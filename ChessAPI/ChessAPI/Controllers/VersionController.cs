
using System.Web.Http;
namespace ChessAPI.Controllers
{
    public class VersionController : ApiController
    {
        public string GetVersion()
        {
            return "Version:1.0";
        }
    }
}
