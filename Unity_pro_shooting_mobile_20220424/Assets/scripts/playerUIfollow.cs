using UnityEngine;

namespace Mui
{
    public class PlayerUIfollow : MonoBehaviour
    {
        [SerializeField, Header("位移")]
        private Vector3 V3Offset;
        private string nameplayer = "戰士";
        public Transform traPlayer;

        public void Awake()
        {

            //  traPlayer = GameObject.Find(nameplayer).transform;
        }

        private void Update()
        {
            Follow();
        }
        /// <summary>
        /// 跟隨玩家
        /// </summary>
        private void Follow()
        {

            transform.position = traPlayer.position + V3Offset;
        }
    }
}
