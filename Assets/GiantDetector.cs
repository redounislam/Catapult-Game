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
            if(other.gameObject.transform.root.GetChild(0).GetComponent<Enemy>().m_EnemyType == EnemyType.Giant)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
