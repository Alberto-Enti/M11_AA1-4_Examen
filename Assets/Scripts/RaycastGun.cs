using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class RaycastGun : MonoBehaviour
{
    public LineRenderer line;
    public float lineFadeSpeed;
    public LayerMask mask;
    public float knockbackForce = 10;
    private RaycastHit hit;
    private Vector3 hitPos;
    private bool hasHit;
    void Update()
    {
        line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, line.startColor.a - Time.deltaTime * lineFadeSpeed);
        line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, line.endColor.a - Time.deltaTime * lineFadeSpeed);

        if (Input.GetButtonDown("Fire1"))
        {
            line.startColor = new Color(line.startColor.r, line.startColor.g, line.startColor.b, 1);
            line.endColor = new Color(line.endColor.r, line.endColor.g, line.endColor.b, 1);

            line.SetPosition(0, transform.position);
            Shot();
            if (hasHit)
            {
            line.SetPosition(1, hitPos);
            hasHit = false;
            }
            else
            {
            line.SetPosition(1, transform.position + transform.forward * 1000);
            }
        }
    }

    void Shot()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hit, 1000f, ~LayerMask.GetMask("IgnoreRaycast"))){
            hitPos = hit.point;
            hasHit = true;

            if(hit.collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100, hit.point, 30);
            }

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyController>().Kill();
            }
        }
        else
        {
            hitPos = new Vector3(0, 0, 0);
        }
    }
}
