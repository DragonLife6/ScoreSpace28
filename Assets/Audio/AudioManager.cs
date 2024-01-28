using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance audioMusic;
    // Start is called before the first frame update
    void Start()
    {
        audioMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
        //audioMusic.start();
    }

    public void GameStart()
    {
        //audioMusic.start();
    }

    public void GameOver()
    {
        //audioMusic.release();
    }
}
