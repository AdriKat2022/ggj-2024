using UnityEngine;
using UnityEngine.Events;

public class TriggerWithPlayer : MonoBehaviour
{
    [SerializeField]
    private bool isforName;

    public UnityEvent<bool> BarTriggeredEvent;
    public UnityEvent<bool> NameTriggeredEvent;

    private bool hasBeenTriggered;


    public bool GotTriggered()
    {
        return hasBeenTriggered;
    }

    void Start()
    {
        hasBeenTriggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement movement))
        {
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
