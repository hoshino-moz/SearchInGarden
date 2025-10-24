using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
    public int prefabNum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("僕は特殊花" +  prefabNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
