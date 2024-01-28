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
        if (other.TryGetComponent(out RoiDemonBehaviour roiDemon))
        {
            StartCoroutine(roiDemon.GetLaunched());

        }
        if (other.TryGetComponent(out RoiBehaviour roi))
        {
            this.transform.parent.position = roi.gameObject.transform.position - new Vector3(0, 1, 0);
            this.transform.parent.GetComponent<Rigidbody2D>().mass = 0;
            StartCoroutine(GetPushed());
        }

        IEnumerator GetPushed()
        {
            yield return new WaitForEndOfFrame();
            this.transform.parent.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * 0.03f);
            StartCoroutine(GetPushed());
        }
    }
}
