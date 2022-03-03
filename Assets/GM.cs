using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GM :Singleton<GM>
{
    public GameObject CannonPoint , CannonPrefab;
    public CannonController cannonController;
    public int TargetThisLevel;
    public int KillCount;
    public GameObject GroundPlane;

    public bool StartGame, End;

    public GameObject CamStartPos, CamEndPos, Cam50HoopsPos;
    public float CamMoveDuration = 1f;
    Camera cam;
    public bool EnemyMove;
    public GameObject ParentCam;
    public Vector3 offset;

    public bool TapTut;


    public GameObject player;


    public IEnumerator CameraRoutine()
    {

        yield return new WaitForSeconds(1);
        GroundPlane.GetComponent<Animator>().enabled = false;
        cam = Camera.main;
        cam.transform.position = CamStartPos.transform.position;
        cam.transform.eulerAngles = new Vector3(CamStartPos.transform.eulerAngles.x, CamStartPos.transform.eulerAngles.y,
        CamStartPos.transform.eulerAngles.z);
        cam.transform.DORotate(new Vector3(CamEndPos.transform.eulerAngles.x, CamEndPos.transform.eulerAngles.y,
          CamEndPos.transform.eulerAngles.z), CamMoveDuration);
        cam.transform.DOMove(CamEndPos.transform.position, CamMoveDuration).OnComplete(() =>
        {
            UiManager.Instance.LevelText.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            UiManager.Instance.LevelText.gameObject.SetActive(false);
            UiManager.Instance. ScoreCard.gameObject.SetActive(true);
            StartGame = true;
            UiManager.Instance.Tut.SetActive(true);
            cam.transform.GetComponent<SharkAttack.CameraController>().enabled = true;
        });

    }

    public override void Start()
    {

        PlayerPrefs.SetInt("current_scene", SceneManager.GetActiveScene().buildIndex);
        if (CannonPrefab != null)
        {
            GameObject g = Instantiate(CannonPrefab, CannonPoint.transform.position, Quaternion.Euler(new Vector3(CannonPoint.transform.eulerAngles.x, CannonPoint.transform.eulerAngles.y, CannonPoint.transform.eulerAngles.z)));
            cannonController = g.transform.GetComponentInChildren<CannonController>();
        }
        base.Start();
    }
    public void StartIt()
    {
      
        StartCoroutine(CameraRoutine());
        UiManager.Instance.StartSceneUI.SetActive(false);
    }
    public void LevelFail()
    {
        SharkAttack.CameraController.Instance.target = null;
        UiManager.Instance.EndSceneUI.SetActive(true);
        UiManager.Instance.ScoreCard.SetActive(false);
             
        GM.Instance.End = true;
    }
    void Update()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }
}
