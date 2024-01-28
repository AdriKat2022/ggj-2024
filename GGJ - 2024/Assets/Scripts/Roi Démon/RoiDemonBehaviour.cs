using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoiDemonBehaviour : MonoBehaviour
{
    [SerializeField] private TriggerWithPlayer TriggerName;

    [SerializeField] private TriggerWithPlayer TriggerBar;
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private GameObject player;
    [SerializeField] private float launchDuration;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float torqueForce;

    public UnityEvent<bool> demonHasBeenKilled;

    public bool isKing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        particle.gameObject.transform.position = this.gameObject.transform.position;
        particle.Play();
        
        TriggerName.NameTriggeredEvent.Invoke(false);
        TriggerBar.BarTriggeredEvent.Invoke(false);

     
    }

    public IEnumerator GetLaunched()
    {
        float elapsedTime = 0f;
        Vector3 moveDirection = Vector3.up;
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.mass = 1;
        Debug.Log(launchSpeed);
        TriggerName.NameTriggeredEvent.Invoke(false);
        TriggerBar.BarTriggeredEvent.Invoke(false);
        particle.transform.position = this.transform.position;
        particle.Play();
        
        while (elapsedTime < launchDuration)
        {
            // Déplace l'objet dans la direction spécifiée
            
            rigidbody.AddForce(launchSpeed * moveDirection);

            // Tourne l'objet constamment
            rigidbody.AddTorque(torqueForce);

            // Incrémente le temps écoulé
            elapsedTime += Time.deltaTime;

            // Attend la prochaine frame

            demonHasBeenKilled?.Invoke(true);

            yield return null;

            print(elapsedTime);
        }
        if (isKing)
        {
            SceneManager.LoadScene("League of Legends");
        }
        this.gameObject.SetActive(false);
    }

    
}
