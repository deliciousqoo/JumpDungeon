using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject EffectPrefab;

    protected BoxCollider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected CompositeCollider2D _composite;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _composite = GetComponent<CompositeCollider2D>();
    }

    
}
