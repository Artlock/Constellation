using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfx;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //Sfx 8 Sound Effect - Noah Smith
    public void SoundEffect(AudioClip clip)
    {
        sfx.pitch = Random.Range(1f, 2f);
        sfx.PlayOneShot(clip);
    }
}
