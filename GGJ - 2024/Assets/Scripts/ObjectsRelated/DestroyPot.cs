using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPot : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update

    [SerializeField] private GameObject inventaire;
    [SerializeField] private ParticleSystem particle;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void Damage(float dmg)
    {
        particle.gameObject.SetActive(true);
        particle.Play();
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        inventaire.transform.position = this.gameObject.transform.position;
        inventaire.SetActive(true);
    }
}