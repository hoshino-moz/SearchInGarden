using UnityEngine;
using System.Collections;

public class Inputcircle : MonoBehaviour
{
    float timer;
    float windSpeed = 4.0f;

    private CircleCollider2D circleCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ゲームオブジェクトのCircleCollider2Dコンポーネントを取得
        circleCollider = GetComponent<CircleCollider2D>();

        StartCoroutine(SelfClean());
    }

    // Update is called once per frame
    void Update()
    {

        //if (GameManager.gameState == GameState.setflw)
        //{
        timer = timer + Time.deltaTime;

        if (timer < 5.0f)
        {
            transform.localScale = new Vector3(timer * windSpeed, timer * windSpeed, 1);
            //circleCollider.radius = timer * windSpeed / 2;
        }
       
        //}
    }

    IEnumerator SelfClean()
    {

        // 1秒待機
        yield return new WaitForSeconds(7.0f);

        Debug.Log("ゲームクリア");
        Destroy(gameObject);

    }
}
