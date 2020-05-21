using System.Collections;
using UnityEngine;

// @todo 攻撃による処理のみにしたい 済
// 現状、強攻撃処理と攻撃による処理の混在
public class AttackProcess : MonoBehaviour
{
    #region 変数宣言

    public AttackRay attackRay = null;
    public AttackCollider attackCollider = null;
    public GameObject circleCollider = null;

    // ぶっ飛ぶから子供にしてないよ
    public GameObject colliderObject = null;

    // false = 弱
    private bool AttackPattern = false;

    // false = 通常, true = カードバフ有
    private bool AttackCollision = false;

    // 攻撃基礎値
    public int AttackPower;

    // コルーチン二重稼働防止用
    // 攻撃発生まで動けない時のためにpublicにしておく@todo仕様確認
    public bool bAttackFrameIsBusy = false;

    #endregion 変数宣言

    private void Start()
    {
        // 初期設定
        AttackPattern = false;
        AttackCollision = false;
        bAttackFrameIsBusy = false;
        AttackPower = 1;
    }

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
        colliderObject = GameObject.Find("attackCollider");

        // 円形範囲攻撃
        if (colliderObject.GetComponent<CardBuffProcess>().AttackBuffType == 2)
        {
            if (AttackPattern)
            {
                AttackPower = 2;
            }
            else
            {
                AttackPower = 1;
            }
            circleCollider.GetComponent<SphereCollider>().enabled = true;
            circleCollider.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            if (AttackPattern)
            {
                AttackPower = 2;

                // カードの効果発動
                ApplyToCardBuff(colliderObject);

                colliderObject.GetComponent<BoxCollider>().enabled = true;
                colliderObject.GetComponent<MeshRenderer>().enabled = true;
                colliderObject.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                AttackPower = 1;

                // カードの効果発動
                ApplyToCardBuff(colliderObject);

                attackRay.AttackHitProcess();
                colliderObject.GetComponent<MeshRenderer>().enabled = true;
                colliderObject.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
    }

    // 攻撃終了処理(当たり判定の削除)
    private void PlayerAttackFinish()
    {
        colliderObject = GameObject.Find("attackCollider");

        // 円形範囲攻撃
        if (colliderObject.GetComponent<CardBuffProcess>().AttackBuffType == 2)
        {
            circleCollider.GetComponent<SphereCollider>().enabled = false;
            circleCollider.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            if (AttackPattern)
            {
                colliderObject.GetComponent<BoxCollider>().enabled = false;
                colliderObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                colliderObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        // フラグの初期化
        bAttackFrameIsBusy = false;

        // バフの切り替え
        colliderObject.GetComponent<CardBuffProcess>().NextBuff();
    }

    /// <summary>
    /// カードの適用
    /// </summary>
    /// <param name="colliderObject"></param>
    private void ApplyToCardBuff(GameObject colliderObject)
    {
        colliderObject.GetComponent<CardBuffProcess>().BuffProcess();
        AttackPower *= colliderObject.GetComponent<CardBuffProcess>().PowerBuff;
    }
}