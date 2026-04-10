using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    void Update()
    {
        scoreText.text = $"TIME {GameManager.Instance.SurvivalTime:F1}s";
    }
}
