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

        /// <summary>
        /// Handles rotating the body of the cannon
        /// </summary>
        private void AimCannon()
        {
            yRotation = rotationalBody.eulerAngles.y;
            zRotation = rotationalBody.eulerAngles.z;

            yRotation += (lookVal.x * roatationSpeed * Time.deltaTime);
            zRotation += (lookVal.y * roatationSpeed * Time.deltaTime);

            zRotation = Mathf.Clamp(zRotation, 1, 120);

            rotationalBody.rotation = Quaternion.Euler(0, yRotation, zRotation);
        }

        /// <summary>
        /// Handles setting and updating the Look values from input
        /// </summary>
        /// <param name="context"></param>
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

        /// <summary>
        /// Uses the weapon it is currently holding
        /// </summary>
        /// <param name="context"></param>
        private void OnAttack(InputAction.CallbackContext context)
        {
            if (currentWeapon == null)
                currentWeapon = Instantiate(weaponPrefab, rotationalBody);

            currentWeapon.Use();
        }

        /// <summary>
        /// Stops using the weapon it is currently holding
        /// </summary>
        /// <param name="context"></param>
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
