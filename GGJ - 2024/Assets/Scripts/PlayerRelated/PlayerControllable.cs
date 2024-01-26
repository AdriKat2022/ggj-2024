using UnityEngine;

public abstract class PlayerControllable : MonoBehaviour
{
    private PlayerInputObj playerInput;

    public virtual void UpdatePlayerInput(PlayerInputObj playerInput)
    {
        this.playerInput = playerInput;
    }
}
