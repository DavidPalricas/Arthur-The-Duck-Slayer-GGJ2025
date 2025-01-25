
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip -------------")]
    public AudioClip ambient;
    public AudioClip death;
    public AudioClip heal;
    public AudioClip finalBoss;
    public AudioClip doorBreak;
    public AudioClip enemyDeath;
    public AudioClip shoot;

    private void Start()
    {
        musicSource.clip = ambient;
        musicSource.Play();
    }

    // M�todo para tocar o efeito sonoro
    public void PlaySFX(AudioClip clip)
    {
        if(clip == shoot)
        {
            SFXSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
            SFXSource.PlayOneShot(clip);
        }

        SFXSource.PlayOneShot(clip);  // Toca o efeito sonoro uma vez
    }

    // M�todo para tocar a m�sica (se necess�rio)
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

}
