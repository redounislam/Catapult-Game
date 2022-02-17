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
            print("asd");
            m_Enemy.RagDollOn();
        }
    }
    void Update()
    {
        
    }
}
