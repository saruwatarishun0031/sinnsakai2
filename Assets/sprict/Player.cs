using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float _speed;
    private Rigidbody _rb;
    [SerializeField,Tooltip("��̃C���[�W")] 
    Image[] point1;
    [SerializeField,Tooltip("�e�I�u�W�F�N�g")] GameObject pointParent;
    [SerializeField, Tooltip("�e")]
    GameObject _amo;
    public int p;
    const int winNum = 5;
    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
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
        // ���E�̃L�[�̓��͂��擾
        float x = Input.GetAxis("Horizontal") * _speed;
        // �㉺�̃L�[�̓��͂��擾
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
                GameManager.Instance.Winner();//�V���O���g���i�Ăяo���p�j
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
        this.gameObject.GetComponent<Player>().enabled = false;//�����ė~�����Ȃ�
        Destroy(this.gameObject, 1.7f);
        
    }
}
