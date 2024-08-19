using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class OptionsPanel : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle vsyncToggle;
    private UILayer gameMenuLayer;

    void Start()
    {
        // Acquire the game menu layer from the UIManager singleton
        gameMenuLayer = UIManager.Instance.GetGameMenuLayer();
        // Load volume or set default to 1 if it doesn't exist
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = volumeSlider.value;

        // Load VSync setting
        int vsyncEnabled = PlayerPrefs.GetInt("VSyncEnabled", 0);
        vsyncToggle.isOn = vsyncEnabled == 1;
        QualitySettings.vSyncCount = vsyncEnabled;
        vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = vsyncToggle.isOn.ToString();
    }


  public void Open()
    {

            // Additional logic for when the Options panel opens
            // Load volume or set default to 1 if it doesn't exist
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
            AudioListener.volume = volumeSlider.value;


            // Load VSync setting
            int vsyncEnabled = PlayerPrefs.GetInt("VSyncEnabled", 0);
            vsyncToggle.isOn = vsyncEnabled == 1;
            QualitySettings.vSyncCount = vsyncEnabled;
            if(vsyncToggle.isOn){
                vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "on";
            }else{
                vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "off";
            }
            
            PushAllChildren();
            InitializeOptions();
    }
    public void Close(){
        PopAllChildren();
    }
      public void PushAllChildren()
    {
        foreach (Transform child in transform)
        {
            UIElement uiElement = child.GetComponent<UIElement>();
            if (uiElement != null)
            {
                uiElement.Show();
                gameMenuLayer.Push(uiElement);
            }
        }
    }

    public void PopAllChildren()
    {
        foreach (Transform child in transform)
        {
            UIElement uiElement = child.GetComponent<UIElement>();
            if (uiElement != null)
            {
                uiElement.Hide();
                gameMenuLayer.Pop(uiElement);
            }
        }
    }
        public void InitializeOptions()
    {
        // Load volume or set default to 1 if it doesn't exist
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        AudioListener.volume = volumeSlider.value;

        // Load VSync setting
        int vsyncEnabled = PlayerPrefs.GetInt("VSyncEnabled", 0);
        vsyncToggle.isOn = vsyncEnabled == 1;
        if(vsyncToggle.isOn){
            vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "on";
        }else{
            vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "off";
        }
    }
    public void SetVolume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Save to PlayerPrefs
    }

    public void SetVSync()
    {
        bool vsyncEnabled = vsyncToggle.isOn;
        QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
        PlayerPrefs.SetInt("VSyncEnabled", vsyncEnabled ? 1 : 0); // Save to PlayerPrefs
        if(vsyncToggle.isOn){
            vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "on";
        }else{
            vsyncToggle.GetComponentInChildren<TextMeshProUGUI>().text = "off";
        };
    }

}
