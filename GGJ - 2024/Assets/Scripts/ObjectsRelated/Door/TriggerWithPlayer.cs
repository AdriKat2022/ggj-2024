using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWithPlayer : MonoBehaviour
{

    private bool hasBeenTriggered;
    [SerializeField] private bool isforName;
    public UnityEvent<bool> BarTriggeredEvent;
    public UnityEvent<bool> NameTriggeredEvent;

    public bool GetTriggered()
    {
        return hasBeenTriggered;
    }
    // Start is called before the first frame update
    void Start()
    {
        hasBeenTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movement))
        {
            print("has been triggered");
            if (isforName)
            {
                NameTriggeredEvent?.Invoke(true);
            }
            else
            {
                BarTriggeredEvent?.Invoke(true);

            }
        }
    } 
}
