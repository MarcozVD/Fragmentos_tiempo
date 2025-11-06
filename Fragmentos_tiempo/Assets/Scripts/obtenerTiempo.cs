using UnityEngine;

public class obtenerTiempo : MonoBehaviour
{
    public Cronometro timerUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pastilla_larga"))
        {
            timerUI.AddTime(15f);
            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
        }
        else if (other.CompareTag("pastilla_corta"))
        {
            timerUI.AddTime(7f);
            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
        }
    }
}
