using UnityEngine;

public class AttackRay : MonoBehaviour
{
    [SerializeField] private WaveCountdown wave = null;

    // 攻撃方向
    private Vector3 GetPlayerDir = Vector3.zero;

    // 攻撃力基礎値
    //private int AttackPower = 1;

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
                // 速度デバフ
                hit.transform.gameObject.GetComponent<EnemyStatusManager>().SetSpeed(GameObject.Find("attackCollider").GetComponent<CardBuffProcess>().RetSpeedDebuff());

                // 敵の体力の減少処理
                hit.transform.gameObject.GetComponent<EnemyStatusManager>().SetHP(GetComponent<AttackProcess>().AttackPower);
                //hit.transform.gameObject.GetComponent<EnemyStatusManager>().SetHP(transform.parent.Find("player").GetComponent<AttackProcess>().AttackPower);
                Debug.Log(hit.transform.gameObject.GetComponent<EnemyStatusManager>().GetHP());

                // 攻撃によってこの敵が破壊されたか？
                if (hit.transform.gameObject.GetComponent<EnemyStatusManager>().GetHP() <= 0)
                {
                    // 当たったエネミーの削除
                    Destroy(hit.transform.gameObject);
                    // 次のWaveまでの残り敵数管理
                    wave.TextCountdown();
                }
            }
        }
    }
}