using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameManager GameManeger;

    public string sceneName; //切り替えたいシーン名を指定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.scoreTotal = 0;
    }

    public void Load()
    {
      
        //シーン切り替えのメソッド
        SceneManager.LoadScene(sceneName);
    }
}
