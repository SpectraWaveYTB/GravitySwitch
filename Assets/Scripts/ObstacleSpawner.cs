using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public enum ObstaclePosition
    {
        Floor,
        Ceiling
    }

    [System.Serializable]
    public class ObstaclePattern
    {
        public string Name;
        public ObstaclePosition[] Steps;
    }

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float spawnOffsetX = 12f;
    [SerializeField] private float distanceMinEasy = 6f;
    [SerializeField] private float distanceMaxEasy = 10f;
    [SerializeField] private float distanceMinHard = 4f;
    [SerializeField] private float distanceMaxHard = 7f;
    [SerializeField] private float phaseTransitionTime = 30f;

    private float _nextSpawnX;
    private int _lastPatternIndex;
    private int _stepInPattern;
    private ObstaclePattern _currentPattern;
    private ObstaclePattern _patternA;
    private ObstaclePattern _patternB;
    private ObstaclePattern _patternC;

    public static ObstacleSpawner Instance { get; private set; }

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

        _patternA = new ObstaclePattern
        {
            Name = "PatternA",
            Steps = new ObstaclePosition[]
            {
                ObstaclePosition.Floor,
                ObstaclePosition.Ceiling,
                ObstaclePosition.Floor,
                ObstaclePosition.Ceiling,
            }
        };
        _patternB = new ObstaclePattern
        {
            Name = "PatternB",
            Steps = new ObstaclePosition[]
    {
            ObstaclePosition.Floor,
            ObstaclePosition.Floor,
            ObstaclePosition.Ceiling
    }
        };

        _patternC = new ObstaclePattern
        {
            Name = "PatternC",
            Steps = new ObstaclePosition[]
            {
            ObstaclePosition.Ceiling,
            ObstaclePosition.Floor,
            ObstaclePosition.Ceiling
            }
        };

        _lastPatternIndex = -1;
    }

    private void Start()
    {
        _nextSpawnX = cameraTransform.position.x + spawnOffsetX + 20f;
    }

    private void Update()
    {
        if (cameraTransform.position.x + spawnOffsetX >= _nextSpawnX)
        {
            SpawnNext();
        }
    }

    private void SpawnNext()
    {
        if (_currentPattern == null || _stepInPattern >= _currentPattern.Steps.Length)
        {
            PickPattern();
        }
        ObstaclePosition step = _currentPattern.Steps[_stepInPattern];

        float spawnY;
        if (step == ObstaclePosition.Floor)
        {
            spawnY = -3f;
        }
        else
        {
            spawnY = 3f;
        }

        GameObject obstacleInstance = Instantiate(obstaclePrefab, new Vector3(_nextSpawnX, spawnY, 0f), Quaternion.identity);

        obstacleInstance.transform.localScale = new Vector3(1f, 3f, 1f);

        Obstacle obstacleScript = obstacleInstance.GetComponent<Obstacle>();

        obstacleScript.Init(cameraTransform);

        float t = GameManager.Instance.SurvivalTime;
        float blend = Mathf.Clamp01(t / phaseTransitionTime);

        float distMin = Mathf.Lerp(distanceMinEasy, distanceMinHard, blend);
        float distMax = Mathf.Lerp(distanceMaxEasy, distanceMaxHard, blend);

        float gap = Random.Range(distMin, distMax);

        _nextSpawnX += gap;

        _stepInPattern++;
    }

    private void PickPattern()
    {
        float t = GameManager.Instance.SurvivalTime;
        ObstaclePattern[] pool;

        if (t < 20)
        {
            pool = new ObstaclePattern[] { _patternA };
        }

        else if (t < 40)
        {
            pool = new ObstaclePattern[] { _patternA, _patternB };
        }

        else
        {
            pool = new ObstaclePattern[] { _patternA, _patternB, _patternC };
        }

        int chosenIndex;

        if (pool.Length == 1)
        {
            chosenIndex = 0;
        }

        else
        {
            chosenIndex = Random.Range(0, pool.Length - 1);

            while (chosenIndex == _lastPatternIndex)
            {
                chosenIndex = Random.Range(0, pool.Length);
            }
        }

        _currentPattern = pool[chosenIndex];

        _lastPatternIndex = chosenIndex;

        _stepInPattern = 0;
    }
}