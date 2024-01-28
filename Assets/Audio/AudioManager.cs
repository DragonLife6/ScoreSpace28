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
    }

    public void Upgrade()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Upgrade");
    }
}
