using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    #region 変数宣言

    // 敵プレハブ
    [SerializeField] private GameObject enemyPrefab = null;

    // 雑魚敵のタイプ
    // @todo 仕様としてどれが何パー出現率があるか聞く
    // 0 = NORMAL, // 攻撃１体力１速さ１
    // 1 = SPEED, // 攻撃１体力１速さ２
    // 2 = POWER, // 攻撃３体力４速さ１
    public int type = 0;

    //敵生成時間間隔
    private float interval;

    //経過時間
    private float time = 0.0f;

    #endregion 変数宣言

    // Start is called before the first frame update
    private void Start()
    {
        // @todo可変値
        // 一番最初の生成時間を指定
        interval = 3.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        // 時間計測
        time += Time.deltaTime;

        // 経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            Generater();

            // 経過時間を初期化して再度時間計測を始める
            time = 0.0f;
        }
    }

    // 生成時間の変更
    private void ChangeInterval()
    {
        interval = Random.Range(0.5f, 5.0f);
        //Debug.Log(interval);
    }

    private void Generater()
    {
        // enemyをインスタンス化する(生成する)
        GameObject enemy = Instantiate(enemyPrefab);

        #region 雑魚のタイプ決定

        type = Random.Range(0, 3);
        var workType = enemy.GetComponent<EnemyStatusManager>().Type = type;

        // Normal
        if (workType == 0)
        {
            enemy.GetComponent<EnemyStatusManager>().SetStatus(1, 1, 1.0f);
            enemy.GetComponent<Renderer>().material.color = Color.green;
        }
        // Speed
        if (workType == 1)
        {
            enemy.GetComponent<EnemyStatusManager>().SetStatus(1, 1, 2.0f);
            enemy.GetComponent<Renderer>().material.color = Color.blue;
        }
        // Power
        if (workType == 2)
        {
            enemy.GetComponent<EnemyStatusManager>().SetStatus(4, 3, 1.0f);
            enemy.GetComponent<Renderer>().material.color = Color.red;
        }
        // ErrorCheck
        if (workType >= 3) { return; }

        #endregion 雑魚のタイプ決定

        #region 生成の条件分岐

        float random = 0.0f;
        random = Random.Range(0, 100);

        if (0 <= random && random < 25)
        {
            // 生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = new Vector3(0.0f, 7.5f, 4.5f);

            ChangeInterval();
        }

        if (25 <= random && random < 50)
        {
            // 生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = new Vector3(0.0f, 7.5f, -4.5f);

            ChangeInterval();
        }

        if (50 <= random && random < 75)
        {
            // 生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = new Vector3(4.5f, 7.5f, 0.0f);

            ChangeInterval();
        }

        if (75 <= random && random < 100)
        {
            // 生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            enemy.transform.position = new Vector3(-4.5f, 7.5f, 0.0f);

            ChangeInterval();
        }

        #endregion 生成の条件分岐

        enemy.SetActive(true);
    }
}