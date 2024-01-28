using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoiBehaviour : MonoBehaviour
{
    [SerializeField] private RoiDemonBehaviour demonBehaviour;
    Rigidbody2D rb;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        demonBehaviour.demonHasBeenKilled.AddListener(Handler);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Handler(bool value)
    {
        StartCoroutine(Wait(1));
       
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        rb.simulated = true;
    }
}
