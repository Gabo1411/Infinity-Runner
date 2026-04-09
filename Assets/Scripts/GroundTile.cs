using UnityEngine;

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
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
    }

    

    public void spawnCoin()
    {
        // Intercalar grupos de 7 u 8 monedas
        int coinsToSpawn = Random.Range(7, 9);
        Collider collider = GetComponent<Collider>();
        
        // Escoger aleatoriamente un carril para las monedas. 
        // Coincide con el "laneDistance" de 3f que asignamos al player
        float[] lanePositions = { -3f, 0f, 3f };
        int selectedLane = Random.Range(0, 3);
        float laneX = transform.position.x + lanePositions[selectedLane];
        
        // Calcular la separación a lo largo de la pieza de suelo
        float startZ = collider.bounds.min.z + 2f; // Margen inicial
        float endZ = collider.bounds.max.z - 2f; // Margen final
        float distanciaLibre = endZ - startZ;
        float spacing = distanciaLibre / (coinsToSpawn - 1); // Separación notable pero sutil

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);  
            float spawnZ = startZ + (i * spacing);
            temp.transform.position = new Vector3(laneX, 1f, spawnZ); // y = 1f es la altura estándar del jugador/monedas

            // Asignamos el índice para el efecto de giro en cascada
            Coin coinScript = temp.GetComponent<Coin>();
            if (coinScript != null)
            {
                coinScript.SetCascadeIndex(i);
            }
        }
    }

}
