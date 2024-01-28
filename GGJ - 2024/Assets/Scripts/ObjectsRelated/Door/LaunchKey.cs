using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchKey : MonoBehaviour
{
    private bool isLaunched;private bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {
        isLaunched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.TryGetComponent(out DoorBehaviour door))
        {
            if (door.GetIsBoss())
            {
                other.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            }

        }

        if (other.TryGetComponent(out PlayerController playerC))
        {
            this.gameObject.transform.SetParent(playerC.gameObject.transform);
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                
                this.gameObject.transform.SetParent(collision.gameObject.transform);
                transform.position = new Vector2(0f, 0f);
                pickedUp = true;
            }
            
        }
    }
}
