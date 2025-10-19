using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    /// <summary>
    /// 初始的透明度
    /// </summary>
    public float originAlpha = 1.0f;
    /// <summary>
    /// 角色到建筑物后面时的透明度
    /// </summary>
    public float targetAlpha = 0.5f;
    /// <summary>
    /// 渐变时间
    /// </summary>
    public float fadeDuration = 0.2f;
    /// <summary>
    /// SpriteRenderer组件
    /// </summary>
    public SpriteRenderer _sp;
    /// <summary>
    /// 当前运行的协程
    /// </summary>
    public Coroutine _coroutine;

    private void Awake()
    {
        _sp = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //关闭当前运行的协程，确保下一个协程能正常开始
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(FadeToAlpha(targetAlpha));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(FadeToAlpha(originAlpha));
        }
    }

    public IEnumerator FadeToAlpha(float target)
    {
        Color startColor = _sp.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, target);

        float time = 0;

        while(time < fadeDuration)
        {
            _sp.color = Color.Lerp(startColor, endColor, time / fadeDuration);
            time += Time.deltaTime;

            yield return null;
        }

        _sp.color = endColor;
    }
}
