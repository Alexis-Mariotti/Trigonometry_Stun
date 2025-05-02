using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public AudioSource audioSourceBackground;
    public Slider sliderBackground;
    public TMP_Text txtVolumeBackground;

    public AudioSource audioSourceSFX;
    public Slider sliderSFX;
    public TMP_Text txtVolumeSFX;

    public Toggle toggleMute;

    void Start()
    {
        SliderBackgroundChange();
        SliderSFXChange();
        toggleMute.isOn = false;
        audioSourceBackground.mute = false;
        audioSourceSFX.mute = false;
    }

    public void SliderBackgroundChange()
    {
        audioSourceBackground.volume = sliderBackground.value;
        txtVolumeBackground.text = (audioSourceBackground.volume * 100).ToString("00") + "%";
    }

    public void SliderSFXChange()
    {
        audioSourceSFX.volume = sliderSFX.value;
        txtVolumeSFX.text = (audioSourceSFX.volume * 100).ToString("00") + "%";
    }

    public void Mute()
    {
        if (audioSourceBackground.mute)
        {
            audioSourceBackground.mute = false;
            audioSourceSFX.mute = false;
        } else
        {
            audioSourceBackground.mute = true;
            audioSourceSFX.mute = true;
        }
        toggleMute.isOn = audioSourceBackground.mute;
    }
}
