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
        if (other.gameObject.CompareTag("Explode"))
        {
            if (other.transform.root.transform.GetChild(0).GetComponent<Enemy>().m_EnemyType == EnemyType.Giant &&  m_Enemy == null)
            {
                other.transform.GetChild(0).gameObject.SetActive(true); other.transform.GetChild(0).gameObject.transform.parent = null;
                other.transform.gameObject.SetActive(false);
            }

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
