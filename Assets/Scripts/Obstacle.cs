using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float destroyOffsetX = 12f;

    private Transform _cameraTransform;

    public void Init(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }

    private void Update()
    {
        if (transform.position.x < _cameraTransform.position.x - destroyOffsetX)
        {
            Destroy(gameObject);
        }
    }
}
