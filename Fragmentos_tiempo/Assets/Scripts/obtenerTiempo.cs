using UnityEngine;

public class obtenerTiempo : MonoBehaviour
{
    public Cronometro timerUI;

    public FadeText textoPastillaLarga;
    public FadeText textoPastillaCorta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pastilla_larga"))
        {
            timerUI.AddTime(15f);

            if (textoPastillaLarga != null)
                textoPastillaLarga.ShowMessage();

            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
        }
        else if (other.CompareTag("pastilla_corta"))
        {
            timerUI.AddTime(7f);

            if (textoPastillaCorta != null)
                textoPastillaCorta.ShowMessage();

            RespawnPickup pickup = other.GetComponent<RespawnPickup>();
            if (pickup != null) pickup.PickUp();
        }
    }
}
