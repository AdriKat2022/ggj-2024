using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Max offsets")]
    [SerializeField]
    private Vector2 maxOffset;
    [SerializeField]
    private float maxAngle;


    [Header("Stress")]
    [SerializeField]
    private float maxStress;
    [SerializeField]
    private float stressRate;


    [Header("Camera")]
    [SerializeField]
    private GameObject shakedObject;

    private bool canShake;
    private float stress;
    private IEnumerator manageShake_CR;

    [HideInInspector]
    public readonly float SmallShake = .1f;
    [HideInInspector]
    public readonly float ModerateShake = .3f;
    [HideInInspector]
    public readonly float StrongShake = .5f;



    public void CanShake(bool can) => canShake = can;

    public void AddStress(float stress)
    {
        this.stress += stress;
        this.stress = Mathf.Clamp(stress, 0, 1);
    }


    private void Start()
    {
        manageShake_CR = ManageShake();
        StartCoroutine(manageShake_CR);
        canShake = true;
    }

    private IEnumerator ManageShake()
    {
        while (true)
        {
            if (canShake)
            {
                Shake(stress * stress);

                stress -= Time.deltaTime * stressRate;
                stress = Mathf.Clamp(stress, 0, 1);
            }

            yield return null;
        }
    }

    private void Shake(float strength)
    {
        Vector3 computedPosition = Vector3.zero;

        computedPosition.x = Random.Range(-maxOffset.x * strength, maxOffset.x * strength);
        computedPosition.y = Random.Range(-maxOffset.y * strength, maxOffset.y * strength);

        Quaternion computedRotation = Quaternion.Euler(0, 0, Random.Range(-maxAngle * strength, maxAngle * strength));

        shakedObject.transform.SetLocalPositionAndRotation(computedPosition, computedRotation);
    }
}
