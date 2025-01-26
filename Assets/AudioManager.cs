
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
    public AudioClip laser;

    private void Start()
    {
        musicSource.clip = ambient;
        musicSource.Play();
    }

    // Método para tocar o efeito sonoro
    public void PlaySFX(AudioClip clip)
    {
        if(clip == shoot)
        {
            SFXSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
            SFXSource.PlayOneShot(clip);
        }
        else if(clip == enemyDeath)
        {
            SFXSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
            SFXSource.PlayOneShot(clip);
        }
        else
            SFXSource.PlayOneShot(clip);  
    }

    // Método para tocar a música (se necessário)
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic(AudioClip clip)
    {
        if (musicSource.isPlaying && musicSource.clip == clip)
        {
            musicSource.Stop();
        }
    }

}
