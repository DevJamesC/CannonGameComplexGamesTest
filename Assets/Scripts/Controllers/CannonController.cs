using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IWantToWorkAtComplexGames
{
    /// <summary>
    /// This class handles the inputs and logic related to aiming and firing
    /// </summary>
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private Transform rotationalBody;
        [SerializeField] private float roatationSpeed;
        [SerializeField] private Weapon weaponPrefab;
        [SerializeField] private AudioSource horizontalAimAudioSource;
        [SerializeField] private AudioSource verticalAimAudioSource;


        private Vector3 lookVal;
        private float yRotation;
        private float zRotation;
        private PlayerInput playerInput;
        private Weapon currentWeapon;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }
        // Start is called before the first frame update
        void Start()
        {
            currentWeapon = Instantiate(weaponPrefab, rotationalBody);
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
        private void OnLook(InputAction.CallbackContext context)
        {
            lookVal = context.ReadValue<Vector2>();
            if (lookVal.x != 0)
                horizontalAimAudioSource.Play();
            else
                horizontalAimAudioSource.Stop();

            if (lookVal.y != 0)
                verticalAimAudioSource.Play();
            else
                verticalAimAudioSource.Stop();
        }

        //Gets invoked by Player Input Component via messaging to pass in new input
        private void OnAttack(InputAction.CallbackContext context)
        {
            currentWeapon.Use();
        }

        private void OnAttackCanceled(InputAction.CallbackContext context)
        {
            currentWeapon.StopUse();
        }

        private void OnEnable()
        {
            playerInput.actions["Look"].started += OnLook;
            playerInput.actions["Look"].performed += OnLook;
            playerInput.actions["Look"].canceled += OnLook;
            playerInput.actions["Attack"].started += OnAttack;
            playerInput.actions["Attack"].canceled += OnAttackCanceled;

        }

        private void OnDisable()
        {
            playerInput.actions["Look"].started -= OnLook;
            playerInput.actions["Look"].performed -= OnLook;
            playerInput.actions["Look"].canceled -= OnLook;
            playerInput.actions["Attack"].started -= OnAttack;
            playerInput.actions["Attack"].canceled -= OnAttackCanceled;

        }


    }
}
