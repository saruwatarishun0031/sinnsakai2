using System;
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
    public Text startText;
    [SerializeField, Tooltip("通知テキスト")]
    public Text _Text;
    [SerializeField, Tooltip("1")]
    GameObject _player1;
    [SerializeField, Tooltip("2")]
    GameObject _player2;
    [SerializeField, Tooltip("3")]
    GameObject _player3;
    [SerializeField, Tooltip("4")]
    GameObject _player4;
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
    //シングルトン（ここまで）
    // Start is called before the first frame update
    void Start()
    {
        startText = startText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCount();
        notice();
        Main();
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

    void notice()
    {
        Observer observer1 = new Observer("Player1");
        Observer observer2 = new Observer("Player2");
        Observer observer3 = new Observer("Player3");
        Observer observer4 = new Observer("Player4");
        Observable observable = new Observable();
        IDisposable disposable1 = observable.Subscribe(observer1);
        if (Player.Instance.p == 3)
        {
            observable.SendNotice();
        }
        IDisposable disposable2 = observable.Subscribe(observer2);
        IDisposable disposable3 = observable.Subscribe(observer3);
        IDisposable disposable4 = observable.Subscribe(observer4);

    }
    void Main()
    {

    }

    public void Winner()
    {
        Debug.Log("勝利");
    }

    public void LoadSceme(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
