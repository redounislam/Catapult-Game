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
            if ( m_Enemy == null)
            {
                if (other.transform.root.transform.GetChild(0).GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
                {
                    other.transform.GetChild(0).gameObject.SetActive(true); other.transform.GetChild(0).gameObject.transform.parent = null;
                    other.transform.gameObject.SetActive(false);
                }
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
        if (collision.gameObject.CompareTag("Explode"))
        {
            if (m_Enemy == null)
            {
                if (collision.transform.root.transform.GetChild(0).GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
                {
                    collision.transform.GetChild(0).gameObject.SetActive(true); collision.transform.GetChild(0).gameObject.transform.parent = null;
                    collision.transform.gameObject.SetActive(false);
                }
            }

        }

    }

}
