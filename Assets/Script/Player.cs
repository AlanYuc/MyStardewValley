using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;//移动速度
    public bool isMove = false;//是否移动
    public Vector2 movementDir;//移动方向，总共八个

    public Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
    }
}
