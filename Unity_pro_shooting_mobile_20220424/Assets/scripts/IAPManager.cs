using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

namespace Mui
{ 
    public class IAPManager : MonoBehaviour
    {
        [SerializeField] private IAPButton iapBuyskinRed;
        [SerializeField] private Text textIAPTip;
        
        //擁有紅色造型
        private bool hasSkinred;

        private void Start()
        {
            iapBuyskinRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
            iapBuyskinRed.onPurchaseFailed.AddListener(PurchasefailedSkinRed);
        }

        /// <summary>
        /// 購買成功
        /// </summary>
        private void PurchaseCompleteSkinRed(Product product)
        {
            textIAPTip.text = product.ToString() + "，購買成功";
            hasSkinred = true;
            //延遲3秒後呼叫隱藏內購訊息
            //延遲呼叫(方法名稱,延遲時間)
            Invoke("hiddenIAPTip", 3);
            
        }
        /// <summary>
        /// 購買失敗
        /// </summary>
        private void PurchasefailedSkinRed(Product product, PurchaseFailureReason rip)
        {
            textIAPTip.text = product.ToString() + "，購買失敗原因:" + rip;
            Invoke("hiddenIAPTip", 3);

        }
        /// <summary>
        /// 隱藏內購提示訊息
        /// </summary>
        private void hiddenIAPTip()
        {
            textIAPTip.text = "";
        }
    }
}

