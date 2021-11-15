using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour
{
    public Slider SoundSlider;
    private float SoundSliderValue;
    public Image ImageMute;
    // Start is called before the first frame update
    void Start()
    {
        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        AudioListener.volume = SoundSlider.value;
        CheckMuteOn();
    }
    public void ChangeSlider(float value)
    {
        SoundSliderValue = value;
        PlayerPrefs.SetFloat("SoundVolume", SoundSliderValue);
        AudioListener.volume = SoundSlider.value;
        CheckMuteOn();
    }
    public void CheckMuteOn()
    {
        if (SoundSliderValue == 0f)
        {
            ImageMute.enabled = true;
        }
        else
        {
            ImageMute.enabled = false;
        }
    }
}
