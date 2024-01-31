using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BossDisplayUI : MonoBehaviour
{
    [Header("Need one script attached for each boss in the scene")]
    [SerializeField] private TriggerWithPlayer nameTrigger;
    [SerializeField] private TriggerWithPlayer barTrigger;
    [SerializeField] private GameObject BossName;
    [SerializeField] private GameObject BossBar;

    private SoundManager soundManager;
    [Header("This is not essential")] 
    [SerializeField] private CameraFollow follow;
    [SerializeField] private GameObject BossFocus;
    [SerializeField] private GameObject player;

    void Start()
    {
        nameTrigger.NameTriggeredEvent.AddListener(HandlerName);
        barTrigger.BarTriggeredEvent.AddListener(HandlerBar);
        soundManager = SoundManager.Instance;
    }

    void HandlerName(bool val)
    {
        
        BossName.SetActive(val);
        if (!val)
        {
            nameTrigger.NameTriggeredEvent.RemoveListener(HandlerName);
        }
        

    }

    void HandlerBar(bool val)
    {
        BossBar.SetActive(val);
        soundManager.PlayMusic(soundManager.BossThemeIntro);
        soundManager.PlayMusic(soundManager.BossTheme, soundManager.BossThemeIntro.length - Time.deltaTime);
        follow.SetFollow(BossFocus.transform);
        follow.SetFollowSpeed(2);
        if (!val)
        {
            follow.SetFollow(player.transform);
            follow.SetFollowSpeed(5);
            soundManager.StopMusic();
           
            barTrigger.BarTriggeredEvent.RemoveListener(HandlerBar);
        }
    }
}
