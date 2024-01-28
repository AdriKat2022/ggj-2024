using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public SubtitleObject subtitles;
    public AudioClip door_open;
    public float time_state1;
    public float time_state2;

    private float timer;
    private int state;


    void Start()
    {
        SoundManager.Instance.PlaySound(door_open);
        state = 0;
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > time_state1 && state == 0)
        {
            DialogueHandler.Instance.ShowSubtitles(subtitles);
            state = 1;
        }

        if (timer > time_state2 && state == 1)
        {

        }
    }
}
