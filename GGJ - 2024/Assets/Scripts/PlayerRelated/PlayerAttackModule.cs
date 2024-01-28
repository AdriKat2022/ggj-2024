using System.Collections;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour
{
    [SerializeField]
    private int damage = 999;
    [SerializeField]
    private float startUpTime = .2f;
    [SerializeField]
    private float activeTime = .1f;

    private bool isActive;
    private float timer;

    private Collider2D hitbox;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
        isActive = false;
    }

    public void UpdateRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    public void ActivateModule()
    {
        StartCoroutine(ActivateAttackModule());
    }

    public void SetDamage(int damage) => this.damage = damage;

    private IEnumerator ActivateAttackModule()
    {
        if(isActive)
            yield break;

        isActive = true;

        yield return new WaitForSeconds(startUpTime);

        hitbox.enabled = true;
        timer = activeTime;

        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        hitbox.enabled = false;
        isActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage);
        }
    }
}
