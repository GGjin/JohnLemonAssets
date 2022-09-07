using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalyMovement : MonoBehaviour
{

    //todo:
    //1.获取玩家输入，在场景中移动玩家角色游戏对象
    //2.移动时，除了位置外position，还需要考了转动rotation
    //3.需要将动画也考虑进去


    //创建一个3D矢量，来表示玩家角色的移动
    Vector3 m_Movement;

    //创建便了，获取用户输入的方向;
    float horizontal;
    float vertical;

    // 创建一个刚体对象
    Rigidbody m_Rigibody;
    Animator m_Animator;

    //用四元数对象 来表示3D游戏中的旋转
    //初始化为不旋转
    Quaternion m_Rotation = Quaternion.identity;

    //旋转速度
    public float turnSpeed = 20.0f;

    AudioSource m_AudioSource;


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigibody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


    }

    private void FixedUpdate()
    {
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement.Normalize();
        //判断是否有横向或者纵向移动
        bool hasHorizontal = !Mathf.Approximately(horizontal, 0.0f);
        bool hasVertical = !Mathf.Approximately(vertical, 0.0f);
        //只要有一个方向移动，就认为玩家角色处于移动状态
        bool isWalking = hasHorizontal || hasVertical;
        m_Animator.SetBool("isWalking", isWalking);
        //用三维矢量来表示旋转后的玩家角色
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (isWalking)
        {
            //保证不是每帧都重复从头播放
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

    }

    //当动画播放引发根移动时执行
    private void OnAnimatorMove()
    {

        m_Rigibody.MovePosition(m_Rigibody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigibody.MoveRotation(m_Rotation);
    }
}
