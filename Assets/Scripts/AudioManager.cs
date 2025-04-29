using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Sources -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip -----------")]
    public AudioClip background;
    public AudioClip ambianceSound;

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        SFXSource.clip = ambianceSound;
        SFXSource.Play();
    }
}
