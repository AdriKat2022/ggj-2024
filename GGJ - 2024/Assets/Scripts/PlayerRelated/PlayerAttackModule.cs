using System.Collections;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour
{
    [SerializeField]
    private float activeTime;

    private bool isActive;
    private float timer;
    private float damage;


    private void onEnable()
    {
        isActive = false;
        timer = 0;
    }

    private void Update()
    {
        if (!isActive)
            return;

    }

    public void UpdateRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public void ActivateModule(float damage)
    {
        this.damage = damage;
        StartCoroutine(ActivateAttackModule());
    }

    private IEnumerator ActivateAttackModule()
    {
        timer = activeTime;

        if(isActive)
            yield break;

        isActive = true;

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        this.gameObject.SetActive(false);
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage);
            print("isDamageable");
        }

        if(other.TryGetComponent(out DoorBehaviour door))
        {
            if (!door.GetIsBoss())
            {
                other.gameObject.SetActive(false);
            }
            
        }
    }
}
