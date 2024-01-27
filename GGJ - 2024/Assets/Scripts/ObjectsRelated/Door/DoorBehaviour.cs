using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TriggerWithPlayer TriggerName;
    
    [SerializeField] private TriggerWithPlayer TriggerBar;
    [SerializeField] private bool isBoss;

    private GameObject UIHandler;

    public bool GetIsBoss()
    {
        return isBoss;
    }

    void Start()
    {
        UIHandler = GameObject.Find("UI Handler");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnDisable()
    {
        if (isBoss)
        {
            TriggerName.NameTriggeredEvent.Invoke(false);
            TriggerBar.BarTriggeredEvent.Invoke(false);

            this.gameObject.SetActive(false);
        }
    }

}