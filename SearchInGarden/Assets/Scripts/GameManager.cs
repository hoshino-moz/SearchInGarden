using UnityEngine;

//ゲーム状態を管理する列挙型
public enum GameState
{
    playing,
    setflw,
    gameover,
    gameclear,
    setlevel,
    blowing,
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState; //ゲームのステータス

    public static float scoreTotal;
    public static float remainingTime;

    public GameController gameCnt;

    void Start()
    {
        //まずはお花セット状態にする
        gameState = GameState.setflw;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime = gameCnt.playingTime;
        scoreTotal = gameCnt.score;
        //Debug.Log("これはGameManagerのremainingTimeです" +  remainingTime);
        //Debug.Log("これはGameManagerのscoreTotalです" +  scoreTotal);
    }
}
