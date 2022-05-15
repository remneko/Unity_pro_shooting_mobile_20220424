using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;                                   //�ޥμs�iAPI

namespace mui
{
    /// <summary>
    /// ���U�ݼs�i���s���[�ݼs�i
    /// �ݧ��s�i��W�[����
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    
    {
        [SerializeField, Header("����"), Range(0, 1000)]
        private int addcoinValue = 100;
        private int coinPlayer;
        private string gameIdAndroid= "4754891";                        //��xAndriod ID
        private string gameIdIos= "4754890";                            //��xios ID
        private string gameId;

        private string AdsIdAndroid = "AddCoin";                        
        private string AdsIdIos = "AddCoin";
        private string AdsId;
        /// <summary>
        /// ���a�����ƶq
        /// </summary>
        private Text textCoin;

        /// <summary>
        /// �ݧ��s�i��W�[����
        /// </summary>
        private Button btnAdsAddcion;
        //��l�Ʀ��\�|���檺��k
        public void OnInitializationComplete()
        {
            print("<color=green>�s�i��l�Ʀ��\</color>");
        }
        //��l�ƥ��ѷ|���檺��k
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>�s�i��l�ƥ���:" + message + "</color>");
        }
        //�s�i���J���\�|���檺��k
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>�s�i���J���\" + placementId +"</color>");
        }
        //�s�i���J���ѷ|���檺��k
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J����:" + message + "</color>");
        }
        /// <summary>
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("���J�s�i�AID:" + AdsId);
            Advertisement.Load(AdsId, this);
            showAds();
       
        }
        //�s�i��ܶ}�l����k
        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>�s�i��ܶ}�l" + placementId + "</color>");
        }
        //�s�i����I������k
        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>�s�i����I��" + placementId + "</color>");
        }
        //�s�i���J��������k
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>�s�i���J����" + placementId + "</color>");
            coinPlayer += addcoinValue;
            textCoin.text = coinPlayer.ToString();
        }

        //�s�i��ܥ��Ѫ���k
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>�s�i��ܥ���:" + message + "</color>");
        }
        private void Awake()
        {
            textCoin = GameObject.Find("�����ƶq").GetComponent<Text>();
            btnAdsAddcion = GameObject.Find("�s�i���s").GetComponent<Button>();
            btnAdsAddcion.onClick.AddListener(LoadAds);

            InitializeAds();
            

            //#if �{���϶��P�_��,����F���~�|����Ӱ϶�
            // �p�G���a �@�~�t�� �O ios�N���w��ios �s�i
            //�_�h�p�G���a �@�~�t�� �O android�N���w�� android �s�i
#if UNITY_IOS
            AdsId = AdsIdIos;
# elif UNITY_ANDROID
            AdsId = AdsIdAndroid;
#endif
            //pc�ݴ���
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
