using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class DamageManager : MonoBehaviourPun
{
    [SerializeField, Header("血量"),Range(0,1000)]
    private float Hp = 200;
    [SerializeField, Header("擊中特效")]
    private GameObject goVFXHit;

    private string nameBullet = "子彈";

    public Image Imghp;
    private float maxHp;

    private void Awake()
    {
        maxHp = Hp;
    }
    //進入
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(nameBullet))
        {
            //collision.contacts[0] 碰到的第一個物件
            //碰到物件的座標
            Damage(collision.contacts[0].point);
        }
    }

    //持續
    private void OnCollisionExit(Collision collision)
    {
        
    }

    //離開
    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void Damage(Vector3 posHit)
    {
        Hp -= 20;
        Imghp.fillAmount = Hp / maxHp;

        //連線.生成(特效,擊中座標,角度)
        PhotonNetwork.Instantiate(goVFXHit.name, posHit, Quaternion.identity);
    }
}
