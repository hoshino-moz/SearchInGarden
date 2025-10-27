
using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{
    public int totalFlwCnt = 30;
    public int targetFlwCnt = 4;
    //string[] caraName;

    public GameObject FlowerOrg;
    public GameObject FlowerTag;

    public GameObject InputCircle;
    public GameObject ClickTrigger;

    public SpriteRenderer Indicator1;
    public SpriteRenderer Indicator2;
    public SpriteRenderer Indicator3;
    public SpriteRenderer Indicator4;

    public GameObject gameClearPanel;
    public GameObject timeOverPanel;
    

    Vector2 spawnArea = new Vector2(12f, 5f);

    bool isBlow = false;
    float inputTime = 0;
    public float playingTime = 25;
    public float score;
    float stageLevel = 1;
    float timer;
    int isFindedCnt = 0;

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
            //Lucky Flower をPicUpした時の動作
            playingTime = playingTime - Time.deltaTime;
            if (playingTime > 0)
            {
                PickUpLuckyFlw();
            }

            //ゲームクリアしたら
            if (isFindedCnt >= 4 || playingTime <0 )
            {
                StartCoroutine(GameClear());
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
            //Debug.Log("選択されたオブジェクト：" + crickTr.gameObject.name);
            criTr.prefabNum = j;
            Debug.Log("この花の番号は　" + criTr.prefabNum);
        }

        timer = timer + Time.deltaTime;
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

    IEnumerator GameClear()
    {

        // 1秒待機
        yield return new WaitForSeconds(1.0f);

        Debug.Log("ゲームクリア");
        GameManager.gameState = GameState.gameclear;
        if (isFindedCnt >= 4)
        {
            gameClearPanel.SetActive(true);
        }
        if (playingTime < 0)
        {
            timeOverPanel.SetActive(true);
        }
        
    }

    void PickUpLuckyFlw()
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

                    ClickTrigger click = hit.collider.gameObject.GetComponent<ClickTrigger>();
                    if (click.isFinded == false)
                    {
                        isFindedCnt++;
                        score = score + stageLevel * playingTime;
                        Debug.Log("score " + stageLevel * playingTime);
                        Debug.Log(isFindedCnt + "個目の正解です");
                        PickUpUI();
                        click.isFinded = true;
                    }

                }
            }
        }
    }

    void PickUpUI ()
    {
        if (isFindedCnt == 1)
        {
            Indicator1.color = Color.yellow;
        }
        if (isFindedCnt == 2)
        {
            Indicator2.color = Color.yellow;
        }
        if (isFindedCnt == 3)
        {
            Indicator3.color = Color.yellow;
        }
        if (isFindedCnt == 4)
        {
            Indicator4.color = Color.yellow;
        }
    }
}
