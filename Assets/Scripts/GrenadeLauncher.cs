using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    public GameObject grenade;
    private float timer = 0;
    public float explosionTime = 2;
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(grenade, transform.position, transform.rotation);

        }
    }
}
