using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    DialogueHandler dialogueHandler;
    SoundManager soundManager;

    private void Start()
    {
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
        isActive = false;
        dialogueHandler = DialogueHandler.Instance;
        soundManager = SoundManager.Instance;
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
        else if (other.TryGetComponent(out RoiDemonBehaviour roiDemon))
        {
            StartCoroutine(roiDemon.GetLaunched());
            dialogueHandler.Interrupt(DialogueHandler.EventType.All);
            soundManager.PlaySound(soundManager.punch);



        }
        else if (other.TryGetComponent(out RoiBehaviour roi))
        {
            this.transform.parent.position = roi.transform.position - new Vector3(0, 1, 0);
            roi.Punch();
            
            soundManager.PlaySound(soundManager.punch);

            StartCoroutine(GetPushed());
        }

        IEnumerator GetPushed()
        {
            soundManager.StopMusic();
            yield return new WaitForSeconds(3);
            soundManager.StopMusic();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
    }
}
