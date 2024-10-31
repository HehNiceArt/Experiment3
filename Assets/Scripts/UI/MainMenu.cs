using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider, sfxSlider;
    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    public AudioClip buttonClickSound;
    public AudioClip jumpSound;

    private void Start()
    {
        settingsPanel.SetActive(false);

        volumeSlider.value = bgMusicSource.volume;
        sfxSlider.value = sfxSource.volume;

        bgMusicSource.Play();  
    }

    public void PlayGame()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("MASTER");
    }

    public void ExitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
    }

    public void OpenSettings()
    {
        PlayButtonClickSound();
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        PlayButtonClickSound();
        settingsPanel.SetActive(false);
    }

    public void SetBGVolume(float volume)
    {
        bgMusicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }

    public void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
    }
}
