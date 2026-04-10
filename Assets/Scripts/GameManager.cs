using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float speedIncreaseRate = 0.3f;
    [SerializeField] private float maxSpeed = 18f;

    public static GameManager Instance { get; private set; }
    public float CurrentSpeed { get; private set; }
    public float SurvivalTime { get; private set; }

    private float _survivalTime;
    private float _currentSpeed;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        SurvivalTime += Time.deltaTime;
        CurrentSpeed = Mathf.Min(baseSpeed + speedIncreaseRate * SurvivalTime, maxSpeed);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}