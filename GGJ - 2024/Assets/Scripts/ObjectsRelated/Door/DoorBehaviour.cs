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

    void Start()
    {
        UIHandler = GameObject.Find("UI Handler");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
