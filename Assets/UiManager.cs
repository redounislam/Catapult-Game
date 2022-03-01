using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text Target, KillCount;
    public GameObject ScoreCard;
    public GameObject StartSceneUI, EndSceneUI, LevelCompleteUI , Tut , HandTap;
    public Image FillBar;
    public TMP_Text LevelText;
    Scene currentScene;
    public override void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        ScoreCard.gameObject.SetActive(false);
        Target.text = GM.Instance.TargetThisLevel.ToString();
        base.Start();
        StartSceneUI.gameObject.SetActive(true);
        LevelText.text = SceneLoadCounter.Instance.SceneLoadCount.ToString();
    }
    public void GetCurrentFill(float current, float max)
    {


        float fillAmmount = (float)current / (float)max;
        FillBar.fillAmount = fillAmmount;

    }

    public void Reset()
    {
        GM.Instance.cannonController.GetComponent<DrawProjection>().enabled = true;
        GM.Instance.cannonController.Played = false;
        GM.Instance.cannonController.launched = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void GoToNextScene()
    {
        GM.Instance.cannonController.GetComponent<DrawProjection>().enabled = true;
        GM.Instance.cannonController.Played = false;
        GM.Instance.cannonController.launched = false;
        DOTween.KillAll();
        string sceneName = currentScene.name;
        // PlayerPrefs.SetInt("SaveScene", SceneManager.GetActiveScene().buildIndex );

        if (sceneName == "3")
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }


    }
}
