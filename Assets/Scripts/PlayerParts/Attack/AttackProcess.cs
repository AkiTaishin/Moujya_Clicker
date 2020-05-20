using System.Collections;
using UnityEngine;

// @todo 攻撃による処理のみにしたい 済
// 現状、強攻撃処理と攻撃による処理の混在
public class AttackProcess : MonoBehaviour
{
    #region 変数宣言

    public AttackRay attackRay = null;
    public AttackCollider attackCollider = null;

    // ぶっ飛ぶから子供にしてないよ
    public GameObject coliider = null;

    // false = 弱
    private bool AttackPattern = false;

    // コルーチン二重稼働防止用
    // 攻撃発生まで動けない時のためにpublicにしておく@todo仕様確認
    public bool bAttackFrameIsBusy = false;

    #endregion 変数宣言

    // Update is called once per frame
    private void Update()
    {
        #region 攻撃処理

        // 攻撃用コルーチンが他に動いていなければ処理開始
        if (!bAttackFrameIsBusy)
        {
            // @todo 判定の長さと発生フレーム数調整
            // 弱
            if (Input.GetKey(KeyCode.C))
            {
                AttackPattern = false;
                StartCoroutine(C_AttackFrame(0.3f));
            }
            // 強
            if (Input.GetKey(KeyCode.V))
            {
                AttackPattern = true;
                StartCoroutine(C_AttackFrame(0.9f));
            }
        }

        #endregion 攻撃処理
    }

    /// <summary>
    /// 入力されてから当たり判定が発生するまで待機する処理
    /// </summary>
    /// <param name="waitFrame">強弱による発生フレーム</param>
    /// <returns></returns>
    private IEnumerator C_AttackFrame(float waitFrame)
    {
        // 二重防止
        bAttackFrameIsBusy = true;

        // waitFrame待つ
        yield return new WaitForSeconds(waitFrame);
        PlayerAttack();

        StartCoroutine(C_FinishAttackFrame());
    }

    // 攻撃判定を消す処理
    private IEnumerator C_FinishAttackFrame()
    {
        // 判定の可視化をなくす
        yield return new WaitForSeconds(0.1f);
        PlayerAttackFinish();
    }

    // 攻撃処理
    private void PlayerAttack()
    {
        coliider = GameObject.Find("attackCollider");
        if (AttackPattern)
        {
            coliider.GetComponent<BoxCollider>().enabled = true;
            coliider.GetComponent<MeshRenderer>().enabled = true;
            coliider.GetComponent<Renderer>().material.color = Color.white;
        }
        else
        {
            attackRay.AttackHitProcess();
            coliider.GetComponent<MeshRenderer>().enabled = true;
            coliider.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    // 攻撃終了処理(当たり判定の削除)
    private void PlayerAttackFinish()
    {
        coliider = GameObject.Find("attackCollider");
        if (AttackPattern)
        {
            coliider.GetComponent<BoxCollider>().enabled = false;
            coliider.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            coliider.GetComponent<MeshRenderer>().enabled = false;
        }

        // フラグの初期化
        bAttackFrameIsBusy = false;
    }
}