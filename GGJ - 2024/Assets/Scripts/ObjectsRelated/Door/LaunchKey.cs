using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchKey : MonoBehaviour
{
    private bool isLaunched;
    // Start is called before the first frame update
    void Start()
    {
        isLaunched = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (isLaunched && other.TryGetComponent(out DoorBehaviour door))
        {
            if (door.IsBoss())
            {
                other.gameObject.SetActive(false);
            }

        }
    }
}
