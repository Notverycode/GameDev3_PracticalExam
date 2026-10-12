
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptionsUI : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    private void Start()
    {
        // Load saved volume settings
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 1f);

        // Set slider positions
        musicSlider.SetValueWithoutNotify(musicVolume);
        sfxSlider.SetValueWithoutNotify(sfxVolume);

        // Apply the saved settings
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

        // Listen for slider changes
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        ApplyVolume(MusicKey, volume);
    }

    public void SetSFXVolume(float volume)
    {
        ApplyVolume(SFXKey, volume);
    }

    private void ApplyVolume(string parameter, float volume)
    {
        volume = Mathf.Clamp01(volume);

        PlayerPrefs.SetFloat(parameter, volume);

        if (audioMixer != null)
        {
            float decibels = volume <= 0.0001f
                ? -80f
                : Mathf.Log10(volume) * 20f;

            audioMixer.SetFloat(parameter, decibels);
        }

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        if (musicSlider != null)
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
