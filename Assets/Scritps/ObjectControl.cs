using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    /// <summary>
    /// 一半的水果
    /// </summary>
    public GameObject halfFruit;

    public GameObject Splash;

    public GameObject SplashFlat;

    public GameObject Firework;

    private bool dead = false;

    public AudioClip ac;

    /// <summary>
    /// 被切割的脚本
    /// </summary>
    public void OnCut()
    {
        //防止重复调用
        if (dead)
            return;

        if(gameObject.name.Contains("Bomb"))//炸弹情况
        {
            Instantiate(Firework, transform.position, Quaternion.identity);

            //炸弹扣分
            UIScore.Instance.Remove(20);

        }
        else//水果的情况
        {
            //先生成被切割的水果
            for (int i = 0; i < 2; i++)
            {
                GameObject go = Instantiate<GameObject>(halfFruit, transform.position, Random.rotation);
                go.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * 5f, ForceMode.Impulse);
            }
            //生成特效
            Instantiate(Splash, transform.position, Quaternion.identity);
            Instantiate(SplashFlat, transform.position, Quaternion.identity);

            //水果加分
            UIScore.Instance.Add(10);

        }
        AudioSource.PlayClipAtPoint(ac, transform.position);

        //销毁自身
        Destroy(gameObject);

        dead = true;
    }
}
