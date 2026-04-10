using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/Player/PlayerController.cs
    void Start()
    {
        
    }

    void Update()
=======
    [SerializeField] private Transform target;
    [SerializeField] private float offsetX = 3f;

    void LateUpdate()
>>>>>>> Stashed changes:Assets/Scripts/CameraFollow.cs
    {
        transform.position = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
    }
}
