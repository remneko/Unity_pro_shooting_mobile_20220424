using UnityEngine;
using UnityEngine.UI;

namespace mui
{ 
    public class SystemAttack : MonoBehaviour
    {
        /// <summary>
        /// 攻擊系統
        /// </summary>
        [HideInInspector]
        public Button btnFire;
        [SerializeField, Header("子彈")]
        private GameObject goBullet;
        [SerializeField, Header("子彈最大數量")]
        private int bulletCountMax = 3;
        [SerializeField, Header("子彈生成位置")]
        private Transform trafire;
        [SerializeField, Header("子彈發射速度"), Range(0, 3000)]
        private int speedfire = 500;

        private int bulletCountCurrent;

        private void Awake()
        {
            //發射按鈕.點擊.施加監聽器(開槍方法)，按下發射按鈕執行開槍方法
            btnFire.onClick.AddListener(Fire);
            
        }

        /// <summary>
        /// 開槍
        /// </summary>
        private void Fire()
        {
            //生成(物件,座標,角度)
            Instantiate(goBullet, trafire.position, Quaternion.identity);
        }

    }

}



