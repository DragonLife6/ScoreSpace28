using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

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
