using UnityEngine;

public class cameralook : MonoBehaviour
{
    public Vector3 targetRotation = new Vector3(18.4f, 181.0f, 0); // Rotation you want to reach
    public float rotationSpeed = 5f;                       // How fast it rotates

    private bool shouldRotate = false;
    private Quaternion initialRotation;
    private Quaternion desiredRotation;

    void Start()
    {
        initialRotation = transform.rotation;
        desiredRotation = Quaternion.Euler(targetRotation);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldRotate = true;
        }

        if (shouldRotate)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

            // Stop rotating when close enough
            if (Quaternion.Angle(transform.rotation, desiredRotation) < 0.1f)
            {
                transform.rotation = desiredRotation;
                shouldRotate = false;
            }
        }
    }
}
