using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByEnemy : MonoBehaviour
{
    Enemy m_Enemy;
    void Start()
    {
        m_Enemy = transform.GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Other"))
        {
            if(m_Enemy == null)
          other.transform.root.transform.GetChild(0).GetComponent<Enemy>().RagDollOn();
            else
            m_Enemy.RagDollOn();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Other"))
        {
            if (m_Enemy == null)
                collision.gameObject.transform.root.transform.GetChild(0).GetComponent<Enemy>().RagDollOn();
            else
                m_Enemy.RagDollOn();
        }
 

    }

}
