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
        ExPlodeStart();
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
    void ExPlodeStart()
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, 20);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();

            if (r != null && r.transform.root.transform.GetComponentInChildren<Enemy>())
            {
                r.transform.root.transform.GetComponentInChildren<Enemy>().RagDollOnAdForce();
                //  r.transform.parent.transform.GetComponentInChildren<Enemy>().RagDollOn();
                //   r.AddExplosionForce(power,transform.position, ImpactRadius);
                // r.AddForce(new Vector3(Random.Range(-18f, 18f),0, Random.Range(-18f, 18f)).normalized * power);
            }
        }
    }
}
