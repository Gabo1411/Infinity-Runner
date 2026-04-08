using UnityEngine;
public class GroundSpawner : MonoBehaviour

{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;
    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            temp.GetComponent<GroundTile>().spawnObstacle();
            temp.GetComponent<GroundTile>().spawnCoin();
        }
    }

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
                SpawnTile(false);
            else
                SpawnTile(true); 
        }
    }
}
