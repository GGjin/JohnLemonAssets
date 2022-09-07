using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{

    //设置NavMeshAgent组件对象，来获取当前游戏对象的导航网格代理组件
    NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        //获取组件对象
        navMeshAgent = GetComponent<NavMeshAgent>();
        //设置导航组件的导航起始点位
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    //每次刷新，都要去试着获取下一个路径点
    //如果满足要求，指定下一个路径点
    //并通过算法，让路径点位循环往复
    // Update is called once per frame
    void Update()
    {
        //当前游戏对象到指定的路径点的距离 如果小于最终停止距离
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            //通过除余%就是求余数的运算， 使得索引数可以从0到waypoints.lendthd -1 循环
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            //设置新的导航位置
            //navMeshAgent.SetDeatination 方法，可以设置移动的目标对象 参数是一个Vector3duix 
            //在游戏场景中，通过空的游戏对象，来代表每个导航点
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

    }
}
