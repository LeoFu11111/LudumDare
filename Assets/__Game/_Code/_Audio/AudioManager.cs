using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioSource backgroundMusic;
    public AudioClip shootSound;
    public AudioClip jumpSound;
    public AudioClip spikesSound;
    public AudioClip breakWallSound;
    public AudioClip dashSound;
    public AudioClip joinSlimeSound;

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }
    
    public void PlayShootSound()
    {
        PlayOneShot(shootSound);
    }

    public void PlayJumpSound()
    {
        PlayOneShot(jumpSound);
    }

    public void PlaySpikesSound()
    {
        PlayOneShot(spikesSound);
    }

    public void PlayBreakWallSound()
    {
        PlayOneShot(breakWallSound);
    }

    public void PlayDashSound()
    {
        PlayOneShot(dashSound);
    }

    public void PlayJoinSlimeSound()
    {
        PlayOneShot(joinSlimeSound);
    }
    
    private void PlayOneShot(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
        //AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        StartCoroutine(HoldHostage(audioClip));
    }

    private IEnumerator HoldHostage(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length + 1.0f);
    }
}
