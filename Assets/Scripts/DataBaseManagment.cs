using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataBaseManager
{
    public static int currentPlayerId = 0;
    
    public static void CreateNewPlayer(string player_name) {
        DataTable scoreboard = MyDataBase.GetTable($"SELECT * FROM Player WHERE player_name = '{player_name}';");
        if (scoreboard.Rows.Count != 1)
        {
            MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Player (player_name) VALUES ('{player_name}');");
            scoreboard = MyDataBase.GetTable($"SELECT * FROM Player WHERE player_name = '{player_name}';");
        }
        currentPlayerId = int.Parse(scoreboard.Rows[0][0].ToString());
        PlayerPrefs.SetInt("savedId", currentPlayerId);
        CreateScore();
    }

    public static void CreateScore()
    {
        DataTable scoreboard = MyDataBase.GetTable($"SELECT * FROM Scores WHERE player_id = '{currentPlayerId}';");
        if (scoreboard.Rows.Count != 1)
        {
            MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Scores (player_id) VALUES ({currentPlayerId});");
        }
        LoadData();
    }

    public static string GetPlayersName()
    {
        DataTable scoreboard = MyDataBase.GetTable($"SELECT * FROM Player WHERE player_id = '{currentPlayerId}';");
        if (scoreboard.Rows.Count == 1)
            return scoreboard.Rows[0][1].ToString();
        return "";
    }
    public static void LoadData() {
        DataTable scoreboard = MyDataBase.GetTable($"SELECT * FROM Scores WHERE player_id = '{currentPlayerId}';");
        StaticData.money = int.Parse(scoreboard.Rows[0][1].ToString());
        StaticData.currentAct = int.Parse(scoreboard.Rows[0][2].ToString());
    }
    public static void SaveData() {
        MyDataBase.ExecuteQueryWithoutAnswer($"UPDATE Scores SET money = {StaticData.money}, currentAct = {StaticData.currentAct} WHERE player_id = '{currentPlayerId}';");
    }
    public static void SaveMoney()
    {
        MyDataBase.ExecuteQueryWithoutAnswer($"UPDATE Scores SET money = {StaticData.money} WHERE player_id = '{currentPlayerId}';");
    }
}
