using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private float playerSpeed = 2.0f;

    private Quaternion cameraRotation;
    //float 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //move = transform.rotation;
        transform.Translate(move * Time.deltaTime * playerSpeed);
    }
}
