using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sources")]

    [SerializeField]
    private AudioSource musicSource, effectSource, effectSourceRepeat;


    [Header("Musics")]

    public AudioClip mainMenu;
    public AudioClip firstTheme;

    [Header("Sound effects")]

    public AudioClip jump;
    public AudioClip damage;



    #region Singleton
    public static SoundManager Instance { get; private set ; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        musicSource.loop = true;
        effectSource.loop = false;
        effectSourceRepeat.loop = true;
    }

    public AudioClip GetCurrentMusic()
    {
        return musicSource.clip;
    }

    #region Sound control

    public void PlaySound(AudioClip clip, float delay = 0)
    {
        if (delay > 0)
        {
            StartCoroutine(PlaySoundAfterTime(clip, delay));
            return;
        }
        effectSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Play a sound on loop
    /// </summary>
    /// <param name="clip">The clip to play in loop</param>
    /// <param name="forceRestart">If true, the sound will restart and play in loop. If false, the sound won't restart if it's the same.</param>
    public void PlaySoundLoop(AudioClip clip, bool forceRestart = false)
    {
        effectSourceRepeat.loop = true;
        if (!forceRestart && clip == effectSourceRepeat.clip)
        {
            if (!effectSourceRepeat.isPlaying)
                effectSourceRepeat.Play();
            return;
        }

        effectSourceRepeat.clip = clip;
        effectSourceRepeat.Play();
    }

    /// <summary>
    /// Stops the looping sound
    /// </summary>
    /// <param name="forceStop">Forces immediate stop, otherwise waits for the last iteration.</param>
    public void StopSoundLoop(bool forceStop = false)
    {
        effectSourceRepeat.loop = false;
        if (forceStop)
            effectSourceRepeat.Stop();
    }

    public void PlayMusic(AudioClip clip, float delay = 0, bool forceRestart = false)
    {
        if(delay > 0)
        {
            StartCoroutine(PlayMusicAfterTime(clip, delay, forceRestart));
            return;
        }

        if (!forceRestart && clip == musicSource.clip)
        {
            if (!musicSource.isPlaying)
                musicSource.Play();
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    #endregion

    private IEnumerator PlaySoundAfterTime(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySound(clip);
    }
    private IEnumerator PlayMusicAfterTime(AudioClip clip, float delay, bool forceRestart)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        PlayMusic(clip, forceRestart: forceRestart);
    }
}
