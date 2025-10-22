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


    void Start()
    {
        //まずはお花セット状態にする
        gameState = GameState.setflw;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
