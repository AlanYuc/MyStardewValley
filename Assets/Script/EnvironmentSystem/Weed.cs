using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weed : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpriteRenderer _playerSpriteRenderer = collision.GetComponent<SpriteRenderer>();

            if(_playerSpriteRenderer!=null && _spriteRenderer != null)
            {
                bool isPlayerBehind = collision.transform.position.y > this.transform.position.y;

                _spriteRenderer.sortingOrder = isPlayerBehind ? 
                    _playerSpriteRenderer.sortingOrder + 1 : _playerSpriteRenderer.sortingOrder - 1;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sortingOrder = 0;
            }
        }
    }

    /// <summary>
    /// ¸î²Ý
    /// </summary>
    public void Mow()
    {
        ItemData weedItemData = DataManager.Instance.GetItemData(101);

        BackpackSystem.Instance.TryAddItem(weedItemData, 1);

        Destroy(gameObject);
    }
}
