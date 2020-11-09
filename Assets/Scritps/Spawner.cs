using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 创建/销毁水果与炸弹
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("水果的预设")]
    public GameObject[] Fruits;

    [Header("炸弹的预设")]
    public GameObject Bomb;

    /// <summary>
    /// 播放声音的组件
    /// </summary>
    public AudioSource AudioSource;

    float time = 3f;//水果刷新时间控制
    /// <summary>
    /// 是否在玩游戏
    /// </summary>
    bool isPlaying = true;

    void Update()
    {
        if (!isPlaying)
            return;

        time -= Time.deltaTime;
        if(time<0)
        {
            //到时间产生水果
            int fruitCount = Random.Range(1, 5);
            for (int i = 0; i < fruitCount; i++)
                onSpawn(true);
            time = 3f;
            //随机产生炸弹
            int bombNum = Random.Range(0, 100);
            if(bombNum>70)
            {
                onSpawn(false);
            }
        }
    }
    /// <summary>
    /// 产生水果与炸弹
    /// </summary>
    private void onSpawn(bool isFruit)
    {
        //播放音乐
        AudioSource.Play();


        //得知范围
        float x = Random.Range(-8.4f, 8.4f);
        float y = transform.position.y;
        float z = Random.Range(-14, -1);

        //实例化水果或者炸弹
        int fruitIndex = Random.Range(0, Fruits.Length);
        GameObject go;
        if (isFruit)
            go = Instantiate<GameObject>(Fruits[fruitIndex], new Vector3(x, y, z), Random.rotation);
        else
            go = Instantiate<GameObject>(Bomb, new Vector3(x, y, z), Random.rotation);


        //定义水果速度与高度
        Vector3 velocity = new Vector3(-x * Random.Range(0.2f, 0.8f),-Physics.gravity.y * Random.Range(1.2f, 1.5f), 0);
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.velocity = velocity;  
    }
    /// <summary>
    /// 有物体碰撞调用
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
}