using Photon.Pun;
using UnityEngine;

namespace Mui
{
    public class Destroyself : MonoBehaviourPun
    {
        [SerializeField, Header("刪除時間"), Range(0, 10)]
        private float timeDestroy = 5;
        [SerializeField, Header("是否需要碰撞後消除")]
        private bool collisionDestroy;

        /// <summary>
        /// 延遲刪除方法
        /// </summary>
        private void Awake()
        {
            Invoke("DestroyDelay", timeDestroy);
        }

        private void DestroyDelay()
        {
            //連線刪除(遊戲物件) - 刪除伺服器內的物件
            PhotonNetwork.Destroy(gameObject);
        }
    }
}

