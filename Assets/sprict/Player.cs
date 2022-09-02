using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 _moveDir;
    public float x = 1;
    public float z = 1;
    // Start is called before the first frame update
    void Start()
    {
        _moveDir = new Vector3(x,0,z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_moveDir);
    }

    public void OnFier(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("ieeeeei");
        }
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        _moveDir = context.ReadValue<Vector3>();
    }
}
