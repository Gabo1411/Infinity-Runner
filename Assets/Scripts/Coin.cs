using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float holdTime = 1f;      // Tiempo que se queda quieta
    [SerializeField] float spinTime = 0.4f;    // Tiempo que tarda en dar el giro
    [SerializeField] float cascadeDelay = 0.15f; // Retraso entre moneda y moneda

    private int cascadeIndex = 0;
    private float timer = 0f;
    private float currentAngle = 0f;
    private Quaternion initialRotation;

    public void SetCascadeIndex(int index)
    {
        cascadeIndex = index;
        timer = -(cascadeIndex * cascadeDelay); // Cada moneda en la fila espera un poco más
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.name != ("Player"))
        {
            return;
        }

        GameManager.Inst.IncrementScore();
        Destroy(gameObject);
    }

    void Start()
    {
        initialRotation = transform.rotation;
        if (timer == 0f) timer = -(cascadeIndex * cascadeDelay); // Asegurar el retraso si se llama desde Start
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Fase de pausa o espera inicial
        if (timer < holdTime)
        {
            if (timer >= 0)
            {
                // Asegurar que la rotación se mantenga fija en el ángulo actual durante la pausa
                transform.rotation = initialRotation * Quaternion.Euler(0, 0, currentAngle);
            }
            return;
        }

        // Fase de giro
        float spinProgress = (timer - holdTime) / spinTime;
        
        if (spinProgress >= 1f)
        {
            // El giro ha terminado
            timer -= (holdTime + spinTime); // Reiniciar ciclo manteniendo la precisión del tiempo
            currentAngle += 180f; 
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, currentAngle); 
        }
        else
        {
            // Interpolar suavemente el ángulo de giro (SmoothStep da un efecto de acelerar y frenar)
            float t = Mathf.SmoothStep(0f, 1f, spinProgress);
            float angle = Mathf.Lerp(currentAngle, currentAngle + 180f, t);
            transform.rotation = initialRotation * Quaternion.Euler(0, 0, angle);
        }
    }
}
