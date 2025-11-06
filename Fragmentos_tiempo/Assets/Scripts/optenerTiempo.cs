using UnityEngine;

public class optenerTiempo : MonoBehaviour
{
    public TimerUI timerUI; // Asigna el TimerManager aquí desde el inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("pastilla_larga"))
        {
            timerUI.AddTime(20f);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("pastilla_corta"))
        {
            timerUI.AddTime(10f);
            Destroy(other.gameObject);
        }
    }
}
