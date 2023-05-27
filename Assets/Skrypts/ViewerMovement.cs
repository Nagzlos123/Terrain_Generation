using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 moveDirection;
    private MapGenerator mapGenerator;

    private CharacterController characterController;
    // Start is called before the first frame update
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mapGenerator =FindObjectOfType<MapGenerator>();
        mapGenerator.DrawMap();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection *= moveSpeed;
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
