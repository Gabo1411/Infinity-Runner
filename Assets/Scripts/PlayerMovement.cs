using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    bool alive = true;   

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    [SerializeField] float laneDistance = 3f; // Distancia entre los carriles
    [SerializeField] float laneChangeSpeed = 15f; // Velocidad para cambiar de carril
    int currentLane = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha

    private void FixedUpdate()
    {
        if (!alive) return;

        // Calcular posición objetivo en X basado en el carril actual
        float targetX = 0f;
        if (currentLane == 0) targetX = -laneDistance;
        else if (currentLane == 2) targetX = laneDistance;

        // Movimiento hacia adelante constante
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        
        // Movimiento lateral suave hacia el carril objetivo
        float newX = Mathf.MoveTowards(rb.position.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);
        
        Vector3 nextPosition = rb.position + forwardMove;
        nextPosition.x = newX;

        rb.MovePosition(nextPosition);
    }
    
    void Update()
    {
        if (!alive) return;

        // Controles para PC (Flechas o A/D)
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentLane++;
            if (currentLane > 2) currentLane = 2;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentLane--;
            if (currentLane < 0) currentLane = 0;
        }

        if (transform.position.y  < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        alive = false;
        Invoke ("Restart", 1);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
