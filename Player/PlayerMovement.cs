using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private const float walkSpeed = 3f;
    private const float runSpeed = 5.5f;
    private const float playerRotationSpeed = 0.1f;
    private float rotationSmoothVelocity;
    private Vector3 moveDirection;
    [SerializeField] private Transform mainCam;
    private Vector3 playerVelocity;
    private const float gravity = -9.81f;
    private bool isAiming;
    // With { get; private set; } I can publicly get Instance, but cannot set it, only from inside this class can it be managed.
    private static PlayerMovement _instance;
    public static PlayerMovement Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        Movement();
        Aiming();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (direction.magnitude >= 0.1f && !isAiming)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, playerRotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            ToggleMoveSpeeds();
        }
    }

    private void ToggleMoveSpeeds()
    {
        if (Input.GetKey("left shift") && !isAiming)
        {
            controller.Move(moveDirection.normalized * runSpeed * Time.deltaTime);
        }
        else if (!isAiming)
        {
            controller.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
        }
    }

    private void Aiming()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            Vector3 direction = new Vector3(0f, 0f, 0f).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, playerRotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            isAiming = false;
        }
    }
}
