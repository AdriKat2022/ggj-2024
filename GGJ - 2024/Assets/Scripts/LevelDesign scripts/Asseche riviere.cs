using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Assecheriviere : MonoBehaviour
{
    [SerializeField] private Transform rivierePosition;
    [SerializeField] private RuleTile assecheTile;


    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>(); 
    }

    private void Update()
    {
        Debug();
    }

    public void Asseche()
    {
        tilemap.FloodFill(tilemap.WorldToCell(rivierePosition.position), assecheTile);
    }

    public void Debug()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Asseche();
        }
    }
}
