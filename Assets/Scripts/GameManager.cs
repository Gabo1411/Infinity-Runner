using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager Inst; 

    [SerializeField] TMP_Text scoreText;

    [Header("Paneles de UI")]
    [SerializeField] public GameObject mainMenuUI;
    [SerializeField] public GameObject gameOverUI;

    private PlayerMovement player;

    public void IncrementScore ()
    {
        score++;
        scoreText.text = "Puntos: " + score;

        if (player != null)
        {
            player.IncreaseSpeed();
        }
    }

    private void Awake()
    {
        Inst = this;
    }
    
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        
        // Pausar el juego al inicio y mostrar el menú
        Time.timeScale = 0f;
        if (mainMenuUI != null) mainMenuUI.SetActive(true);
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (mainMenuUI != null) mainMenuUI.SetActive(false);
    }

    public void GameOver()
    {
        if (gameOverUI != null) gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        // Reactivamos la velocidad (aún cuando Start() la pone en 0)
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detener en el editor
        #endif
    }
}
