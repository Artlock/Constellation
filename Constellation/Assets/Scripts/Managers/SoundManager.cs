using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource sfx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    //Sfx 8 Sound Effect - Noah Smith
    public void SoundEffect(AudioClip clip)
    {
        sfx.pitch = Random.Range(1f, 2f);
        sfx.PlayOneShot(clip);
    }
}
