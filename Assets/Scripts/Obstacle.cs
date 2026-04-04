using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;
     void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void Start()
    {
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovement.Die();

        }    
        //Kill the player

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
