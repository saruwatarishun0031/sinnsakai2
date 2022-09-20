using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField,Tooltip("��̃C���[�W")] 
    Image[] point1;
    [SerializeField,Tooltip("�e�I�u�W�F�N�g")] 
    GameObject pointParent;
    [SerializeField, Tooltip("�e")]
    GameObject _amo;
    [SerializeField, Tooltip("�Q�b�g���[�^�[")]
    Slider _pointSlider;
    [SerializeField] int XYspeed;
    [SerializeField] float rayDistance;
    public int p;
    const int winNum = 5;
    public float _getTime;
    public float _MaxGetTime;
    public int NumberOfBullets;
    [SerializeField] Transform muzzle;
    public float speed = 1000;
    public float _interval = 3;
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
        NumberOfBullets = 6;

    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Attke();
        _interval -= Time.deltaTime;
    }
    
    void Move()
    {
       
        float mousex = Input.GetAxis("Mouse X");
        transform.RotateAround(transform.position, transform.up, mousex);

        float x = Input.GetAxis("Horizontal12");
        float z = Input.GetAxis("Vertical12");

        var direction = transform.forward;
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.0f, 0.0f);
        Ray ray = new Ray(rayPosition, direction);
        Debug.DrawRay(rayPosition, direction * rayDistance, Color.red);
        Vector3 directionn = _rb.transform.forward * z + _rb.transform.right * x;
        directionn *= XYspeed * Time.deltaTime;
        _rb.AddForce(directionn, ForceMode.Impulse);
    }

    void Attke()
    {
        if (Input.GetButtonDown("Fire1") && NumberOfBullets >= 1 && _interval <= 0)
        {

            // �e�ۂ̕���
            GameObject bullets = Instantiate(_amo) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * speed;

            // Rigidbody�ɗ͂������Ĕ���
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // �e�ۂ̈ʒu�𒲐�
            bullets.transform.position = muzzle.position;
            NumberOfBullets -= 1;
            _interval = 2;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            NumberOfBullets = 6;
            _interval = 4;
        }
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            reset();
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
