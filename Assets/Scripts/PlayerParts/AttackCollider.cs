using System.Collections;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    // コルーチン二重稼働防止用
    // 攻撃発生まで動けない時のためにpublicにしておく@todo仕様確認
    public bool bAttackFrameIsBusy = false;

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
                StartCoroutine(C_AttackFrame(0.5f));
            }
            // 強
            if (Input.GetKey(KeyCode.V))
            {
                StartCoroutine(C_AttackFrame(2.0f));
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
        yield return new WaitForSeconds(1);
        PlayerAttackFinish();
    }

    // 攻撃処理
    private void PlayerAttack()
    {
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<MeshRenderer>().enabled = true;
    }

    // 攻撃終了処理(当たり判定の削除)
    private void PlayerAttackFinish()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;

        // フラグの初期化
        bAttackFrameIsBusy = false;
    }

    /// <summary>
    /// 攻撃との衝突判定
    /// タグで何とぶつかったのか判定する
    /// </summary>
    /// <param name="other">衝突した物体のタグが入る</param>
    void OnTriggerEnter(Collider other)
    {
        //ターゲットにしたオブジェクトにタグをつけとく
        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("hit");

            // @todoここでダメージ判定処理

            // コルーチン停止
            // 物体どっちにしろ消えるしこれいらないのでは？
            // chase.GetComponent<Chase>().SetbC_();

            // 当たったエネミーの削除
            Destroy(other.gameObject);
            Debug.Log("攻撃によって死亡");
        }
    }
}