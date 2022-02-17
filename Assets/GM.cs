using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using DG.Tweening;
public class GM :Singleton<GM>
{
    public int TargetThisLevel;
    public int KillCount;


    public bool StartGame, End;

    public GameObject CamStartPos, CamEndPos;
    public float CamMoveDuration = 1f;
    Camera cam;


    public override void Start()
    {
        cam = Camera.main;
        cam.transform.position = CamStartPos.transform.position;
        cam.transform.eulerAngles = new Vector3(CamStartPos.transform.eulerAngles.x, CamStartPos.transform.eulerAngles.y,
        CamStartPos.transform.eulerAngles.z);
        cam.transform.DORotate(new Vector3(CamEndPos.transform.eulerAngles.x, CamEndPos.transform.eulerAngles.y,
          CamEndPos.transform.eulerAngles.z), CamMoveDuration);
        cam.transform.DOMove(CamEndPos.transform.position, CamMoveDuration).OnComplete(() =>
        {
          
            cam.transform.GetComponent<SharkAttack.CameraController>().enabled = true;
        });

        base.Start();
    }
    void Update()
    {
        
    }
}
