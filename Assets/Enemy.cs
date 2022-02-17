using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool EnableSword;
    public bool EnableShield;
    public bool EnableHelmet;
    public bool EnableSpear;
    public GameObject Sword, Shiled, Helmet, Spear;


    public GameObject thisGuysRig;
    Collider[] ragCol;
    Rigidbody[] limbsRb;
    public Collider mainCollider;
    public bool Death;

    void Start()
    {
        if(EnableHelmet)
        {
            Helmet.gameObject.SetActive(true);
        }
        else
            Helmet.gameObject.SetActive(false);

        if (EnableShield)
        {
            Shiled.gameObject.SetActive(true);
        }
        else
            Shiled.gameObject.SetActive(false);

        if (EnableSword)
        {
            Sword.gameObject.SetActive(true);
        }
        else
            Sword.gameObject.SetActive(false);
        if (EnableSpear)
        {
            Spear.gameObject.SetActive(true);
        }
        else
            Spear.gameObject.SetActive(false);

        GetRagDollBits();
        RagDollOff();
    }
    void GetRagDollBits()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();

    }
   public void RagDollOn()
    {

        Death = true;
        foreach (Collider col in ragCol)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.isKinematic = false;
            rigid.AddForce(new Vector3(Random.Range(-18f, 18f), Random.Range(20, 30f), Random.Range(-18f, 18f)).normalized * 150);
        }
        GetComponent<Animator>().enabled = false;
    }
    void RagDollOff()
    {
        foreach (Collider col in ragCol)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in limbsRb)
        {
            rigid.isKinematic = true;
        }
        mainCollider.enabled = true;
        mainCollider.transform.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Animator>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

   
}
