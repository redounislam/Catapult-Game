using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Canvas, PopUp;
    public bool EnableSword;
    public bool EnableShield;
    public bool EnableHelmet;
    public bool EnableSpear; public bool EnableSflag;
    public GameObject Sword, Shiled, Helmet, Spear , Flag;


    public GameObject thisGuysRig;
    Collider[] ragCol;
    Rigidbody[] limbsRb;
    public Collider mainCollider;
    public bool Death;

    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
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

        if (EnableSflag)
        {
            Flag.gameObject.SetActive(true);
            GetComponent<Animator>().SetTrigger("Flag");
        }
        else
        {
            Flag.gameObject.SetActive(false);

        }

        GetRagDollBits();
        RagDollOff();
    }
    void GetRagDollBits()
    {
        ragCol = thisGuysRig.GetComponentsInChildren<Collider>();
        limbsRb = thisGuysRig.GetComponentsInChildren<Rigidbody>();

    }
    bool RagDollPlayed;
   public void RagDollOn()
    {
        if (!RagDollPlayed)
        {
            GameObject g = Instantiate(PopUp, transform.position, Quaternion.identity);
        
           
            Destroy(g, .5f);
            GM.Instance.KillCount++;
            UiManager.Instance.KillCount.text = GM.Instance.KillCount.ToString();
            if (Flag.activeSelf)
            {
                //  Shiled.transform.GetChild(0).gameObject.SetActive(true);
                // Shiled.transform.GetChild(0).transform.parent = null;
                Flag.transform.gameObject.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Flag.transform.gameObject.gameObject.AddComponent<BoxCollider>();
                Flag.transform.parent = null;
            }
            if (Shiled.activeSelf)
            {
              //  Shiled.transform.GetChild(0).gameObject.SetActive(true);
               // Shiled.transform.GetChild(0).transform.parent = null;
                Shiled.transform.gameObject.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Shiled.transform.gameObject.gameObject.AddComponent<BoxCollider>();
               Shiled.transform.parent = null;
            }

            if (Sword.activeSelf)
            {
                Sword.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Sword.transform.gameObject.AddComponent<BoxCollider>(); Sword.transform.parent = null;
            }
            if (Spear.activeSelf)
            {
                Spear.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Spear.transform.gameObject.AddComponent<BoxCollider>(); Spear.transform.parent = null;
            }
            if (Helmet.activeSelf)
            {
                Helmet.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Helmet.transform.gameObject.AddComponent<BoxCollider>(); Helmet.transform.parent = null;
            }




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


            RagDollPlayed = true;
        }
    }
    public void RagDollOnAdForce()
    {
        if (!RagDollPlayed)
        {
            GameObject g = Instantiate(PopUp, transform.position, Quaternion.identity);


            Destroy(g, .5f);
            GM.Instance.KillCount++;
            UiManager.Instance.KillCount.text = GM.Instance.KillCount.ToString();
            if (Shiled.activeSelf)
            {
                //  Shiled.transform.GetChild(0).gameObject.SetActive(true);
                // Shiled.transform.GetChild(0).transform.parent = null;
                Shiled.transform.gameObject.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Shiled.transform.gameObject.gameObject.AddComponent<BoxCollider>();
                Shiled.transform.parent = null;
            }

            if (Sword.activeSelf)
            {
                Sword.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Sword.transform.gameObject.AddComponent<BoxCollider>(); Sword.transform.parent = null;
            }
            if (Spear.activeSelf)
            {
                Spear.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Spear.transform.gameObject.AddComponent<BoxCollider>(); Spear.transform.parent = null;
            }
            if (Helmet.activeSelf)
            {
                Helmet.transform.gameObject.AddComponent<Rigidbody>().AddForce(Vector3.up * 50);
                Helmet.transform.gameObject.AddComponent<BoxCollider>(); Helmet.transform.parent = null;
            }




            Death = true;




            foreach (Collider col in ragCol)
            {
                col.enabled = true;
            }
            foreach (Rigidbody rigid in limbsRb)
            {
                rigid.isKinematic = false;
                rigid.AddExplosionForce(FindObjectOfType<Projectile>().power, FindObjectOfType<Projectile>().transform.position, FindObjectOfType<Projectile>().ImpactRadius);

            }
            GetComponent<Animator>().enabled = false;


            RagDollPlayed = true;
        }
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
