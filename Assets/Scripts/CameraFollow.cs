using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetX = 3f;

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offsetX, transform.position.y, transform.position.z);
    }
}
