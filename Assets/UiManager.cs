using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.UI;
using TMPro;
public class UiManager : Singleton<UiManager>
{
    public TMP_Text Target, KillCount;
    public GameObject ScoreCard;
    public GameObject StartSceneUI, EndSceneUI, LevelCompleteUI;
    public override void Start()
    {
        ScoreCard.gameObject.SetActive(false);
        Target.text = GM.Instance.TargetThisLevel.ToString();
        base.Start();
    }
    void Update()
    {
        
    }
}
