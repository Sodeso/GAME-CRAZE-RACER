using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooping : MonoBehaviour
{
    public float loopSpeed;
    SpriteRenderer _spriteRender;

    private void Awake()
    {
        _spriteRender = GameObject.Find("Background").GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        _spriteRender.material.mainTextureOffset += new Vector2(0f, loopSpeed * Time.deltaTime);
    }
}
