using System.Collections;
using UnityEngine;

public class Chase : MonoBehaviour
{
    #region 変数宣言

    [SerializeField] public EnemyStatusManager status = null;

    // UnityでプレイヤーにPlayerタグを設定する
    // プレイヤーを突っ込む
    private GameObject playerController = null;

    // プレイヤーの座標を管理
    private Vector3 LocatePlayer;

    // コルーチン二重防止用
    public bool bC_ChasePlayerIsBusy = false;

    #endregion 変数宣言

    // Start is called before the first frame update
    private void Start()
    {
        playerController = GameObject.Find("Player");
        LocatePlayer = playerController.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // コルーチンが動いていない時のみ
        if (!bC_ChasePlayerIsBusy)
        {
            StartCoroutine(C_ChasePlayer());
            // Debug.Log(LocatePlayer);
        }
    }

    /// <summary>
    /// プレイヤーを追尾する為のコルーチン
    /// </summary>
    /// <returns>Frame</returns>
    private IEnumerator C_ChasePlayer()
    {
        // 防止
        bC_ChasePlayerIsBusy = true;

        // 重なっていない時はどんどん追尾
        while (this.gameObject.transform.position != LocatePlayer)
        {
            // 加速度調整
            float speed = status.GetSpeed() * Time.deltaTime;
            yield return new WaitForEndOfFrame();

            // ここで追尾処理
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, LocatePlayer, speed);
        }
    }

    // 外部からのフラグ戻し
    public void SetbC_()
    {
        bC_ChasePlayerIsBusy = false;
    }
}