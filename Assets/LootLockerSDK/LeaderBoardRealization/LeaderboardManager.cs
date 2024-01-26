using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] LeaderboardGlobal leaderboard;


    void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine(10);
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if(response.success)
            {
                Debug.Log("Loot Locker Connected!");

                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Loot Locker Connecting Error!");

                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
