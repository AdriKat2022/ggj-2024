using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossDisplayUI : MonoBehaviour
{
    [Header("Need one script attached for each boss in the scene")]
    [SerializeField] private TriggerWithPlayer nameTrigger;
    [SerializeField] private TriggerWithPlayer barTrigger;
    [SerializeField] private GameObject BossName;
    [SerializeField] private GameObject BossBar;

    private SoundManager soundManager;
    
    void Start()
    {
        nameTrigger.NameTriggeredEvent.AddListener(HandlerName);
        barTrigger.BarTriggeredEvent.AddListener(HandlerBar);
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandlerName(bool val)
    {

        BossName.SetActive(val);
    }
    void HandlerBar(bool val)
    {
        BossBar.SetActive(val);
        soundManager.PlayMusic(soundManager.BossThemeIntro);
        soundManager.PlayMusic(soundManager.BossTheme, soundManager.BossThemeIntro.length - Time.deltaTime);
    }
}
