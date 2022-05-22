using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

namespace Mui
{ 
    public class IAPManager : MonoBehaviour
    {
        [SerializeField] private IAPButton iapBuyskinRed;
        [SerializeField] private Text textIAPTip;
        
        //�֦�����y��
        private bool hasSkinred;

        private void Start()
        {
            iapBuyskinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            iapBuyskinRed.onPurchaseFailed.AddListener(PurchasefailedSkinRed);
        }

        /// <summary>
        /// �ʶR���\
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = product.ToString() + "�A�ʶR���\";
            hasSkinred = true;
            //����3���I�s���ä��ʰT��
            //����I�s(��k�W��,����ɶ�)
            Invoke("hiddenIAPTip", 3);
            
        }
        /// <summary>
        /// �ʶR����
        /// </summary>
        private void PurchasefailedSkinRed(Product product, PurchaseFailureReason rip)
        {
            textIAPTip.text = product.ToString() + "�A�ʶR���ѭ�]:" + rip;
            Invoke("hiddenIAPTip", 3);

        }
        /// <summary>
        /// ���ä��ʴ��ܰT��
        /// </summary>
        private void hiddenIAPTip()
        {
            textIAPTip.text = "";
        }
    }
}

