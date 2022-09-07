using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通过触发器代表的敌人监视玩家的代码
public class Observer : MonoBehaviour
{


    //声明玩家变化组件对象，用来挂接玩家
    public Transform player;

    //声明一个bool变量，表示玩家是否被视线扫到
    //即是否进入代表敌人视线的触发器区域内
    bool m_IsPlayerInRange;
    //声明游戏结束脚本组件类对象，为了掉用游戏结束脚本中的公有方法
    public GameEnding gameEnding;

    //触发器时间
    //玩家进入视线触发器区域
    //更改开关值

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    //在update中监控开关的值，一旦玩家进入视线触发器区域
    //执行对应的逻辑

    private void Update()
    {
        if (m_IsPlayerInRange)
        {
            //设置投射射线用到的方向矢量
            Vector3 direction = player.position - transform.position + Vector3.up;
            //创建射线
            Ray ray = new Ray(transform.position, direction);
            //射线集中对象，包含射线碰撞信息
            RaycastHit raycastHit;
            //使用物理系统发射射线，如果碰到物体
            //out 代表第二个参数是输出参数，可以 带出数据到参数中
            if (Physics.Raycast(ray, out raycastHit))
            {
                //如果碰到的是玩家
                if (raycastHit.collider.transform == player)
                {
                    //掉用GameEnding脚本中的抓到玩家的方法
                    gameEnding.Caught();
                }
            }
        }
    }
}
