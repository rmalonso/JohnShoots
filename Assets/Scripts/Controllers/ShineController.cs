using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShineController : MonoBehaviour
{
    public Slider ShineSlider;
    private float ShineSliderValue;
    public Image PanelShine;
    void Start()
    {
        ShineSlider.value = PlayerPrefs.GetFloat("Shine", 0.5f);
        PanelShine.color = new Color(PanelShine.color.r, PanelShine.color.g, PanelShine.color.b, ShineSlider.value);
    }
    public void ChangeSlider(float value)
    {
        ShineSliderValue = value;
        PlayerPrefs.SetFloat("Shine", ShineSliderValue);
        PanelShine.color = new Color(PanelShine.color.r, PanelShine.color.g, PanelShine.color.b, ShineSlider.value);
    }
}
