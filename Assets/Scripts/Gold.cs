using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(0, -GameObject.Find("Background").gameObject.GetComponent<BackgroundLooping>().loopSpeed * GameObject.Find("Player").gameObject.GetComponent<Player>().playerSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Bot")
        {
            Destroy(gameObject);
        }
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<GameManager>().gold++;
        }
    }
}
