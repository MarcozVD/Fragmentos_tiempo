using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [Header("Punto inicial de respawn")]
    public Transform initialRespawnPoint;

    private Transform currentRespawnPoint;

    void Awake()
    {
        Instance = this;
        currentRespawnPoint = initialRespawnPoint;
    }

    public void SetRespawnPoint(Transform newPoint)
    {
        currentRespawnPoint = newPoint;
        Debug.Log("Nuevo respawn guardado: " + newPoint.name);
    }

    public Transform GetRespawnPoint()
    {
        return currentRespawnPoint;
    }
}
