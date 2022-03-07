using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantDetector : MonoBehaviour
{
    // Start is called before the first frame update 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Other"))
        {
            if(other.gameObject.GetComponentInParent<Enemy>().m_EnemyType == EnemyType.Giant)
            {
                transform.GetComponent<MeshRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
