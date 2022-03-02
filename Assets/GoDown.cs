using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GoDown : MonoBehaviour
{
    private void OnEnable()
    {
    GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-18f, 30f), Random.Range(5,20f), Random.Range(-30f, 18f)).normalized * 150);
    }
}
