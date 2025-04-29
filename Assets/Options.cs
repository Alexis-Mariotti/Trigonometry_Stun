using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    public TMP_Text txtVolume;
    public Toggle toggleMute;

    void Start()
    {
        SliderChange();
        toggleMute.isOn = audioSource.mute;
    }

    void Update()
    {
    
    }

    public void SliderChange()
    {
        audioSource.volume = slider.value;
        txtVolume.text = (audioSource.volume * 100).ToString("00") + "%";
    }

    public void Mute()
    {
        if (audioSource.mute)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }
        toggleMute.isOn = audioSource.mute;
    }
}
