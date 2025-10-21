using UnityEngine;

public class Flower : MonoBehaviour
{
    public float ampX = 2.0f;
    public float ampY = 0.1f;
    public float dampX = 1.0f;
    public float dampY = 1.0f;
    public float freqX = 5.0f;
    public float freqY = 2.0f;
    float timer;
    float moveX;
    float moveY;

    Rigidbody2D rbody; //PlayerについているRigidBody2Dを扱うための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        moveX=ampX*Mathf.Exp(-dampX* timer) * Mathf.Sin(freqX *timer* Mathf.PI);
        moveY=ampY*Mathf.Exp(-dampY* timer) * Mathf.Sin(freqY *timer* Mathf.PI);
        //Debug.Log("positionX = " + posX); 
        //Velocity 代入
        rbody.linearVelocity = new Vector2(moveX, moveY);
    }
}
