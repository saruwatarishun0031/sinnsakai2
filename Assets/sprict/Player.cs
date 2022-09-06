using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float _speed;
    private Rigidbody _rb;
    [SerializeField] Image[] point1;
    [SerializeField] GameObject pointParent;
    int p;

    const int winNum = 5;
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
        Destroy(this.gameObject, 1.7f);
    }
}
