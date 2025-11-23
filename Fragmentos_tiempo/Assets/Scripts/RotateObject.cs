using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Velocidad de giro")]
    public Vector3 rotationSpeed = new Vector3(0, 100f, 0); 
    // (x, y, z) grados por segundo

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
