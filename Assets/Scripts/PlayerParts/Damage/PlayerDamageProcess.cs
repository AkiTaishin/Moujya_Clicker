using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageProcess : MonoBehaviour
{
    // 調節用
    readonly float PlayerMaxHP = 7.0f;

    // 体力
    public float MaxHitPoint;
    public float HitPoint;


    private void Start()
    {
        // 現状７
        MaxHitPoint = HitPoint = PlayerMaxHP;
    }

    /// <summary>
    /// プレイヤーとの衝突判定
    /// タグで何とぶつかったのか判定する
    /// </summary>
    /// <param name="other">衝突した物体が入る</param>
    void OnTriggerEnter(Collider other)
    {
        //ターゲットにしたオブジェクトにタグをつけとく
        if (other.gameObject.tag == "enemy")
        {
            // 当たったエネミーの削除
            Destroy(other.gameObject);

            // @todoここでダメージ判定処理
            // ぶつかった敵の種類に応じた体力減算処理
            HitPoint -= other.gameObject.GetComponent<EnemyStatusManager>().GetPower();
            Debug.Log("いたい");

            // Debug用
            if(HitPoint <= 0)
            {
                HitPoint = PlayerMaxHP;
            }
        }
    }
}
