using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Sources -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource clickSound;

    [Header("--------- Audio Clip -----------")]
    public AudioClip background;
    public AudioClip ambianceSound;

    public ThemeTemplate theme;

    void Start()
    {
        
        // make music loop
        musicSource.loop = true;

        if (theme != null)
        {
            PlayThemeBackgroundMusic();
        }
        else
        {
            musicSource.clip = background;
            musicSource.Play();
        }
        SFXSource.clip = ambianceSound;
        SFXSource.Play();
    }

    public void PlayDeathSound()
    {
        // play random sounds 
        int index = UnityEngine.Random.Range(0, theme.deathSounds.Length);
        SFXSource.clip = theme.deathSounds[index];
        SFXSource.Play();
    }

    public void PlayFinishSound()
    {

        // play random sounds 
        int index = UnityEngine.Random.Range(0, theme.finishSound.Length);
        SFXSource.clip = theme.finishSound[index];
        SFXSource.Play();
    }

    public void PlayThemeBackgroundMusic()
    {
        // loop
        musicSource.clip = theme.backgroundMusic;
        musicSource.Play();
    }

    public void Click()
    {
        clickSound.Play();
    }
}
