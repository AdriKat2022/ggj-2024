using UnityEngine;

public class DoorBehaviour : MonoBehaviour, IDamageable
{



    
    [SerializeField] private GameObject TriggerBar;
    [SerializeField] private bool isBoss;
    [SerializeField] private ParticleSystem particle;

    private SoundManager soundM;

    private void Start()
    {
        soundM = FindObjectOfType<SoundManager>();
    }
    public bool IsBoss()
    {
        return isBoss;
    }

    public void Damage(int dmg)
    {
        if (!isBoss)
            soundM.PlaySound(soundM.totalDestruction);

            gameObject.SetActive(false);
        
    }

    private void OnDisable()
    {
        particle.gameObject.transform.position = this.gameObject.transform.position;
        particle.Play();
        if (isBoss)
        {
            TriggerBar.SetActive(true);

            this.gameObject.SetActive(false);
        }
    }
}
