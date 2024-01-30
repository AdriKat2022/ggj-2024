using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RoiBehaviour : MonoBehaviour
{
    [SerializeField] private RoiDemonBehaviour demonBehaviour;
    [SerializeField] private GameObject player;
    Rigidbody2D rb;
    [SerializeField] private Image blackScreen;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        demonBehaviour.demonHasBeenKilled.AddListener(Handler);
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
        player.GetComponent<Rigidbody2D>().mass = 1;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().freezeRotation =  false;
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(25, -25), ForceMode2D.Impulse);
        player.GetComponent<Rigidbody2D>().AddTorque(10, ForceMode2D.Impulse);
        StartCoroutine(FadeTo(1.0f, 3.0f));
    }
    
            
   
    
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = blackScreen.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            blackScreen.color = newColor;
            yield return null;
        }
    }
}
