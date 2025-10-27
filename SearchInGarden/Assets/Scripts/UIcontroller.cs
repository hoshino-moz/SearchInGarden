using TMPro;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI remainTime;

    public GameManager gameMng;

    int timeRm;
    int scoreT;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeRm = (int)GameManager.remainingTime;
        scoreT = (int)GameManager.scoreTotal;
        score.text = scoreT.ToString();
        remainTime.text = timeRm.ToString();



    }
}
