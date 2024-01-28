using UnityEngine;

public class DestroyableComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private GameObject effectOnDestroy;
    [SerializeField]
    private AudioClip soundOnDestroy;

    [Header("Dialogues")]
    [SerializeField]
    private bool interruptDialoguesOnDeath;


    private int currentHp;

    public void Damage(int dmg)
    {
        currentHp -= dmg;
        if (currentHp < 0)
            DeleteObject();
    }

    private void DeleteObject()
    {
        if(effectOnDestroy != null)
            Instantiate(effectOnDestroy);
        if (soundOnDestroy != null)
            SoundManager.Instance.PlaySound(soundOnDestroy);

        if (interruptDialoguesOnDeath)
            DialogueHandler.Instance.Interrupt(DialogueHandler.EventType.All);

        Destroy(gameObject);
    }

    private void Start()
    {
        currentHp = maxHp;
    }
}
