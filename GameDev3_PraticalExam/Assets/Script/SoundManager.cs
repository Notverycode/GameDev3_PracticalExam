
using UnityEngine;
using UnityEngine.Audio;
using StarterAssets;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Background Music")]
    public AudioClip backgroundMusic;

    [Header("Item Sound Effects")]
    public AudioClip addTimeSound;
    public AudioClip removeTimeSound;
    public AudioClip addScoreSound;
    public AudioClip removeScoreSound;
    public AudioClip jumpSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Player")]
    public ThirdPersonController player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ApplySavedVolumes();

        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayItemSound(ItemType itemType)
    {
        if (sfxSource == null)
            return;

        AudioClip clip = null;

        switch (itemType)
        {
            case ItemType.AddTime:
                clip = addTimeSound;
                break;

            case ItemType.RemoveTime:
                clip = removeTimeSound;
                break;

            case ItemType.AddScore:
                clip = addScoreSound;
                break;

            case ItemType.RemoveScore:
                clip = removeScoreSound;
                break;
        }

        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

    public void PlayWinSound()
    {
        PlaySFX(winSound);
    }

    public void PlayLoseSound()
    {
        PlaySFX(loseSound);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetMusicVolume(float volume)
    {
        SetMixerVolume("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SetMixerVolume("SFXVolume", volume);
    }

    private void SetMixerVolume(string parameter, float volume)
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
    }

    private void OnEnable()
    {
        if (player != null)
            player.OnJumped += PlayJumpSound;
    }

    private void OnDisable()
    {
        if (player != null)
            player.OnJumped -= PlayJumpSound;
    }

    private void ApplySavedVolumes()
    {
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }
}
