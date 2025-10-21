using UnityEngine;

//ゲーム状態を管理する列挙型
public enum GameState
{
    playing,
    setflw,
    gameover,
    gameclear,
    setlevel,
}

public class GameManager : MonoBehaviour
{
    public static GameState gameState; //ゲームのステータス

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
