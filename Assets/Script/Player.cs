using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 5f;
    /// <summary>
    /// 是否移动
    /// </summary>
    public bool isMove = false;
    /// <summary>
    /// 移动方向，总共八个
    /// </summary>
    public Vector2 movementDir;
    /// <summary>
    /// 记录停止移动前的移动方向，用于确定角色动画朝向
    /// </summary>
    public Vector2 lastMovementDir;
    /// <summary>
    /// 角色刚体
    /// </summary>
    public Rigidbody2D _rb;
    /// <summary>
    /// 角色动画器
    /// </summary>
    public Animator _anim;

    private void Awake()
    {
        Instance = this;

        _rb     = GetComponent<Rigidbody2D>();
        _anim   = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //处理相机、物理计算

        //这里使用Input.GetAxisRaw而不是Input.GetAxis
        //GetAxisRaw的返回值仅 -1、0、1 三个整数之一，适合2D平台跳跃、射击的快速操作
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //由于返回值仅 -1、0、1，因此单位化后，有八个方向
        movementDir = new Vector2(horizontal, vertical).normalized;

        //使用刚体进行移动
        _rb.velocity = movementDir * moveSpeed;

        //判断是否移动
        if(movementDir == Vector2.zero)
        {
            isMove = false;
        }
        else
        {
            isMove= true;
        }

        //将参数赋值到animator
        _anim.SetBool("IsMove", isMove);

        //记录并将最后的移动方向赋值到animator,只记录不为0
        if(movementDir != Vector2.zero)
        {
            lastMovementDir = movementDir;
            _anim.SetFloat("LastHorizontal", lastMovementDir.x);
            _anim.SetFloat("LastVertical", lastMovementDir.y);
        }
    }
}
