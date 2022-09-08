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
    [SerializeField,Tooltip("親オブジェクト")] GameObject pointParent;
    [SerializeField, Tooltip("弾")]
    GameObject _amo;
    public int p;
    const int winNum = 5;
    //シングルトンパターン（簡易型、呼び出される）
    public static Player Instance;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        point1 = pointParent.GetComponentsInChildren<Image>();
        p = 0;
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
        if (Input.GetButtonDown("Fire1") && other.gameObject.tag == "Point")
        {
            point1[p].color = new Color(0, 255, 237, 255);
            p++;
            Destroy(other.gameObject);

            if (p >= winNum)
            {
                GameManager.Instance.Winner();//シングルトン（呼び出し用）
            }
        }
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
