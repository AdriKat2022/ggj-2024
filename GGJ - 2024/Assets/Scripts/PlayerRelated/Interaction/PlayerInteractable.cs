using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField]
    protected bool reusableAfterExitCollision;
    [SerializeField]
    protected LayerMask interactableLayer;

    [Header("Animation")]
    [SerializeField]
    protected GameObject interactableVisual;


    protected bool interactable;
    protected bool hasInteracted;


    protected void Start()
    {
        ToogleInteractable(false);
    }

    protected void Update()
    {
        if (!interactable)
            return;
        OnInteractable();
        CheckInteraction();
    }

    protected void CheckInteraction()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;

        hasInteracted = true;
        ToogleInteractable(false);
        Interact();
    }

    public abstract void Interact();
    public virtual void RefreshInteraction()
    {
        hasInteracted = false;
    }

    /// <summary>
    /// Executes every frame while the player can interact
    /// </summary>
    protected virtual void OnInteractable()
    {

    }
    protected virtual void ToogleInteractable(bool toogle)
    {
        interactable = toogle;
        interactableVisual.SetActive(toogle);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO : Detect if it is the player collision


        if (!hasInteracted && Helper.IsLayerInLayerMask(collision.gameObject.layer, interactableLayer))
        {
            ToogleInteractable(true);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        ToogleInteractable(false);

        if(reusableAfterExitCollision)
            RefreshInteraction();
    }
}
