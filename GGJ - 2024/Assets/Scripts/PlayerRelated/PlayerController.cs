using UnityEngine;

// Maps input and updates scripts of type <PlayerControllable>
public class PlayerController : MonoBehaviour
{
    private enum InputType
    {
        Classic,
        Inversed,
        Rotated90,
        Rotated270
    }

    [Header("Input")]
    [SerializeField]
    private InputType inputType;

    [Header("References")]
    [SerializeField]
    private PlayerControllable controlledScript;


    private bool isPlayerLocked;


    private void OnEnable()
    {
        DialogueHandler.OnDialogueOpenIsPlayerLocked += LockPlayer;
    }

    private void OnDisable()
    {
        DialogueHandler.OnDialogueOpenIsPlayerLocked -= LockPlayer;
    }

    private void Start()
    {
        if(controlledScript == null)
            TryGetComponent(out controlledScript);

        if (controlledScript == null)
            Debug.Log("There is no script to control attached to " + gameObject + " !");
    }

    private void Update()
    {
        SendPlayerInput();
    }

    private void SendPlayerInput()
    {
        PlayerInputObj input;

        if (isPlayerLocked)
        {
            controlledScript.UpdatePlayerInput(new PlayerInputObj(0,0,false));
            return;
        }

        switch (inputType)
        {
            case InputType.Classic:
                input = new(Mathf.Ceil(Input.GetAxis("Horizontal")), Mathf.Ceil(Input.GetAxis("Vertical")), false);
                break;

            case InputType.Inversed:
                input = new(-Mathf.Ceil(Input.GetAxis("Horizontal")), -Mathf.Ceil(Input.GetAxis("Vertical")), false);
                break;

            case InputType.Rotated90:
                input = new(-Mathf.Ceil(Input.GetAxis("Vertical")), Mathf.Ceil(Input.GetAxis("Horizontal")), false);
                break;

            case InputType.Rotated270:
                input = new(Mathf.Ceil(Input.GetAxis("Vertical")), -Mathf.Ceil(Input.GetAxis("Horizontal")), false);
                break;

            default:
                input = new();
                break;
        }

        input.attack = Input.GetKeyDown(KeyCode.Space);


        controlledScript.UpdatePlayerInput(input);
    }

    private void LockPlayer(bool lockPlayer)
    {
        isPlayerLocked = lockPlayer;
    }
}
