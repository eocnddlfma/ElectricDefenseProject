using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    //private Vector3 playerVelocity;
    [SerializeField]private Vector3 playerRotated;
    //private bool groundedPlayer;
    [SerializeField]private float playerSpeed = 2.0f;
    [SerializeField]private float mouseSpeed = 2.0f;
    //[SerializeField]private float jumpHeight = 1.0f;
    //[SerializeField]private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.rotation * Quaternion.Euler(playerRotated) * move;
        controller.Move(move * Time.deltaTime * playerSpeed);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSpeed);
;            
    }
}
