using UnityEngine;

public class DoorBehaviour : MonoBehaviour, IDamageable
{



    [SerializeField] private TriggerWithPlayer TriggerName;
    
    [SerializeField] private TriggerWithPlayer TriggerBar;
    [SerializeField] private bool isBoss;
    [SerializeField] private ParticleSystem particle;


    public bool IsBoss()
    {
        return isBoss;
    }

    public void Damage(int dmg)
    {
        if (!isBoss)
            gameObject.SetActive(false);
        
    }

    private void OnDisable()
    {
        particle.gameObject.transform.position = this.gameObject.transform.position;
        particle.Play();
        if (isBoss)
        {
            TriggerName.NameTriggeredEvent.Invoke(false);
            TriggerBar.BarTriggeredEvent.Invoke(false);

            this.gameObject.SetActive(false);
        }
    }
}
