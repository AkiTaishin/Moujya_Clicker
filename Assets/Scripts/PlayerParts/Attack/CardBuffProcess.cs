using UnityEngine;

public class CardBuffProcess : MonoBehaviour
{
    #region 変数宣言

    // 先頭の箱(カード)
    [SerializeField] public GameObject GetBuff = null;

    // Caseに使用。バフはなにか確認。
    public int AttackBuffType;

    public int PowerBuff;
    public float SpeedDebuff;

    #endregion 変数宣言


    private void Start()
    {
        // 次に適用されるバフタイプの取得
        AttackBuffType = GetBuff.GetComponent<CardBuff>().CardBuffType;
        PowerBuff = 1;
        SpeedDebuff = 1.0f;
    }

    private void Update()
    {
        // 重くなりそうで怖い@todo
        AttackBuffType = GetBuff.GetComponent<CardBuff>().CardBuffType;
    }

    // ここにバフごとの処理
    public void BuffProcess()
    {
        switch (AttackBuffType)
        {
            case 0:
                // 基礎攻撃力を2倍に
                PowerBuff = 2;
                SpeedDebuff = 1.0f;
                break;

            case 1:
                // 敵の移動速度を半分に
                PowerBuff = 1;
                SpeedDebuff = 0.5f;
                break;

            case 2:
                // 次の攻撃を360°の判定に
                PowerBuff = 1;
                SpeedDebuff = 1.0f;
                break;

            // ErrorCheak
            default:
                PowerBuff = 1;
                SpeedDebuff = 1.0f;
                return;
        }
    }

    public float RetSpeedDebuff()
    {
        return SpeedDebuff;
    }


    // ここで先頭の箱のバフを切り替えることにより、再帰的にすべての箱の中身が変わる
    public void NextBuff()
    {
        GetBuff.GetComponent<CardBuff>().ChangeType();
    }
}