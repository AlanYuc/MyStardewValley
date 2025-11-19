using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements.Experimental;

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

    /// <summary>
    /// 玩家走路特效
    /// </summary>
    public GameObject walkEffect;
    public float walkEffectOffset;

    /// <summary>
    /// 音效组件
    /// </summary>
    public AudioSource _walkAudioSource;

    //玩家信息

    /// <summary>
    /// 玩家名称
    /// </summary>
    public string playerName;
    /// <summary>
    /// 农场名称
    /// </summary>
    public string farmName;
    /// <summary>
    /// 玩家最喜欢的物品名称
    /// </summary>
    public string favoriteName;
    /// <summary>
    /// 经验值
    /// </summary>
    public int exp;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;

    private void Awake()
    {
        Instance = this;

        _rb     = GetComponent<Rigidbody2D>();
        _anim   = GetComponent<Animator>();
        walkEffect = transform.Find("WalkEffect").gameObject;
        _walkAudioSource = transform.Find("WalkMusic").GetComponent<AudioSource>();

        walkEffectOffset = 0.2f;

        _walkAudioSource.clip = MusicManager.Instance.GetAudioClip("走路");
        _walkAudioSource.loop = true;
        _walkAudioSource.playOnAwake = false;
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
            walkEffect.SetActive(false);
        }
        else
        {
            isMove= true;
            walkEffect.SetActive(true);
            SetWalkEffectPosition(movementDir);
        }

        //将参数赋值到animator
        _anim.SetBool("IsMove", isMove);

        //播放走路音效
        PlayAudio(isMove);

        //记录并将最后的移动方向赋值到animator,只记录不为0
        if (movementDir != Vector2.zero)
        {
            lastMovementDir = movementDir;
            _anim.SetFloat("LastHorizontal", lastMovementDir.x);
            _anim.SetFloat("LastVertical", lastMovementDir.y);
        }
    }

    public void SetWalkEffectPosition(Vector2 movementDir)
    {
        walkEffect.transform.position =
            transform.position +
            new Vector3(0, 0.2f, 0) -
            new Vector3(movementDir.x * walkEffectOffset, movementDir.y * walkEffectOffset, 0);
    }

    /// <summary>
    /// 加载玩家数据
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadGame(SaveData saveData)
    {
        //更新位置
        Vector3 position = new Vector3();
        position.x = saveData.playerSaveData.player_pos_x;
        position.y = saveData.playerSaveData.player_pos_y;
        position.z = saveData.playerSaveData.player_pos_z;
        transform.position = position;

        //更新数据
        playerName = saveData.playerSaveData.player_name;
        farmName = saveData.playerSaveData.farm_name;
        favoriteName = saveData.playerSaveData.favorite;
        exp = saveData.playerSaveData.exp;
        level = saveData.playerSaveData.level;
    }

    /// <summary>
    /// 保存玩家数据
    /// </summary>
    /// <returns></returns>
    public PlayerSaveData SaveGame()
    {
        PlayerSaveData playerSaveData = new PlayerSaveData();
        playerSaveData.player_pos_x = transform.position.x;
        playerSaveData.player_pos_y = transform.position.y;
        playerSaveData.player_pos_z = transform.position.z;
        playerSaveData.player_name = playerName;
        playerSaveData.farm_name = farmName;
        playerSaveData.favorite = favoriteName;
        playerSaveData.exp = exp;
        playerSaveData.level = level;

        return playerSaveData;
    }

    /// <summary>
    /// 播放走路音效
    /// </summary>
    /// <param name="isPlay">是否需要播放</param>
    public void PlayAudio(bool isPlay)
    {
        //已经在走路了
        if (_walkAudioSource.isPlaying && isPlay)
        {
            return;
        }

        //开始走路
        if (isPlay)
        {
            _walkAudioSource.time = 0;//从0开始播放
            _walkAudioSource.Play();
        }
        //停下
        else
        {
            _walkAudioSource.Stop();
        }
    }
}
