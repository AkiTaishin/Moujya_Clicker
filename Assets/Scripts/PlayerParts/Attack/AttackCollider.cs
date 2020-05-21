using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private WaveCountdown wave = null;


    /// <summary>
    /// 攻撃との衝突判定
    /// タグで何とぶつかったのか判定する
    /// </summary>
    /// <param name="other">衝突した物体が入る</param>
    void OnTriggerEnter(Collider other)
    {
        //ターゲットにしたオブジェクトにタグをつけとく
        if (other.gameObject.tag == "enemy")
        {
            // 速度デバフ
            other.transform.gameObject.GetComponent<EnemyStatusManager>().SetSpeed(GetComponent<CardBuffProcess>().RetSpeedDebuff());

            // 敵の体力の減少処理
            other.transform.gameObject.GetComponent<EnemyStatusManager>().SetHP(transform.parent.Find("player").GetComponent<AttackProcess>().AttackPower);
            Debug.Log(other.transform.gameObject.GetComponent<EnemyStatusManager>().GetHP());

            // 攻撃によってこの敵が破壊されたか？
            if (other.transform.gameObject.GetComponent<EnemyStatusManager>().GetHP() <= 0)
            {
                // 当たったエネミーの削除
                Destroy(other.transform.gameObject);
                // 次のWaveまでの残り敵数管理
                wave.TextCountdown();
            }
        }
    }
}
