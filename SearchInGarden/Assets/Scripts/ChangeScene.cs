using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指定

    public GameController gameCnt;

    string thisSceneName;


    void Start()
    {
        thisSceneName = SceneManager.GetActiveScene().name;
    }

    public void Load()
    {
        gameCnt.restart = false;
       
        //シーン切り替えのメソッド
        SceneManager.LoadScene(sceneName);
    }

    public void LoadThis()
    {
        gameCnt.restart = true;
        //シーン切り替えのメソッド
        SceneManager.LoadScene(thisSceneName);
    }
}
