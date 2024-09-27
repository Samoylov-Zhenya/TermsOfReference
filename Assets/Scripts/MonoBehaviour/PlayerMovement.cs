using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;             // Walking speed
    public float runSpeed = 10f;             // Running speed when Shift is held
    public float accelerationTime = 0.5f;    // Time to reach max speed
    public float decelerationTime = 0.5f;    // Time to stop moving

    [Header("Acceleration Curves")]
    public AnimationCurve accelerationCurve; // Curve for acceleration
    public AnimationCurve decelerationCurve; // Curve for deceleration TODO

    [Header("Look Settings")]
    public float lookSensitivity = 2f;       // Mouse sensitivity for looking around

    [SerializeField] private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _currentSpeed = 0f;
    private float _accelerationTimer = 0f;
    private float _decelerationTimer = 0f;
    private float _rotationX = 0f;

    //Debug
#if UNITY_EDITOR
    [SerializeField] private Vector3 _movement;
#endif

    void Start()
    {
        _characterController ??= GetComponent<CharacterController>();

        // Initialize curves if not set
        if (accelerationCurve == null)
            accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        if (decelerationCurve == null)
            decelerationCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    //if you add gravity, you'll need to add jumps too.
    //private void DoGravity() => _characterController.Move(Vector3.down);

    void HandleMovement()
    {
        // Get input from WASD and arrow keys
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        // Combine inputs into a direction vector
        Vector3 inputDirection = new Vector3(inputX, 0, inputZ).normalized;

        // Check if the player is moving
        if (inputDirection.magnitude > 0)
        {
            // Reset deceleration timer and increment acceleration timer
            _decelerationTimer = 0f;
            _accelerationTimer += Time.deltaTime / accelerationTime;
            _accelerationTimer = Mathf.Clamp01(_accelerationTimer);

            // Determine target speed (walk or run)
            float targetSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runSpeed : walkSpeed;

            // Calculate current speed using the acceleration curve
            _currentSpeed = targetSpeed * accelerationCurve.Evaluate(_accelerationTimer);
        }
        else
        {
            // Reset acceleration timer and increment deceleration timer
            _accelerationTimer = 0f;
            _decelerationTimer += Time.deltaTime / decelerationTime;
            _decelerationTimer = Mathf.Clamp01(_decelerationTimer);

            // Decrease speed using the deceleration curve
            _currentSpeed = _currentSpeed * decelerationCurve.Evaluate(_decelerationTimer);
        }

        // Move the character
        Vector3 movement = transform.TransformDirection(inputDirection) * _currentSpeed * Time.deltaTime;
#if UNITY_EDITOR
        _movement = movement;
#endif
        _characterController.Move(movement);
    }

    void HandleMouseLook()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate the player horizontally
        transform.Rotate(0, mouseX, 0);

        // Calculate vertical rotation and clamp it
        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90, 90);

        // Apply vertical rotation to the camera
        Camera.main.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }
}
