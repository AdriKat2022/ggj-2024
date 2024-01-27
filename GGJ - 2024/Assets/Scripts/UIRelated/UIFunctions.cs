using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [Tooltip("Your options screen")] [SerializeField] private GameObject Settings;
    private bool onPause;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !onPause)
        {
            Time.timeScale = 0;
            onPause = true;
            Settings.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && onPause)
        {
            onPause = false;
            Time.timeScale = 1;
            Settings.SetActive(false);
        }
    }

}
