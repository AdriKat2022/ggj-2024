using UnityEngine;

// Maps input and updates scripts of type <PlayerControllable>
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public PlayerControllable controlledScript;


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
        PlayerInputObj input = new((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"));
        controlledScript.UpdatePlayerInput(input);
    }
}
