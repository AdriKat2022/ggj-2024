using UnityEngine;

public class TimeAccelerator : MonoBehaviour
{
    [SerializeField]
    private string fastForwardKey = "f";
    [SerializeField]
    private bool activate = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(activate)
            Debug.LogWarning("WARNING, TIME ACCELERATOR DEBUG TOOL IS PRESENT ON THE SCENE", gameObject);
    }

    void Update()
    {
        if (!activate)
            return;

        if(Input.GetKey(fastForwardKey))
            Time.timeScale = 10.0f;
        else
            Time.timeScale = 1.0f;
    }
}
