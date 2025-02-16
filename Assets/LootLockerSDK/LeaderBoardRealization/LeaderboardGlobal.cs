using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.XR;

// This script controlls global hightscore leaderboard (load / save data)
public class LeaderboardGlobal : MonoBehaviour
{
    [SerializeField] string leaderboardKey = "gameGlobalHighscore";
    [SerializeField] TMP_Text playersValues;
    [SerializeField] TMP_Text scoresValues;

    public IEnumerator SubmitScoreRoutine(int score)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score was saved!");

                done = true;
            }
            else
            {
                Debug.Log("Score saving error: " + response.errorData);

                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void SetPlayerName(string name)
    {
        LootLockerSDKManager.SetPlayerName(name, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.errorData.message);
            }
        });
    }

    public IEnumerator FetchPlayerName()
    {
        string playerID = PlayerPrefs.GetString("PlayerID");
        bool done = false;

        LootLockerSDKManager.GetPlayerName((response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful:" + response.name);
                PlayerPrefs.SetString("PlayerName", response.name);
                done = true;
            }
            else
            {
                Debug.Log("failed: " + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }




    public IEnumerator FetchPlayerHighestScore()
    {
        string playerID = PlayerPrefs.GetString("PlayerID");
        bool done = false;

        LootLockerSDKManager.GetMemberRank(leaderboardKey, playerID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful:" + response.score.ToString());
                PlayerPrefs.SetInt("PlayerScore", response.score);
                done = true;
            }
            else
            {
                Debug.Log("failed: " + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }



    public IEnumerator FetchTopHighscoresRoutine(int countOfScores = 10)
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardKey, countOfScores, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    } else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }

                playersValues.text = tempPlayerNames;
                scoresValues.text = tempPlayerScores;

                done = true;
            }
            else
            {
                Debug.Log("Score loading Error: " + response.errorData);

                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
