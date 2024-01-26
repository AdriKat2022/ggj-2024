using UnityEngine;

public abstract class PlayerControllable : MonoBehaviour
{
    protected PlayerInputObj playerInput;

    public virtual void UpdatePlayerInput(PlayerInputObj playerInput)
    {
        this.playerInput = playerInput;
    }
}
