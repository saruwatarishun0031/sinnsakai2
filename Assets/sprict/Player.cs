using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float _speed;
    private Rigidbody _rb;
    [SerializeField,Tooltip("空のイメージ")] 
    Image[] point1;
    [SerializeField,Tooltip("親オブジェクト")] 
    GameObject pointParent;
    [SerializeField, Tooltip("弾")]
    GameObject _amo;
    [SerializeField, Tooltip("ゲットメーター")]
    Slider _pointSlider;
    public int p;
    const int winNum = 5;
    public float _getTime;
    public float _MaxGetTime;
    //シングルトンパターン（簡易型、呼び出される）
    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        point1 = pointParent.GetComponentsInChildren<Image>();
        p = 0;
        _getTime = 0;

    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attke();
    }

    void Move()
    {
        // 左右のキーの入力を取得
        float x = Input.GetAxis("Horizontal") * _speed;
        // 上下のキーの入力を取得
        float z = Input.GetAxis("Vertical") * _speed;
        _rb.AddForce(x, 0, z);
        float mousex = Input.GetAxis("Mouse X");
        transform.RotateAround(transform.position, transform.up, mousex);
    }

    void Attke()
    {

    }
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Fire1") && other.gameObject.tag == "Point")
        {
            _pointSlider.gameObject.SetActive(true);
            _getTime += Time.deltaTime;
            _pointSlider.value = (float)_getTime / (float)_MaxGetTime;
            if (_getTime > 5)
            {
                point1[p].color = new Color(0, 255, 237, 255);
                p++;
                Destroy(other.gameObject);
                reset();
            }

            if (p >= winNum)
            {
                GameManager.Instance.Winner();//シングルトン（呼び出し用）
            }
        }
    }
    void reset()
    {
        _getTime = 0;
        _pointSlider.value = (float)_getTime / (float)_MaxGetTime;
        _pointSlider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "amo")
        {
            Death();
        }
    }

    void Death()
    {
        this.gameObject.GetComponent<Player>().enabled = false;//動いて欲しくない
        Destroy(this.gameObject, 1.7f);
    }
}
