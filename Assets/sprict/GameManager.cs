using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("スタート")]
    Image start;
    [SerializeField, Tooltip("カウントテキスト")]
    Text startText;
    public float s ;

    //シングルトンパターン（簡易型、呼び出される）
    public static GameManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        startText = startText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCount();
    }
    public void StartCount()
    { 
        startText.text = s.ToString("0");
        if (s <= 3)
        {
            s -= Time.deltaTime;
        }
        if (s < 0.6)
        {
            start.enabled = false;
            startText.text = "スタート!";
        }
        if (s < 0.2)
        {
            startText.enabled = false;
        }

    }

    public void Winner()
    {

    }

    public void LoadSceme(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }
}
