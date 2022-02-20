using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    Enemy m_Enemy;
    void Start()
    {
        m_Enemy = GetComponentInChildren<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.Instance.StartGame && GM.Instance.EnemyMove && !m_Enemy.Death)
        {
           transform.position += Vector3.right * Speed;
        }
    }
}
