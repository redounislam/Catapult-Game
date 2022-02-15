using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ragdollOn : MonoBehaviour
{
    public GameObject thisGuysRig;
    Collider[] ragCol;
    Rigidbody[] limbsRb;


    public float IncreaseAmmount;

    public float radius = 5.0F;
    public float power = 10.0F;

    void ExPlodeStart()
    {
        Collider[] objects = UnityEngine.Physics.OverlapSphere(transform.position, 100);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(1000,transform.position, 1000);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExPlodeStart();
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }




    void GetRagDollBits()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();

    }
    void RagDollOn()
    {

        thisGuysRig.GetComponent<Rigidbody>().mass = 10f; 
        thisGuysRig.GetComponent<Collider>().enabled = false;
        
        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.mass = 1f;
            rigid.angularDrag = 0; rigid.drag = 0;
            rigid.isKinematic = false;
            
           rigid.velocity =FindObjectOfType<CannonController>(). ShotPoint.transform.up * FindObjectOfType<CannonController>().BlastPower;
        }
    //   GetComponent<Collider>().enabled = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
       
    }


    IEnumerator Ragdoll()
    {
        yield return new WaitForSeconds(0f);
        GetRagDollBits();
        RagDollOn();
    }
    private void OnEnable()
    {
        StartCoroutine(Ragdoll());
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100, ForceMode.Impulse);
            other.tag = "Untagged";
        }
        if (other.gameObject.CompareTag("Collectable"))
        {
            this.transform.DOScale(new Vector3(this.transform.localScale.x + IncreaseAmmount, this.transform.localScale.y + IncreaseAmmount,
       this.transform.localScale.z + IncreaseAmmount), .3f).SetEase(Ease.InOutElastic);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().RagDollOn();
            other.GetComponent<Enemy>().Death = true;

            other.GetComponent<Enemy>().Shiled.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
            other.GetComponent<Enemy>().Shiled.transform.gameObject.AddComponent<BoxCollider>();
            other.GetComponent<Enemy>().Sword.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
            other.GetComponent<Enemy>().Sword.transform.gameObject.AddComponent<BoxCollider>();

            other.GetComponent<Enemy>().Spear.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
            other.GetComponent<Enemy>().Spear.transform.gameObject.AddComponent<BoxCollider>();
            other.GetComponent<Enemy>().Helmet.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
            other.GetComponent<Enemy>().Helmet.transform.gameObject.AddComponent<BoxCollider>();

            other.GetComponent<Enemy>().Shiled.transform.parent = null;
            other.GetComponent<Enemy>().Sword.transform.parent = null;
            other.GetComponent<Enemy>().Spear.transform.parent = null;
            other.GetComponent<Enemy>().Helmet.transform.parent = null;


        }
    }

}
