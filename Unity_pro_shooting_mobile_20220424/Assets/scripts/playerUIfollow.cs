using UnityEngine;

namespace Mui
{
    public class PlayerUIfollow : MonoBehaviour
    {
        [SerializeField, Header("�첾")]
        private Vector3 V3Offset;
        private string nameplayer = "�Ԥh";
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
        /// ���H���a
        /// </summary>
        private void Follow()
        {

            transform.position = traPlayer.position + V3Offset;
        }
    }
}
