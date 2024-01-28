using UnityEngine;

public class SpinAndScale : MonoBehaviour
{
    private enum Axis
    {
        X, Y, Z
    }


    [SerializeField]
    private Axis spinningAxis;
    [SerializeField]
    private Vector3 baseRotation;
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private float baseScale;
    [SerializeField]
    private float scaleDepth;
    [SerializeField]
    private float scaleSpeed;


    private void Update()
    {
        Turn();
        Scale();
    }


    private void Turn()
    {
        switch (spinningAxis)
        {
            case Axis.X:
                transform.rotation = Quaternion.Euler(baseRotation.x + Time.time * spinSpeed, baseRotation.y, baseRotation.z);
                break;

            case Axis.Y:
                transform.rotation = Quaternion.Euler(baseRotation.x, baseRotation.y + Time.time * spinSpeed, baseRotation.z);
                break;

            case Axis.Z:
                transform.rotation = Quaternion.Euler(baseRotation.x, baseRotation.y, baseRotation.z + Time.time * spinSpeed);
                break;
        }
        
    }

    private void Scale()
    {
        transform.localScale = (baseScale + Mathf.Cos(Time.time * scaleSpeed) * scaleDepth) * Vector3.one;
    }

}
