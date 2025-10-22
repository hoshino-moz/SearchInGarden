using UnityEngine;

//ゲーム状態を管理する列挙型

public class GameController : MonoBehaviour
{
    public int totalFlwCnt = 30;
    public int targetFlwCnt =4;
    //string[] caraName;

    public GameObject FlowerOrg;
    public GameObject FlowerTag;
    public GameObject InputCircle;

    Vector2 spawnArea = new Vector2(12f, 5f);

    bool isBlow = false;
    float inputTime = 0;
    float inputInterval = 0.5f;

    void Start()
    {
        GenerateDots(); //普通花のランダムな配置
        GenerateTags(); //特徴花のランダムな配置
        //ゲームステートをプレイにする
        GameManager.gameState = GameState.blowing;
    }

    // Update is called once per frame
    void Update()
    {
        //風送り待ち
        if (GameManager.gameState == GameState.blowing)
        {
            inputTime = inputTime + Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                // マウスのスクリーン座標を取得
                Vector3 mousePosition = Input.mousePosition;
                // スクリーン座標をワールド座標に変換
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                // z座標が0になるように調整（2D向け）
                worldPosition.z = 0f;

                // 指定した場所にプレハブを生成
                Instantiate(InputCircle, worldPosition, Quaternion.identity);

                isBlow = true;
            }

            //3秒たったら自動的に風発生
            if (inputTime > 3.0f)
            {
                //GameStateをplayingに
                GameManager.gameState = GameState.playing;
                //風が発生していなかったら　自動生成
                if (!isBlow)
                {
                    Vector3 pos = new(0, 0, 0);
                    Instantiate(InputCircle,pos , Quaternion.identity);
                }
            }

        }
    }

    void GenerateDots()
    {
        for (int i = 0; i < totalFlwCnt; i++)
        {
            // ランダムな位置を計算
            float randomX = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
            float randomY = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // お花のプレハブを生成
            Instantiate(FlowerOrg, randomPosition, Quaternion.identity);
        }
    }

    void GenerateTags()
    {
        //Debug.Log("特殊花　" + targetFlwCnt);
        for (int j = 0; j < targetFlwCnt; j++)
        {
           
            // ランダムな位置を計算
            float randomX = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
            float randomY = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0);

            // お花のプレハブを生成
            Instantiate(FlowerTag, randomPosition, Quaternion.identity);
        }
    }
}
