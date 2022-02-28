using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoDown : MonoBehaviour
{
    private void OnEnable()
    {
    GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-18f, 18f), Random.Range(2, 12f), Random.Range(-18f, 18f)).normalized * 100);
    }
}
