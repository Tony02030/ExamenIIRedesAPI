using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using ExamenIIRedesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenIIRedesAPI.Controllers
{
    [Produces("application/json")]
    [Route("/game/")]
    public class SetUpController : ControllerBase
    {
        ///<summary>
        ///list all games
        ///</summary>
        ///<remarks>List all games. Query strings can be used to indicate filters to be applied on the server side.</remarks>
        /// <param name="filter">game property to be used as filter</param>
        /// <param name="filterValue">property value to match with. When empty should return an empty array</param>
        /// <response code="200">returns all games</response>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<GameGet>))]
        public List<GameGet> Get(string? filter, string? filterValue)
        {
            if (filter == "owner")
            {
                List<GameGet> games = new List<GameGet>();

                for (int i = 0; i < Util.Utility.gameList.Count(); i++)
                {
                    if (Util.Utility.gameList[i].Owner == filterValue)
                    {
                        GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
                        games.Add(game);
                    }


                }
                return games;


            }
            else if (filter == "gameId")
            {
                List<GameGet> games = new List<GameGet>();

                for (int i = 0; i < Util.Utility.gameList.Count(); i++)
                {
                    if (Util.Utility.gameList[i].GameId == filterValue)
                    {
                        GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
                        games.Add(game);
                    }


                }
                return games;
            }
            else if (filter == "status")
            {
                List<GameGet> games = new List<GameGet>();

                for (int i = 0; i < Util.Utility.gameList.Count(); i++)
                {
                    if (Util.Utility.gameList[i].Status == filterValue)
                    {
                        GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
                        games.Add(game);
                    }


                }
                return games;

            }
            else
            {
                List<GameGet> games = new List<GameGet>();

                for (int i = 0; i < Util.Utility.gameList.Count(); i++)
                {

                    GameGet game = new GameGet(Util.Utility.gameList[i].GameId, Util.Utility.gameList[i].Name);
                    games.Add(game);

                }
                return games;
            }

        }

        ///<summary>
        ///Create a new game
        ///</summary>
        ///<remarks>Create a new game. A header 'name' should be used to indicate the game owner and identity to use forward</remarks>
        /// <param name="name">Owner's identity</param>
        ///<param name="game">New Game info</param>
        ////// <response code="200">returns all games</response>
        [HttpPost]
        [Route("[action]")]
        public IActionResult create([Required][FromHeader] string name, [FromBody][Required] GameBase game)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, new ErrorMsg("missing game owner or password parameters"));

            }

            if (name == null || name == "" || game.Name == null || game.Password == null || game.Name == "")

            {

                return StatusCode(406, new ErrorMsg("missing name header or game name parameters"));

            }

            else

            {

                Game game1 = new Game(game.Name, name, game.Password);

                Util.Utility.gameList.Add(game1);

                return StatusCode(200, game1);

            }





        }
    }
}
