using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;          // El personaje a seguir
    public Vector3 offset = new Vector3(0f, 2f, -4f); // Posición relativa

    [Header("Rotación")]
    public float mouseSensitivity = 100f;
    public float minPitch = -35f;  // Límite inferior (mirar hacia abajo)
    public float maxPitch = 60f;   // Límite superior (mirar hacia arriba)

    private float yaw;   // Rotación horizontal
    private float pitch; // Rotación vertical

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Inicializa los ángulos
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Entrada del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY; // se resta porque mirar hacia arriba baja el mouse
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotación de cámara
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Posición de cámara
        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;

        // Mira siempre al personaje
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
