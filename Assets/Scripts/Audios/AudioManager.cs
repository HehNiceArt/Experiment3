using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField]  private AudioClip[] audios;
    PlayerController playerController;
    PlayerDash playerDash;
    private bool wasJumping = false;
    private bool wasDashing = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerDash = FindObjectOfType<PlayerDash>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (playerController != null && playerController.isJumping && !wasJumping)
        {
            PlayJumpSound();
        }
        wasJumping = playerController.isJumping;

        if (playerDash != null && playerDash.isDashing && !wasDashing)
        {
            PlayDashSound();
        }
        wasDashing = playerDash.isDashing;
    }
    public void PlayJumpSound()
    {
        PlayAudio("SFX_Jump");
    }

    public void PlayDashSound()
    {
        PlayAudio("SFX_Dash");
    }

    private void PlayAudio(string audioName)
    {
        AudioClip clip = FindAudioByName(audioName);

        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();
    }

    private AudioClip FindAudioByName(string audioName)
    {
        foreach (AudioClip audio in audios)
        {
            if (audio.name == audioName)
            {
                return audio;
            }
        }
        return null;
    }
}
