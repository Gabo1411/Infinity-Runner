using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public static GameManager Inst; 

    [SerializeField] TMP_Text scoreText;

    public void IncrementScore ()
    {
        score++;
        scoreText.text = "Puntos: " + score;
    }

    private void Awake()
    {
        Inst = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
