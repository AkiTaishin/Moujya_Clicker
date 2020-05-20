using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // プレイヤーの体力バー
    [SerializeField] public Image FillAmount = null;
    // プレイヤーの体力を取得する
    [SerializeField] public PlayerDamageProcess playerDamageProcess = null;

    private float workHP;


    private void Start()
    {
        // 最初は満タン
        FillAmount.fillAmount = 1.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // 必ずこっちを処理してからにしたい
        workHP = playerDamageProcess.GetComponent<PlayerDamageProcess>().HitPoint;
    }

    private void LateUpdate()
    {
        // 現状の体力のパーセントを算出
        FillAmount.fillAmount = workHP / playerDamageProcess.GetComponent<PlayerDamageProcess>().MaxHitPoint;
    }
}
