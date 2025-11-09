using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject listeningPosition;
    [SerializeField] GameObject listeningPositionPlayer;

    [SerializeField] AudioClip uiSound;
    [SerializeField] [Range(0f,1f)] float uiVolume = 1;

    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0f, 1f)] float shootVolume = 1;

    [SerializeField] AudioClip healSound;
    [SerializeField][Range(0f, 1f)] float healVolume = 1;

    [SerializeField] AudioClip attachSound;
    [SerializeField][Range(0f, 1f)] float attachVolume = 1;

    [SerializeField] AudioClip spawnSound;
    [SerializeField][Range(0f, 1f)] float spawnVolume = 1;

    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0f, 1f)] float deathVolume = 1;

    [SerializeField] AudioClip backgroundSound;
    [SerializeField][Range(0f, 1f)] float backgroundVolume = 1;

    [SerializeField] AudioClip bossSpawnSound;
    [SerializeField][Range(0f, 1f)] float bossSpawnVolume = 1;

    [SerializeField] AudioClip bossDeathSound;
    [SerializeField][Range(0f, 1f)] float bossDeathVolume = 1;

    [SerializeField] AudioClip bossShootSound;
    [SerializeField][Range(0f, 1f)] float bossShootVolume = 1;

    bool uiSoundMax = false;

    public void PlayBossShootSound()
    {
        PlaySoundEffect(bossShootSound, bossShootVolume);
    }

    public void PlayBossSpawnSound()
    {
        PlaySoundEffect(bossSpawnSound, bossSpawnVolume);
    }

    public void PlayBossDeathSound()
    {
        PlaySoundEffect(bossDeathSound, bossDeathVolume);
    }

    public void PlayBackgroundNoise()
    {
        PlaySoundEffect(backgroundSound, backgroundVolume);
    }

    public void PlaySpawnSound()
    {
        PlaySoundEffect(spawnSound, spawnVolume);
    }

    public void PlayDeathSound()
    {
        PlaySoundEffect(deathSound, deathVolume);
    }

    public void PlayUISound()
    {
        //limitovan pocet zvuku hitu najednou aby hrace neohlušily
        if (!uiSoundMax)
        {
            uiSoundMax = true;
            PlaySoundEffectOnCenter(uiSound, uiVolume);
        }
        if (uiSoundMax)
            StartCoroutine(MaxSoundDelay());

    }

    IEnumerator MaxSoundDelay()
    {
        yield return new WaitForSeconds(0.1f);
        uiSoundMax = false;

    }

    public void PlayAttachSound()
    {

        PlaySoundEffect(attachSound, attachVolume);
    }

    public void PlayHealSound()
    {

        PlaySoundEffect(healSound, healVolume);
    }

    public void PlayShootSound()
    {
        PlaySoundEffectOnCenter(shootSound, shootVolume);
    }

    private void PlaySoundEffectOnCenter(AudioClip soundEffect, float volume)
    {
        if (soundEffect != null)
        {
            //prehraje na pozici playerovy postavy, u nekterych zvuku to zni lepe a u stereo prehravani vice vycentrovane
            AudioSource.PlayClipAtPoint(soundEffect, new Vector3(listeningPositionPlayer.transform.position.x, listeningPositionPlayer.transform.position.y,-5), volume);
        }
    }


    private void PlaySoundEffect(AudioClip soundEffect, float volume)
    {
        if (soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, listeningPosition.transform.position, volume);
        }
    }

}
