using ExamenIIRedesAPI.Models;

namespace ExamenIIRedesAPI.Util
{
    public class Utility
    {
        public static List<Game> gameList = new List<Game>();

        public Utility()
        {

        }

        public static bool existGameId(string value)
        {
            bool gameExist=false;
            for (int i = 0; i < gameList.Count(); i++)
            {
                if (gameList[i].GameId == value)
                {
                    gameExist=true; 
                }
               

            }
            return gameExist;


        }

        public static bool existOwner(string value)
        {
            bool gameExist = false;
            for (int i = 0; i < gameList.Count(); i++)
            {
                if (gameList[i].GameId == value)
                {
                    gameExist = true;
                }


            }
            return gameExist;


        }
        public static bool existPlayer(string gameId, string value)
        {
            bool playerExist = false; 
            for(int j=0; j < getGame(gameId).Players.Count(); j++)
            {
                if (getGame(gameId).Players[j] == value)
                {
                    playerExist=true;
                }

            }


            
            return playerExist;


        }

       public static bool verifyPlayersCount(string gameId, int count)
        {
            bool verify = false;

            if (getGame(gameId).Players.Count == 5)
            {
                if (getGame(gameId).Rounds.Count() == 1 && count == 2)
                {
                    verify = true;
                }else if (getGame(gameId).Rounds.Count() == 2 && count == 3)
                {
                    verify = true;
                }else if (getGame(gameId).Rounds.Count() == 3 && count == 2)
                {
                    verify = true;
                }else if (getGame(gameId).Rounds.Count() == 4 && count == 3)
                {
                    verify = true;
                }else if (getGame(gameId).Rounds.Count() == 5 && count == 3)
                {
                    verify = true;
                }
                
            }else if (getGame(gameId).Players.Count == 6)
            {
                if (getGame(gameId).Rounds.Count() == 1 && count == 2)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 2 && count == 3)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 3 && count == 4)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 4 && count == 3)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 5 && count == 4)
                {
                    verify = true;
                }

            }else if (getGame(gameId).Players.Count == 7)
            {
                if (getGame(gameId).Rounds.Count() == 1 && count == 2)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 2 && count == 3)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 3 && count == 3)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 4 && count == 4)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 5 && count == 4)
                {
                    verify = true;
                }

            }else if (getGame(gameId).Players.Count >= 8)
            {
                if (getGame(gameId).Rounds.Count() == 1 && count == 3)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 2 && count == 4)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 3 && count == 4)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 4 && count == 5)
                {
                    verify = true;
                }
                else if (getGame(gameId).Rounds.Count() == 5 && count == 5)
                {
                    verify = true;
                }

            }

            return verify;
        }

        public static int getRounds(string gameId)
        {
            return getGame(gameId).Rounds.Count();
        }

        public static bool verifyPlayersExist(string gameId, GroupProposal group)
        {
            bool gameExist = true;
                for (int j = 0; j < group.Players.Count(); j++)
                {
                    if (!getGame(gameId).Players.Contains(group.Players[j]))
                    {
                        gameExist = false;
                    }

                }
            return gameExist;
        }

        public static bool password(string value)
        {
            bool gameExist = false;
            for (int i = 0; i < gameList.Count(); i++)
            {
                if (gameList[i].GameId == value)
                {
                    gameExist = true;
                }


            }
            return gameExist;


        }

        public static Game getGame(string gameId)
        {
            Game game= null;
            for (int i = 0; i < gameList.Count(); i++)
            {
                if (gameList[i].GameId == gameId)
                {
                    game = gameList[i];
                }


            }
            return game;
        }
    }
}
