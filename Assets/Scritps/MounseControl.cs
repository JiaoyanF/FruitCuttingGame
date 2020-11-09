using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实现刀痕效果
/// </summary>
public class MounseControl : MonoBehaviour
{
    /// <summary>
    /// 直线渲染器
    /// </summary>
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private AudioSource audioSource;


    /// <summary>
    /// 是否鼠标地第一次按下
    /// </summary>
    private bool firstMouseDown = false;
    /// <summary>
    /// 是否鼠标一直按下
    /// </summary>
    private bool mouseDown = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            firstMouseDown = true;
            mouseDown = true;

            audioSource.Play();//播放刀痕声音
        }
        if(Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        onDrawLine();
        firstMouseDown = false;
    }
    /// <summary>
    /// 保存所有的坐标
    /// </summary>
    private Vector3[] positions = new Vector3[10];

    /// <summary>
    /// 保存当前坐标数量
    /// </summary>
    private int posCount = 0;

    /// <summary>
    /// 代表每一帧鼠标的位置（头的坐标）
    /// </summary>
    private Vector3 head;

    /// <summary>
    /// 代表鼠标上一帧的位置
    /// </summary>
    private Vector3 Last;

    /// <summary>
    /// 控制画线
    /// </summary>
    private void onDrawLine()
    {
        if(firstMouseDown)
        {
            //先把计数器置为0
            posCount = 0;
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Last = head;
        }
        if(mouseDown)
        {
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Vector3.Distance(head,Last)>0.01f)
            {
                //如果两点距离比较远，就保存带在数组里面
                savePosition(head);
                posCount++;

                //发射一条射线
                onRayCast(head);

            }
            Last = head;
        }
        else
        {
            positions = new Vector3[10];
        }
        changePositions(positions);
    }

    /// <summary>
    /// 发射射线
    /// </summary>
    private void onRayCast(Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //检测到所有的物体
        RaycastHit[] hits = Physics.RaycastAll(ray);

        for(int i=0;i<hits.Length;i++)
        {
            //Debug.Log(hits[i].collider.gameObject.name);
            hits[i].collider.gameObject.SendMessage("OnCut", options: SendMessageOptions.DontRequireReceiver);

        }
    }
    /// <summary>
    /// 保存坐标点
    /// </summary>
    /// <param name="pos"></param>
    private void savePosition(Vector3 pos)
    {
        pos.z = 0;
        if(posCount<=9)
        {
            for (int i = posCount; i < 10; i++)
                positions[i] = pos;
        }
        else
        {
            for (int i = 0; i < 9; i++)
                positions[i] = positions[i + 1];
            positions[9] = pos;
        }
    }
    /// <summary>
    /// 修改直线渲染器的坐标
    /// </summary>
    /// <param name="positions"></param>
    private void changePositions(Vector3[] positions)
    {
        lineRenderer.SetPositions(positions);
    }
}