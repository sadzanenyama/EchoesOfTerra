
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsPage : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private TMP_Dropdown _resDropDown;
    [SerializeField] private TextMeshProUGUI _resText;
    [SerializeField] private TMP_Dropdown _graphicsDrop;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Toggle _fpsToggle;
    public AudioMixer music;
    public AudioMixer sfx;
    public void Start()
    {
        _musicSlider.value = AudioManager.instance.MusicAudioValues();
        _sfxSlider.value = AudioManager.instance.SFXAudioValues();
        ChangeSoundEffectsVolume();
        ChangeMusicVolume();
        _resDropDown.ClearOptions();
        if(ResoultionManager.ResInstance.GetScreenOptions() != null)
        {
            _resDropDown.AddOptions(ResoultionManager.ResInstance.GetScreenOptions());
            
            _resDropDown.value = 0;
            ResoultionManager.ResInstance.SetCurrentIndex(0);
            _resDropDown.RefreshShownValue();
        }
        else
        {
            _resDropDown.gameObject.SetActive(false);
            _resText.gameObject.SetActive(false);
        }
        

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
        AudioManager.instance.AdjustMusicVolume(_musicSlider.value/80);
    }

    public void ChangeSoundEffectsVolume()
    {
        AudioManager.instance.AdjustSFXVolume(_sfxSlider.value/80);
    }

    public void SetResValue() 
    {
        ResoultionManager.ResInstance.SetResolution(_resDropDown.value);
    }

    public void SetVolumeMixer(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume/80);
        music.SetFloat("Volume", volume); 
    }
    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("AudioVolume", volume / 80);
        sfx.SetFloat("VolumeSFX", volume);
    }
    public void VSYNC()
    {
        QualitySettings.vSyncCount = _vsyncToggle.isOn ? 1 :0;
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
    public void FPS()
    {
        UIManager.UIManagerInstance.SetFPSActive(_fpsToggle.isOn);
    }

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
}
