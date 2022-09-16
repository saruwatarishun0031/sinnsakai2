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
    [SerializeField,Tooltip("�e�I�u�W�F�N�g")] 
    GameObject pointParent;
    [SerializeField, Tooltip("�e")]
    GameObject _amo;
    [SerializeField, Tooltip("�Q�b�g���[�^�[")]
    Slider _pointSlider;
    public int p;
    const int winNum = 5;
    public float _getTime;
    public float _MaxGetTime;
    //�V���O���g���p�^�[���i�ȈՌ^�A�Ăяo�����j
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
                GameManager.Instance.Winner();//�V���O���g���i�Ăяo���p�j
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
        this.gameObject.GetComponent<Player>().enabled = false;//�����ė~�����Ȃ�
        Destroy(this.gameObject, 1.7f);
    }
}
