using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource jumpAudio;
    public AudioSource buttonAudio;
    public AudioSource doorAudio;
    public AudioSource fallAudio;
    public AudioSource collectAudio;
    public AudioSource tutorialAudio;
    public AudioSource characterAudio;

    public void PlayJumpSound()
    {
        jumpAudio.Play();
    }

    public void PlayButtonSound()
    {
        buttonAudio.Play();
    }

    public void PlayDoorSound()
    {
        doorAudio.Play();
    }

    public void PlayFallSound()
    {
        fallAudio.Play();
    }

    public void PlayCollectSound()
    {
        collectAudio.Play();
    }

    public void PlayTutorialSound()
    {
        tutorialAudio.Play();
    }

    public void PlayCharacterSound()
    {
        characterAudio.Play();
    }
}
