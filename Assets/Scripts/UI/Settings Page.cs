
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SettingsPage : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private TMP_Dropdown _resDropDown;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Toggle _fpsToggle;
    public void Start()
    {
        _musicSlider.value = AudioManager.instance.MusicAudioValues();
        _sfxSlider.value = AudioManager.instance.SFXAudioValues();
        ChangeSoundEffectsVolume();
        ChangeMusicVolume();
        _resDropDown.ClearOptions();
        _resDropDown.AddOptions(ResoultionManager.ResInstance.GetScreenOptions());
        _resDropDown.value = 0;
        ResoultionManager.ResInstance.SetCurrentIndex(0);
        _resDropDown.RefreshShownValue();
        if(QualitySettings.vSyncCount == 0)
        {
            _vsyncToggle.isOn = false;
        }
        else
        {
           _vsyncToggle.isOn= true;
        }
        _fpsToggle.isOn = UIManager.UIManagerInstance.FPSMarkerActive(); 
    }
    public void Back()
    {
        AudioManager.instance.PlayAudioSFX("ButtonClick");
        UIManager.UIManagerInstance.SetMainMenuPage();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.AdjustMusicVolume(_musicSlider.value);
    }

    public void ChangeSoundEffectsVolume()
    {
        AudioManager.instance.AdjustSFXVolume(_sfxSlider.value);
    }

    public void SetResValue() 
    {
        ResoultionManager.ResInstance.SetResolution(_resDropDown.value);
    }

    public void VSYNC()
    {
        QualitySettings.vSyncCount = _vsyncToggle.isOn ? 1 :0;
    }

    public void FPS()
    {
        UIManager.UIManagerInstance.SetFPSActive(_fpsToggle.isOn);
    }
}
