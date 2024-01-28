using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SousGun : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject munitionPrefab;
    [SerializeField] private Transform origin;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float shootDelay;
    [SerializeField] private float munitionSpeed;
    [SerializeField] private float destroyDelay;
    [SerializeField] private float destroyMunitionDelay;

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        rb.AddTorque(rotateSpeed, ForceMode2D.Impulse);
        StartCoroutine(Shoot());
        Destroy(gameObject, destroyDelay);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator Shoot()
    {
        GameObject munition = Instantiate(munitionPrefab, origin.position, Quaternion.Euler(transform.eulerAngles));
        munition.GetComponent<Rigidbody2D>().AddForce(transform.right * munitionSpeed, ForceMode2D.Impulse);
        Destroy(munition, destroyMunitionDelay);

        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(Shoot());

    }
}
