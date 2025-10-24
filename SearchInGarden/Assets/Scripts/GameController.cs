using System.Threading;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public int totalFlwCnt = 30;
    public int targetFlwCnt =4;
    //string[] caraName;

    public GameObject FlowerOrg;
    public GameObject FlowerTag;
    
    public GameObject InputCircle;
    public GameObject ClickTrigger;

    Vector2 spawnArea = new Vector2(12f, 5f);

    bool isBlow = false;
    float inputTime = 0;
    float playingTime = 25;
    float score;
    float stageLevel = 1;
    float timer;

    float[] posFlwX;
    float[] posFlwY;
    int[] flowerNum;

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
        WindBlowing();
        }

        //ターゲットピック動作
        if (GameManager.gameState == GameState.playing)
        {
            playingTime = playingTime - Time.deltaTime;
            if (playingTime > 0)
            {
                // マウスクリックを検知
                if (Input.GetMouseButtonDown(0)) // 0は左クリック
                {
                    // マウスの位置をワールド座標に変換
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // マウスの位置からRayを飛ばし、衝突したコライダーを取得
                    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                    // 何らかのオブジェクトに衝突した場合
                    if (hit.collider != null)
                    {
                        // 衝突したオブジェクトを選択する処理を記述
                        //Debug.Log("選択されたオブジェクト：" + hit.collider.gameObject.name);
                        if (hit.collider.gameObject.name == "ClickTrigger")
                        {
                            Debug.Log("正解に当たった");
                            //GameObject parentObject = hit.transform.parent.gameObject;
                            //Flower getFlw = parentObject.GetComponent<Flower>();
                            //int getFlwNum = getFlw.prefabNum;
                            score = score + stageLevel * playingTime;
                            Debug.Log("score " + stageLevel * playingTime);
                            //Debug.Log("Prefab No. " + getFlwNum);

                            //選択されたオブジェクトのアルファ値を変える
                            //SpriteRenderer spriteRenderer = parentObject.GetComponent<SpriteRenderer>();
                            //if (spriteRenderer != null)
                            //{
                            //    spriteRenderer.color = new Color(1,1,1,0.5f);
                            //}
                        }
                    }
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
            //posFlwX[j] = randomX;
            //posFlwY[j] = randomY;
            //flowerNum[j] = j;

            // お花のプレハブを生成
            GameObject newFlw = Instantiate(FlowerTag, randomPosition, Quaternion.identity);
            GameObject crickTr = GameObject.Find("ClickTrigger");
            ClickTrigger criTr = crickTr.GetComponent<ClickTrigger>();
            //criTr.prefabNum = j;
        }

        timer = timer + Time.deltaTime;
        Debug.Log("時間　" +  timer);
    }

    void WindBlowing()
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
            //3秒たったらGameStateをplayingに
            GameManager.gameState = GameState.playing;
            //風が発生していなかったら　自動生成
            if (!isBlow)
            {
                Vector3 pos = new(0, 0, 0);
                Instantiate(InputCircle, pos, Quaternion.identity);
            }
        }
    }
}
