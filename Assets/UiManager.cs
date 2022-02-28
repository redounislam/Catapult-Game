using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text Target, KillCount;
    public GameObject ScoreCard;
    public GameObject StartSceneUI, EndSceneUI, LevelCompleteUI;
    public Image FillBar;
    public override void Start()
    {
        ScoreCard.gameObject.SetActive(false);
        Target.text = GM.Instance.TargetThisLevel.ToString();
        base.Start();
        StartSceneUI.gameObject.SetActive(true);
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
    public IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(.2f);
       
    }
}
