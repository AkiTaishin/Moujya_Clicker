using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 変数宣言

    [SerializeField] private AttackProcess attackProcess = null;

    private GameObject chase = null;
    public Vector3 PlayerDir;

    private float PlayerRot = 0.0f;

    #endregion

    private void Start()
    {
        chase = GameObject.Find("Enemy");
        PlayerDir = Vector3.back;
    }

    // Update is called once per frame
    private void Update()
    {
        // 攻撃しているときは動けません
        if (!attackProcess.bAttackFrameIsBusy)
        {
            #region プレイヤーの向き操作(WASD)

            if (Input.GetKey(KeyCode.W))
            {
                PlayerRot = 0.0f;
                PlayerDir = Vector3.back;
                this.transform.rotation = Quaternion.Euler(0.0f, PlayerRot, 0.0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerRot = 90.0f;
                PlayerDir = Vector3.left;
                this.transform.rotation = Quaternion.Euler(0.0f, PlayerRot, 0.0f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerRot = 180.0f;
                PlayerDir = Vector3.forward;
                this.transform.rotation = Quaternion.Euler(0.0f, PlayerRot, 0.0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                PlayerRot = 270.0f;
                PlayerDir = Vector3.right;
                this.transform.rotation = Quaternion.Euler(0.0f, PlayerRot, 0.0f);
            }

            #endregion プレイヤーの向き操作(WASD)
        }
    }
}