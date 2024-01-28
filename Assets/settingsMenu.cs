using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using FMOD;
using FMODUnity;


public class settingsMenu : MonoBehaviour
{
    //public AudioMixer audioMixer;
    public float mastervolume = 1;
    private Bus masterBus;


    private void Awake()
    {
        masterBus = RuntimeManager.GetBus("bus:/");

    }

    private void Update()
    {
        

    }

    // Start is called before the first frame update
    public void SetVolume(float mastervolume)
    {
        //audioMixer.SetFloat("volume",volume);
        masterBus.setVolume(mastervolume);

    }
}
