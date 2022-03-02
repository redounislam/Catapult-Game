using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantDetector : MonoBehaviour
{
    // Start is called before the first frame update 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
