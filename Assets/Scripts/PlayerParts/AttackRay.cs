using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRay : MonoBehaviour
{
    [SerializeField] private WaveCountdown wave = null;

    private Vector3 GetPlayerDir = Vector3.zero;

    // 攻撃による当たり判定
    // Collisionを使わずに貫通判定を無くすようにする
    private Ray ray;
    public float RayLengs = 2.5f;


    /// <summary>
    /// Raycastを使った当たり判定
    /// </summary>
    public void AttackHitProcess()
    {
        // レイの向き取得
        GetPlayerDir = GameObject.Find("Player").GetComponent<PlayerController>().PlayerDir;

        // レイの作成
        ray = new Ray(transform.position, GetPlayerDir);

        // 衝突した物体の情報を取得
        RaycastHit hit;

        // Debug用に可視化
        Debug.DrawRay(ray.origin, ray.direction * RayLengs, Color.yellow, 2.0f, false);

        // ここで衝突判定
        if (Physics.Raycast(ray, out hit, RayLengs))
        {
            // 敵だった時
            if (hit.transform.tag == "enemy")
            {
                Debug.Log("RayHit");
                Debug.Log(hit.collider.gameObject.transform.position);

                // 次のWaveまでの残り敵数管理
                wave.TextCountdown();

                // 当たったエネミーの削除
                Destroy(hit.transform.gameObject);
                //Debug.Log("Ray_攻撃によって死亡");
            }
        }
    }
}
