using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float speedIncreaseRate = 0.3f;
    [SerializeField] private float maxSpeed = 18f;

    /// Current horizontal speed, increases over time up to maxSpeed.
    public float CurrentSpeed { get; private set; }

    /// Current survival time in seconds since last spawn</summary>
    public float SurvivalTime { get; private set; }

    private void Awake()
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

    private void Update()
    {
        SurvivalTime += Time.deltaTime;
        CurrentSpeed = Mathf.Min(baseSpeed + speedIncreaseRate * SurvivalTime, maxSpeed);
    }

    /// Reloads the active scene, resetting all state.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}