using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;   

    public float speed = 5;
    public float speedIncrement = 0.1f;
    public float maxSpeed = 15f;
    [SerializeField] Rigidbody rb;
    
    // NUEVO: Agregamos la referencia al Animator para controlar las animaciones
    [SerializeField] Animator anim; 

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
        // NUEVO: Evitamos que el código de muerte se ejecute más de una vez
        if (!alive) return; 

        alive = false;

        // NUEVO: Disparamos la animación de muerte
        if (anim != null) 
        {
            anim.SetTrigger("Die");
        }

        // Mostrar menú de derrota a través del GameManager
        GameManager.Inst.GameOver();
    }

    public void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            speed += speedIncrement;
        }
    }
}