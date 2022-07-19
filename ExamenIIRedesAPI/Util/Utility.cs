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
                if (gameList[i].Owner == value)
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
            return getGame(gameId).Rounds.Count()-1;
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
                if (gameList[i].Password == value)
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

        public static bool verifyPlayerSelection(string gameId, string name)//Verifica si el jugador ya eligió un camino
        {
            bool verify = false;
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Name.Equals(name))
                {
                    if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Psycho == null)
                    {
                        verify = true;
                    }
                }


            }
            return verify;
            
        }
        public static Group GetGroup(string gameId, string name)
        {
            Group group = null;
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Name.Equals(name))
                {
                    group = getGame(gameId).Rounds[getRounds(gameId)].Group[i];
                }


            }
            return group;

        }
        public static bool verifyAllGroupSelection(string gameId)//Verifica si el grupo ya realizo todas sus elecciones
        {
            bool verify = false;
            int count = 0;
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Psycho!=null)
                {
                    count++;
                }


            }
            if(count == getGame(gameId).Rounds[getRounds(gameId)].Group.Count())
            {
                verify = true;
            }

            return verify;
        }
        public static bool verifyPsychoWin(string gameId)//Verifica si los psycho ganaron la ronda
        {
            bool verify = false;
            int countPsycho = 0;
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Psycho == true)
                {
                    countPsycho++;
                }
                


            }
            if (countPsycho>0)
            {
                verify = true;
            }

            return verify;
        }

        public static string getRandomLeader(string gameId)
        {
            Random rd = new Random();
            Game game = getGame(gameId);
            int rand_num = rd.Next(0, (game.Players.Count()-1));
            return game.Players[rand_num];

        }

        public static bool verifyGameWinner(string gameId)//Verifica si hay un bando ya es ganador
        {
            bool verify = false;
            int countPsycho = 0;
            int countExem = 0;
            for (int i = 0; i < getGame(gameId).PsychoWin.Count(); i++)
            {
                if (getGame(gameId).PsychoWin[i] == true)
                {
                    countPsycho++;
                }
                else
                {
                    countExem++;
                }
                


            }
            if (countPsycho == 3 || countExem == 3)
            {
                verify = true;
            }

            return verify;
        }

        public static bool verifyGroupList(string gameId, string name)//Verifica si el jugador pertenece al grupo enviado
        {
            bool verify = false;
            
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).Rounds[getRounds(gameId)].Group[i].Name.Equals(name))
                {
                    verify = true;
                }



            }
            return verify;
        }
        public static bool psychosWin(string gameId)//Verifica si el jugador pertenece al grupo enviado
        {
            bool verify = false;
            int countPsycho = 0;
            for (int i = 0; i < getGame(gameId).Rounds[getRounds(gameId)].Group.Count(); i++)
            {
                if (getGame(gameId).PsychoWin[i].Equals(true))
                {
                    countPsycho++;
                }



            }
            if (countPsycho==3)
            {
                verify=true;
            }

            
            return verify;
        }
        
        public static int getPsychosCount(string gameId)
        {
            Game game = getGame(gameId);
            if (game.Players.Count() == 5 || game.Players.Count() == 6)
            {
                return 2;
            }else if(game.Players.Count() == 7 || game.Players.Count() == 8 || game.Players.Count() == 9)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public static bool verifyPlayersCount(string gameId)
        {
            Game game = getGame(gameId);
            bool verify = true;
            if (game.Players.Count() < 5)
            {
                verify = false;
            }
            return verify;
        }
    }
}
