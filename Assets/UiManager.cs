using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.UI;
public class UiManager : Singleton<UiManager>
{
    public Text Target, KillCount;

    public override void Start()
    {
        Target.text = GM.Instance.TargetThisLevel.ToString();
        base.Start();
    }
    void Update()
    {
        
    }
}
