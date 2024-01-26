using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] LeaderboardGlobal leaderboard;
    [SerializeField] TMP_Text scoreTextField;
    float currentScore;


    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentScore += Time.deltaTime;
        scoreTextField.text = Mathf.FloorToInt(currentScore).ToString();
    }

    public IEnumerator SendScore()
    {
        yield return leaderboard.SubmitScoreRoutine(Mathf.FloorToInt(currentScore));
    }
}
