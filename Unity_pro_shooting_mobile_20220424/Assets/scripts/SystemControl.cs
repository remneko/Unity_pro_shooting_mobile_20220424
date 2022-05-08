using UnityEngine;
using UnityEngine.UI;


//命名空間:程式區塊
namespace Mui
{
    /// <summary>
    /// 控制系統:移動功能
    /// 虛擬搖桿控制角色移動
    /// </summary>
    public class SystemControl : MonoBehaviour
    {
        [SerializeField, Header("虛擬搖桿")]
        private Joystick joystick;
        [SerializeField, Header("移動速度"),Range(0,30)]
        private float speed = 3.5f;
        [SerializeField, Header("角色方向圖示")]
        private Transform traDirectionIcon;
        [SerializeField, Header("角色方向圖示範圍"), Range(0, 5f)]
        private float rangeDirectionIcon = 2.5f;
        [SerializeField, Header("角色旋轉速度"), Range(0, 5f)]
        private float speedTurn = 0.5f;


        private Rigidbody rig;

        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            GetJoystickvalue();
            UpdateDirectionIconPos();
            LookDirectionIcon();
        }
        /// <summary>
        /// 取得虛擬搖桿值
        /// </summary>
        private void GetJoystickvalue()
        {
            print("<color=yellow>水平:" + joystick.Horizontal + "</color>");
        }

        private void Move()
        {
            //剛體,加速度 = 三維向量(x,y,z)
            rig.velocity = new Vector3(joystick.Horizontal, 0, joystick.Vertical) * speed;
            
        }

        /// <summary>
        /// 更新角色方向圖示的座標
        /// </summary>
        private void UpdateDirectionIconPos()
        {
            //新座標 = 腳色座標 + 三維向量(x,y,z)(虛擬搖桿的水平與垂直) + 方向圖標的圖示
            Vector3 pos = transform.position + new Vector3(joystick.Horizontal, 1f, joystick.Vertical) * rangeDirectionIcon;
            //更新方向圖示的座標 = 新座標
            traDirectionIcon.position = pos;
        }

        private void LookDirectionIcon()
        {
            //取得面相角度 = 四位元.面相角度(方向圖示 - 角色) - 方向圖示與角色的向量
            Quaternion look = Quaternion.LookRotation(traDirectionIcon.position - transform.position);
            //角色的角度 = 四位元.插值(角色的角度,面相角度,旋轉速度*一幀的時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, look, speedTurn * Time.deltaTime);
            //角色的.歐拉角度 = 三維向量(0,原本的歐拉角度,0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}

 