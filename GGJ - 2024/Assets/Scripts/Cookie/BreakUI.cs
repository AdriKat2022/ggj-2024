using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreakUI : MonoBehaviour
{
    [SerializeField] private Image cookieUI;
    [SerializeField] private Sprite breakSprite;
    [SerializeField] private TextMeshProUGUI counter;


    
    public void BreakCounter()
    {
        cookieUI.sprite = breakSprite;
        counter.text = "505";

    }
}
