using UnityEngine;

// Maps input and updates scripts of type <PlayerControllable>
public class PlayerController : MonoBehaviour
{
    private enum InputType
    {
        Classic,
        Inversed
    }

    [Header("Input")]
    [SerializeField]
    private InputType inputType;

    [Header("References")]
    [SerializeField]
    private PlayerControllable controlledScript;


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

        switch (inputType)
        {
            case InputType.Classic:
                input = new(Mathf.Ceil(Input.GetAxis("Horizontal")), Mathf.Ceil(Input.GetAxis("Vertical")));
                break;

            case InputType.Inversed:
                input = new(-Mathf.Ceil(Input.GetAxis("Horizontal")), -Mathf.Ceil(Input.GetAxis("Vertical")));
                break;

            default:
                input = new();
                break;
        }

        controlledScript.UpdatePlayerInput(input);
    }
}
