using UnityEngine;

public class Flower : MonoBehaviour
{
    public float ampX;  //風方向の振幅
    public float dampX;  //風方向の減衰
    public float freqX;  //風方向の周期
    public float ampY;  //横方向の振幅
    public float dampY;  //横方向の減衰
    public float freqY;  //横方向の周期
    float timer;
    float moveX;
    float moveY;
    bool isMove;
    float diffX;
    float diffY;
    bool colliOnce;


    Rigidbody2D rbody; //PlayerについているRigidBody2Dを扱うための変数

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>(); //Playerについているコンポーネント情報を取得
    }

    // Update is called once per frame
    void Update()
    {
        //風を送るとどっちらかるの対策
        // オブジェクトの動きが鈍くなったら固定する
        //if (rbody.linearVelocity.magnitude < 0.01f)
        //{
        //    rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        //}
        //else
        //{
        //    rbody.constraints = RigidbodyConstraints2D.None;
        //}
        // オブジェクトの動きが鈍くなったら固定する

        //お花を動かす　playing と blowingの間
        if (GameManager.gameState == GameState.playing || GameManager.gameState == GameState.blowing)
        {
            if (isMove)
            {
                MoveFlower();
            }

        }

    }

    //InputCircleとぶつかったらお花の動き発動
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliOnce) return;

        //ぶつかった相手がInputCircleだったら
        if (collision.gameObject.CompareTag("InputCircle"))
        {
            isMove = true;
            colliOnce = true;

            //花とInputCircleとの差を取得し、方向を決める　風の方向に揺れる
            Vector3 v = (transform.position - collision.gameObject.transform.position).normalized;
            diffX = v.x;
            diffY = v.y;
        }
    }

    //実際の動き
    void MoveFlower()
    {
        timer = timer + Time.deltaTime;

        moveX = ampX * Mathf.Exp(-dampX * timer) * Mathf.Sin(freqX * timer * Mathf.PI);
        moveY = ampY * Mathf.Exp(-dampY * timer) * Mathf.Cos(freqY * timer * Mathf.PI);
        //Debug.Log("positionX = " + posX); 
        //Velocity 代入
        //rbody.linearVelocity = new Vector2(moveX, moveY);
        rbody.linearVelocity = new Vector2((moveX * diffX) + (moveY * -diffY), (moveX * diffY) + (moveY * diffX));
    }
}
