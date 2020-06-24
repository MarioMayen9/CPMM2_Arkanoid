
using System;
using System.Collections.Generic;
using System.Data;
using ProyectoFinal_Arkanoid;

namespace Arkanoid
{
    public static class PlayerController
    {
        public static bool CreatePlayer(string nickname)
        {
            var dt = DataBaseController.ExecuteQuery($"SELECT * FROM PLAYER WHERE nickname = '{nickname}'");

            if(dt.Rows.Count > 0)
                return true;
            else
            {
                DataBaseController.ExecuteNonQuery("INSERT INTO PLAYER(nickname) VALUES" +
                                                   $"('{nickname}')");
                return false;
            }
        }

        public static void CreateNewScore(string player, int score)
        {
            DataBaseController.ExecuteNonQuery(string.Format("insert into scores(score, idplayer) select {0}, pl.idplayer from player pl where pl.nickname = '{1}' ", score, player));
        }
        public static List<Player> ObtainTopPlayers()
        {
            var topPlayers = new List<Player>();
            DataTable dt = DataBaseController.ExecuteQuery("SELECT pl.nickname, sc.score " +
                                                           "FROM PLAYER pl, SCORES sc " +
                                                           "WHERE pl.idPlayer = sc.idPlayer " +
                                                           "ORDER BY sc.score DESC " +
                                                           "LIMIT 10");

            foreach(DataRow dr in dt.Rows)
            {
                topPlayers.Add(new Player(dr[0].ToString(), Convert.ToInt32(dr[1])));
            }

            return topPlayers;
        }
    }
}
