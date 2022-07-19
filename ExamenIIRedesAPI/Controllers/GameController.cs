﻿using Microsoft.AspNetCore.Mvc;
using ExamenIIRedesAPI.Models;
using System.Security.Cryptography;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExamenIIRedesAPI.Controllers
{
    [Produces("application/json")]
    [Route("/game/")]

    public class GameController : ControllerBase

    {

        // GET: api/<GameController>
        [HttpGet]
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

        // GET api/<GameController>/5
        [HttpGet("{gameId}")]
        public Game Get(string gameId, [FromHeader] string name, [FromHeader] string password)
        {
            Game game = new Game();

            for (int i = 0; i < Util.Utility.gameList.Count(); i++)
            {
                game = Util.Utility.gameList[i];
                if (game.GameId.Equals(gameId) && game.Name.Equals(name) && game.Password.Equals(password))
                {
                    return game;
                }
                else
                {
                    return game;
                }
            }

            return game;

        }

        // POST /game/create       
       [HttpPost]
       [Route("[action]")]
        public ActionResult create(string? owner, [FromBody] GameBase game)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, "missing game name or password parameters");

            }
                
            if (owner == null || owner == "" || game.Name == null || game.Password == null || game.Name == "" || game.Password == "")
                
            {
                    
                return StatusCode(406, "missing name header or game name parameters");
                
            }
                
            else
                
            {
                    
                Game game1 = new Game(game.Name,owner, game.Password);
                    
                Util.Utility.gameList.Add(game1);
                    
                return Ok();
                
            }
                

            
            
           
        }

        

        // PUT api/<GameController>/5
        [HttpPut]
        [Route("{gameId}/[action]")]
        public ActionResult join(string gameId, [FromHeader] string name, [FromHeader] string password)
        {
            for (int i = 0; i < Util.Utility.gameList.Count(); i++)
            {
                if (Util.Utility.gameList[i].GameId.Equals(gameId))
                {
                    Game game = Util.Utility.gameList[i];

                    if (!ModelState.IsValid || !Util.Utility.existGameId(gameId))
                    {
                        return StatusCode(404, "Invalid Game's id");
                    }

                    else if (!Util.Utility.existPlayer(gameId, name))//afinar esta condición
                    {
                        return StatusCode(403, "You are not part of the players list");
                    }
                    else if (!Util.Utility.password(password))
                    {
                        return StatusCode(401, "Unathorized");
                    }

                    else if (game.Status.Equals("started") || game.Players.Count() >= 10)
                    {
                        return StatusCode(406, "Game has already started or is full");
                    }

                    else if (Util.Utility.existPlayer(gameId, name))
                    {
                        return StatusCode(409, "You are already part of this game");
                    }

                    else
                    {
                        Util.Utility.gameList[i].Players.Add(name);
                        return StatusCode(200, "Player was added to the ongoing game");
                    }
                }//if
            }//for

            return StatusCode(404, "Trouble adding");

        }
        [HttpHead]
        [Route("{gameId}/[action]")]
        public ActionResult start(String gameId, [FromHeader]  String name, [FromHeader] String password)
        {

            if (Util.Utility.existGameId(gameId))
            {
                if (Util.Utility.existOwner(name))
                {
                    if (Util.Utility.password(password))
                    {

                        if (Util.Utility.verifyPlayersCount(gameId))
                        {
                            if (Util.Utility.getGame(gameId).Status == "lobby")
                            {
                                Util.Utility.getGame(gameId).Status = "leader";
                                int countPsychos = Util.Utility.getPsychosCount(gameId);
                                int count = 0;
                                while (count < countPsychos)
                                {
                                    string psycho = Util.Utility.getRandomLeader(gameId);
                                    if (!Util.Utility.getGame(gameId).Psychos.Contains(psycho))
                                    {
                                        Util.Utility.getGame(gameId).Psychos.Add(psycho);
                                        count++;
                                    }
                                }

                                Round round = new Round(Util.Utility.getRandomLeader(gameId));
                                Util.Utility.getGame(gameId).Rounds.Add(round);
                                return Ok("Operation successful");
                            }
                            else
                            {
                                return StatusCode(401, "Unathorized");
                            }
                        }
                        else
                        {
                            return StatusCode(406, "Not Acceptable");
                        }
                    }
                    else
                    {
                        return StatusCode(401, "Unathorized");
                    }

                }
                else
                {
                    return StatusCode(401, "You are not the game's owner");


                }
            }
            else
            {
                return StatusCode(404, "Invalid Game's id");
            }


        }

        [HttpPost]
        [Route("{gameId}/[action]")]
        public ActionResult group(string gameId, [FromHeader] string name, [FromHeader] string password, [FromBody] GroupProposal playersGroup)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, "Provided group has invalid parameters");
            }
            else
            {
                if (Util.Utility.verifyPlayersCount(gameId, playersGroup.Players.Count()) && Util.Utility.verifyPlayersExist(gameId, playersGroup))
                {
                    if (Util.Utility.existGameId(gameId))
                    {
                        if (Util.Utility.existPlayer(gameId, name))
                        {
                            if (Util.Utility.password(password))
                            {
                                if (Util.Utility.getGame(gameId).Status == "leader")
                                {
                                    Util.Utility.getGame(gameId).Status = "rounds";

                                    for (int i = 0; i < playersGroup.Players.Count(); i++)
                                    {
                                        Group group = new Group(playersGroup.Players[i]);
                                        Util.Utility.getGame(gameId).Rounds[Util.Utility.getRounds(gameId)].Group.Add(group);


                                    }

                                    return Ok("Operation successful");
                                }
                                else
                                {
                                    return StatusCode(406, "Game is not in the groups stage");
                                }

                            }
                            else
                            {
                                return StatusCode(401, "Unathorized");
                            }

                        }
                        else
                        {
                            return StatusCode(403, "You are not part of the players list");


                        }
                    }
                    else
                    {
                        return StatusCode(404, "Invalid Game's id");
                    }
                }
                else
                {
                    return StatusCode(406, "Provided group has invalid parameters (size/players)");
                }

            }
        }



        [HttpPost]
        [Route("{gameId}/[action]")]
        public ActionResult go(string gameId, [FromHeader] string name, [FromHeader] string password, [FromBody] PsychoSelection psycho)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(406, "Data provided is invalid");
            }
            else
            {
                if (Util.Utility.verifyPlayerSelection(gameId, name))
                {
                    if (Util.Utility.existGameId(gameId))
                    {
                        if (Util.Utility.existPlayer(gameId, name))
                        {
                            if (Util.Utility.verifyGroupList(gameId, name))
                            {


                                if (Util.Utility.password(password))
                                {
                                    if (Util.Utility.getGame(gameId).Status == "rounds")
                                    {
                                        Util.Utility.GetGroup(gameId, name).Psycho = psycho.Psycho;

                                        if (Util.Utility.verifyAllGroupSelection(gameId))
                                        {
                                                if (Util.Utility.verifyPsychoWin(gameId))
                                                {
                                                    
                                                    Util.Utility.getGame(gameId).PsychoWin.Add(true);
                                                    if (Util.Utility.verifyGameWinner(gameId))
                                                    {
                                                        Util.Utility.getGame(gameId).Status = "ended";
                                                    }
                                                    else
                                                    {
                                                    Util.Utility.getGame(gameId).Rounds[Util.Utility.getRounds(gameId)].Winner = "psychos";
                                                    Util.Utility.getGame(gameId).Status = "leader";
                                                    Round round = new Round(Util.Utility.getRandomLeader(gameId));
                                                    Util.Utility.getGame(gameId).Rounds.Add(round);
                                                    }
                                                
                                                    
                                                }
                                                else
                                                {
                                                    
                                                    Util.Utility.getGame(gameId).PsychoWin.Add(false);
                                                if (Util.Utility.verifyGameWinner(gameId))
                                                {
                                                    Util.Utility.getGame(gameId).Status = "ended";
                                                }
                                                else
                                                {
                                                    Util.Utility.getGame(gameId).Rounds[Util.Utility.getRounds(gameId)].Winner = "players";
                                                    Util.Utility.getGame(gameId).Status = "leader";
                                                    Round round = new Round(Util.Utility.getRandomLeader(gameId));
                                                    Util.Utility.getGame(gameId).Rounds.Add(round);
                                                }

                                                }
                                            

                                        }

                                        return Ok("Operation successful");
                                    }
                                    else
                                    {
                                        return StatusCode(406, "Game is not in the rounds stage");
                                    }
                                }
                                else
                                {
                                    return StatusCode(401, "Unathorized");
                                }
                            }
                            else
                            {
                                return StatusCode(401, "You are not part of the round list");
                            }


                        }
                        else
                        {
                            return StatusCode(403, "You are not part of the players list");


                        }
                    }
                    else
                    {
                        return StatusCode(404, "Invalid Game's id");
                    }
                }
                else
                {
                    return StatusCode(409, "There is already an entry for you this round");
                }

            }

        }


    }




}

