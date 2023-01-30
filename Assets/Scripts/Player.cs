using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    public float playerSpeed;
    float _isRotate;
    public float sideSpeed = 3.5f;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Rotate();
        _rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * sideSpeed, Input.GetAxisRaw("Vertical") * playerSpeed);
    }
    void Rotate()
    {
        _isRotate = Input.GetAxisRaw("Horizontal");

        if (_isRotate == 0f)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 1500);
        }
        //left
        else if (_isRotate == -1f)
        {

            if (transform.rotation.z == 0f)
            {
                Quaternion target = Quaternion.Euler(0, 0, 5);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 1500);
            }
        }
        //right
        else if (_isRotate == 1f)
        {
            if (transform.rotation.z == 0f)
            {
                Quaternion target = Quaternion.Euler(0, 0, -5);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 1500);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bot")
        {
            Time.timeScale = 0;
            if(GameObject.Find("GameManager").GetComponent<GameManager>().currentScore > GameObject.Find("GameManager").GetComponent<GameManager>().maxScore)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().maxScore = GameObject.Find("GameManager").GetComponent<GameManager>().currentScore;
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().lastScore = GameObject.Find("GameManager").GetComponent<GameManager>().currentScore;
            GameObject.Find("GameManager").GetComponent<GameManager>().currentScore = 0;
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseGame();
        }
    }
}
