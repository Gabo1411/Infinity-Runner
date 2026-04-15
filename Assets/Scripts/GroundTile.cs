using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{ 
    
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject coinPrefab;

    GroundSpawner groundSpawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
        
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

   
    public void spawnObstacle()     
    {
        // Spawn más agresivo: 5% para 0, 35% para 1, 60% para 2 obstáculos
        float chance = Random.Range(0f, 100f);
        int obstaclesToSpawn = 0;
        
        if (chance <= 5f) {
            obstaclesToSpawn = 0;
        } else if (chance <= 40f) {
            obstaclesToSpawn = 1;
        } else {
            obstaclesToSpawn = 2;
        }

        if (obstaclesToSpawn == 0) return;

        // Índices de los hijos donde aparecen obstáculos (carriles 2, 3 y 4)
        List<int> lanes = new List<int> { 2, 3, 4 };

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            // Selecciona un carril de la lista y lo remueve para no repetir
            int listIndex = Random.Range(0, lanes.Count);
            int obstacleSpawnIndex = lanes[listIndex];
            lanes.RemoveAt(listIndex);

            Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
            Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }

    

    public void spawnCoin()
    {
        // Generar 1 o 2 grupos de monedas para llenar más la pantalla y no dejar huecos largos
        int linesToSpawn = Random.Range(1, 3);
        List<int> availableLanes = new List<int> { 0, 1, 2 };
        Collider collider = GetComponent<Collider>();
        
        float startZ = collider.bounds.min.z + 2f; 
        float endZ = collider.bounds.max.z - 2f; 
        float distanciaLibre = endZ - startZ;
        float[] lanePositions = { -3f, 0f, 3f };

        for (int line = 0; line < linesToSpawn; line++)
        {
            int coinsToSpawn = Random.Range(6, 12); // Grupos un poco más densos
            float spacing = distanciaLibre / (coinsToSpawn - 1); 

            // Seleccionar un carril sin repetir
            int listIndex = Random.Range(0, availableLanes.Count);
            int selectedLane = availableLanes[listIndex];
            availableLanes.RemoveAt(listIndex);

            float laneX = transform.position.x + lanePositions[selectedLane];

            for (int i = 0; i < coinsToSpawn; i++)
            {
                GameObject temp = Instantiate(coinPrefab, transform);  
                float spawnZ = startZ + (i * spacing);
                temp.transform.position = new Vector3(laneX, 1f, spawnZ); 

                Coin coinScript = temp.GetComponent<Coin>();
                if (coinScript != null)
                {
                    coinScript.SetCascadeIndex(i);
                }
            }
        }
    }

    public void SpawnEntranceObstacles()
    {
        // Spawnea siempre de forma fija 2 obstáculos: Uno a la izquierda (index 2) y otro a la derecha (index 4)
        Transform spawnLeft = transform.GetChild(2).transform;
        Transform spawnRight = transform.GetChild(4).transform;

        Instantiate(obstaclePrefab, spawnLeft.position, Quaternion.identity, transform);
        Instantiate(obstaclePrefab, spawnRight.position, Quaternion.identity, transform);
    }
}
