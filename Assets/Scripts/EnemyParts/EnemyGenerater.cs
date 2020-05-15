using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerater : MonoBehaviour
{
    // 敵プレハブ
    [SerializeField] private GameObject enemyPrefab = null;
    
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // @todo可変値
        interval = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 時間計測
        time += Time.deltaTime;

        // 経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            // enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(true);

            float random = 0.0f;
            random = Random.Range(0, 99);

            #region 生成の条件分岐

            if(0 <= random && random < 25)
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

            #endregion

            // 経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
    }

    // 生成時間の変更
    void ChangeInterval()
    {
        interval = Random.Range(0.5f, 5.0f);
        Debug.Log(interval);
    }
}
