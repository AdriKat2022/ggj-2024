using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoiBehaviour : MonoBehaviour
{
    [SerializeField] private RoiDemonBehaviour demonBehaviour;
    [SerializeField] private GameObject player;
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
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Punch()
    {
        player.transform.parent.position = this.gameObject.transform.position - new Vector3(0, 1, 0);
        player.transform.parent.GetComponent<Rigidbody2D>().mass = 0;
        StartCoroutine(GetPushed());
    }

    IEnumerator GetPushed()
    {
        yield return new WaitForEndOfFrame();
        this.transform.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 0.03f);
        StartCoroutine(GetPushed());
    }
}
