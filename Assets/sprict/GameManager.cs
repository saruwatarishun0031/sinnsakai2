using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("�X�^�[�g")]
    Image start;
    [SerializeField, Tooltip("�J�E���g�e�L�X�g")]
    public Text startText;
    [SerializeField, Tooltip("�ʒm�e�L�X�g")]
    public Text _Text;
    [SerializeField, Tooltip("����UI")]
    Canvas _Win;
    public float s ;
    List<string> PlayerList = new List<string>();

    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
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
    //�V���O���g���i�����܂Łj
    // Start is called before the first frame update
    void Start()
    {
        startText = startText.GetComponent<Text>();
        PlayerList.Add("Player1");
        PlayerList.Add("Player2");
        PlayerList.Add("Player3");
        PlayerList.Add("Player4");
    }

    // Update is called once per frame
    void Update()
    {
        StartCount();
        notice();
        Main();
        Cursor.lockState = CursorLockMode.Locked;
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
            startText.text = "�X�^�[�g!";
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
        if (Player2.Instance.p == 3)
        {

        }
        IDisposable disposable3 = observable.Subscribe(observer3);
        if (Player3.Instance.p == 3)
        {

        }
        IDisposable disposable4 = observable.Subscribe(observer4);
        if (Player4.Instance.p == 3)
        {

        }

    }
    void Main()
    {
        int PlayerCount = PlayerList.Count;
        Debug.Log(PlayerCount);
        if (Player.Instance._Death == true)
        {
            PlayerList.Remove("Player1");
        }
        else if (Player2.Instance._Death == true)
        {
            PlayerList.Remove("Player2");
        }
        else if (Player3.Instance._Death == true)
        {
            PlayerList.Remove("Player3");
        }
        else if (Player4.Instance._Death == true)
        {
            PlayerList.Remove("Player4");
        }

        if (PlayerCount ==1)
        {
            Winner();
        }
    }

    public void Winner()

    {
        Debug.Log("����");
        _Win.enabled = true;
    }
}
