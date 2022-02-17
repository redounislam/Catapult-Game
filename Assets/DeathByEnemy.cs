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
          
            m_Enemy.RagDollOn();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Other"))
        {
            
            m_Enemy.RagDollOn();
        }
    }
    void Update()
    {
        
    }
}
