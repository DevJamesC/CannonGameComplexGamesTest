using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    [SerializeField] private Transform rotationalBody;
    [SerializeField] private float roatationSpeed;

    private Vector3 lookVal;
    private float yRotation;
    private float zRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AimCannon();
    }

    //Handles rotating the body of the cannon
    private void AimCannon()
    {
        yRotation = rotationalBody.eulerAngles.y;
        zRotation = rotationalBody.eulerAngles.z;

        yRotation += (lookVal.x * roatationSpeed * Time.deltaTime);
        zRotation += (lookVal.y * roatationSpeed * Time.deltaTime);

        zRotation = Mathf.Clamp(zRotation, 1, 120);

        rotationalBody.rotation = Quaternion.Euler(0, yRotation, zRotation);
    }

    //Gets invoked by Player Input Component via messaging to pass in new input
    private void OnLook(InputValue value)
    {
        lookVal = value.Get<Vector2>();
    }
}
