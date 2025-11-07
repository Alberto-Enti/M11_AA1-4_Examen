using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public struct Speed
{
    public float walk;
    public float run;

    public Speed(float w, float r)
    {
        walk = w;
        run = r;
    }
}

public class EnemyController : MonoBehaviour
{
    public float speedRotation;
    public float stoppingDistance;
    private Transform target;
    public Rigidbody rb;
    private ZombieManager zombieManager;
    private Animator animator;
    private Quaternion targetRot;
    private CapsuleCollider capsuleCollider;
    public float playerDistance;
    public bool isKilled = false;

    [SerializeField]
    private Speed speeds;

    public float tmpSpeed;
    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        zombieManager = transform.parent.GetComponent<ZombieManager>();
        target = zombieManager.player.transform;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isKilled)
        {
            Kill();
        }
        GetPlayerDistance();
        ControlRotation();
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public void Kill()
    {
        Destroy(rb);
        Destroy(animator);
        Destroy(capsuleCollider);
        Destroy(this);
    }

    private void ControlRotation()
    {
        targetRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speedRotation * Time.deltaTime);
    }

    private void MoveEnemy()
    {
        if(playerDistance <= 3)
        {
            rb.linearVelocity = transform.forward * Mathf.Lerp(0 , speeds.walk, playerDistance / 3) * Time.fixedDeltaTime;
        }
        else
        {
            rb.linearVelocity = transform.forward * Mathf.Lerp(speeds.walk, speeds.run, playerDistance / 20) * Time.fixedDeltaTime;
        }
        animator.SetFloat("Velocity", rb.linearVelocity.magnitude);
    }

    private void GetPlayerDistance()
    {
        playerDistance = Vector3.Distance(transform.position, target.position);
    }


} 
