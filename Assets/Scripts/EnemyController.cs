using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float stoppingDistance;
    public Transform target;
    private Quaternion targetRot;
    private void Start()
    {
    }
    private void Update()
    {
        targetRot = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speedRotation * Time.deltaTime);
    }

    public void Kill()
    {

    }
}
