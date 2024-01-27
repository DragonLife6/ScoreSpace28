using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathMenuScript : MonoBehaviour
{
    [SerializeField] TMP_Text currentScoreTextField;
    [SerializeField] TMP_Text maximumScoreTextField;

    [SerializeField] TMP_InputField playerNameInputField;

    [SerializeField] LeaderboardGlobal leaderboard;

    [SerializeField] PlayerScore playerScore;
    int currentScore, maximumScore;


    private void Start()
    {
        StartCoroutine(DeathMenuInitializationRoutine());
    }

    IEnumerator DeathMenuInitializationRoutine()
    {
        yield return leaderboard.FetchPlayerName();
        yield return leaderboard.FetchPlayerHighestScore();
        DeathMenuSetup();
    }

    private void DeathMenuSetup()
    {
        currentScore = playerScore.GetPlayerScore();
        maximumScore = PlayerPrefs.GetInt("PlayerScore");
        string playerName = PlayerPrefs.GetString("PlayerName");

        playerNameInputField.text = playerName;
        currentScoreTextField.text = currentScore.ToString();
        maximumScoreTextField.text = maximumScore.ToString();

        LoadLeaderboardData();
    }


    public void ReloadButtonPressed()
    {
        string newName = playerNameInputField.text;

        if(newName.Length > 0)
        {
            leaderboard.SetPlayerName(newName);
        }

        LoadLeaderboardData();
    }

    public void RestartButtonPressed()
    {
        int currentSceneID = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneID);
    }


    public void LoadLeaderboardData()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return leaderboard.FetchTopHighscoresRoutine(10);
    }
}
