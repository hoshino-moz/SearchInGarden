using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; //切り替えたいシーン名を指定
    public bool toTitle; //タイトル切り替えのフラグ

 
    public void Load()
    {
        //シーン切り替えのメソッド
        SceneManager.LoadScene(sceneName);
    }

}
