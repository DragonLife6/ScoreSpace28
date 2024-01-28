using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public void Upgrade()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Upgrade");
    }

    public void Footstep()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Footstep");
    }
}
