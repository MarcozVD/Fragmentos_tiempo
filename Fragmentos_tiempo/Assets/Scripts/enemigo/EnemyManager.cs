using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<EnemyData> enemies = new List<EnemyData>();

    [System.Serializable]
    public class EnemyData
    {
        public GameObject enemy;
        public Vector3 startPos;
        public Quaternion startRot;
    }

    private void Awake()
    {
        Instance = this;

        // Buscar todos los enemigos por TAG
        GameObject[] foundEnemies = GameObject.FindGameObjectsWithTag("enemigo");

        foreach (var e in foundEnemies)
        {
            enemies.Add(new EnemyData
            {
                enemy = e,
                startPos = e.transform.position,
                startRot = e.transform.rotation
            });
        }
    }

    public void ResetEnemies()
    {
        foreach (var data in enemies)
        {
            if (data.enemy != null)
            {
                data.enemy.transform.position = data.startPos;
                data.enemy.transform.rotation = data.startRot;

                // Reactivar si estaban desactivados
                data.enemy.SetActive(true);

                // Resetear IA si tienes scripts tipo Patrulla o Chase
                var nav = data.enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (nav != null)
                    nav.Warp(data.startPos);
            }
        }
    }
}
