using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public GameObject thisGuysRig;
    Collider[] ragCol;
    Rigidbody[] limbsRb;
    private void Start()
    {
        
          

        
    }
    private void OnEnable()
    {
        BlastNow();
    }
    public void  BlastNow()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();
        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.isKinematic = false;
            rigid.AddExplosionForce(FindObjectOfType<Projectile>().power+500, FindObjectOfType<Projectile>().transform.position, FindObjectOfType<Projectile>().ImpactRadius+100);

        }
    }
}
