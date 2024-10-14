
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
    public AudioMixer masterMixer;

    public void Start()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        Debug.Log("Setting slider " + PlayerPrefs.GetFloat("SFXVolume"));
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

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
        UIManager.UIManagerInstance.SetMainMenuPage();
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
    }

    public void SetResValue() 
    {
        ResoultionManager.ResInstance.SetResolution(_resDropDown.value);
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {      
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
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
