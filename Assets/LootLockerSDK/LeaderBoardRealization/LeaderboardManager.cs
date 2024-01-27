using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using Unity.VisualScripting;

public class LeaderboardManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoginRoutine());
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
