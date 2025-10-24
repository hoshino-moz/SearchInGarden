using UnityEngine;

public class DummyDot : MonoBehaviour
{
    public float ampX = 1;  //風方向の振幅
    public float dampX = 1;  //風方向の減衰
    public float freqX = 1;  //風方向の周期
    public float ampY = 1;  //横方向の振幅
    public float dampY = 1;  //横方向の減衰
    public float freqY = 1;  //横方向の周期
    float timer;
    float moveX;
    float moveY;
    bool isMove;
    float diffX;
    float diffY;
    bool colliOnce;

    int totalFlwCnt;

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
        if (rbody.linearVelocity.magnitude < 0.01f)
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rbody.constraints = RigidbodyConstraints2D.None;
        }
        // オブジェクトの動きが鈍くなったら固定する

        //お花を動かす　playing と blowingの間
        //if (GameManager.gameState == GameState.setflw)
        //{
            //if (isMove)
            //{
                MoveFlower();
            //}

        //}

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
        rbody.linearVelocity = new Vector2(moveX, moveY);
    }
}
