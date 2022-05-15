using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;                                   //引用廣告API

namespace mui
{
    /// <summary>
    /// 按下看廣告按鈕後觀看廣告
    /// 看完廣告後增加金幣
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    
    {
        [SerializeField, Header("金幣"), Range(0, 1000)]
        private int addcoinValue = 100;
        private int coinPlayer;
        private string gameIdAndroid= "4754891";                        //後台Andriod ID
        private string gameIdIos= "4754890";                            //後台ios ID
        private string gameId;

        private string AdsIdAndroid = "AddCoin";                        
        private string AdsIdIos = "AddCoin";
        private string AdsId;
        /// <summary>
        /// 玩家金幣數量
        /// </summary>
        private Text textCoin;

        /// <summary>
        /// 看完廣告後增加金幣
        /// </summary>
        private Button btnAdsAddcion;
        //初始化成功會執行的方法
        public void OnInitializationComplete()
        {
            print("<color=green>廣告初始化成功</color>");
        }
        //初始化失敗會執行的方法
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>廣告初始化失敗:" + message + "</color>");
        }
        //廣告載入成功會執行的方法
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>廣告載入成功" + placementId +"</color>");
        }
        //廣告載入失敗會執行的方法
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>廣告載入失敗:" + message + "</color>");
        }
        /// <summary>
        /// 載入廣告
        /// </summary>
        private void LoadAds()
        {
            print("載入廣告，ID:" + AdsId);
            Advertisement.Load(AdsId, this);
            showAds();
       
        }
        //廣告顯示開始的方法
        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>廣告顯示開始" + placementId + "</color>");
        }
        //廣告顯示點擊的方法
        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>廣告顯示點擊" + placementId + "</color>");
        }
        //廣告載入完成的方法
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>廣告載入完成" + placementId + "</color>");
            coinPlayer += addcoinValue;
            textCoin.text = coinPlayer.ToString();
        }

        //廣告顯示失敗的方法
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>廣告顯示失敗:" + message + "</color>");
        }
        private void Awake()
        {
            textCoin = GameObject.Find("金幣數量").GetComponent<Text>();
            btnAdsAddcion = GameObject.Find("廣告按鈕").GetComponent<Button>();
            btnAdsAddcion.onClick.AddListener(LoadAds);

            InitializeAds();
            

            //#if 程式區塊判斷式,條件達成才會執行該區塊
            // 如果玩家 作業系統 是 ios就指定為ios 廣告
            //否則如果玩家 作業系統 是 android就指定為 android 廣告
#if UNITY_IOS
            AdsId = AdsIdIos;
# elif UNITY_ANDROID
            AdsId = AdsIdAndroid;
#endif
            //pc端測試
            AdsId = AdsIdAndroid;
        }

        private void showAds() 
        {
            Advertisement.Show(AdsId, this);
        }

        private void InitializeAds()
        {
            gameId = gameIdAndroid;

            Advertisement.Initialize(gameId, true, this);
        }


        
    }

}
