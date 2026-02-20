using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float accelerationFactor = 15f;
    [SerializeField] private float decelerationFactor = 20f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 0.5f;
    [SerializeField] private float gravity = -15f;

    [Header("Dash")]
    [SerializeField] private float dashingCooldown = 1.5f;
    [SerializeField] private float dashingTime = 1f;
    [SerializeField] private float dashingSpeed = 8f;

    [Header("References")]
    [SerializeField] private Animator animator;

    private CharacterController _characterController;
    private InputSystem_Actions _playerInputActions;

    private Vector3 _input;
    private Vector3 _velocity;
    private float _currentSpeed;

    private bool _isGrounded;
    private bool _canDash = true;
    private bool _isDashing;

    private bool _jumpInput;
    private bool _dashInput;
    private bool _attackInput;

    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();
        _characterController = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0f)
            _velocity.y = -2f;

        GatherInput();

        HandleJump();
        HandleAttack();
        ApplyGravity();

        Look();
        CalculateSpeed();
        Move();

        if (_dashInput && _canDash)
            StartCoroutine(Dash());

        UpdateAnimator();
    }

    private void HandleAttack()
    {
        if (_attackInput)
        {
            if (animator != null)
                animator.SetTrigger("Attack");
        }
    }

    private void HandleJump()
    {
        if (_jumpInput && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
                animator.SetTrigger("Jump");
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        yield return new WaitForSeconds(dashingTime);

        _isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        _canDash = true;
    }

    private void CalculateSpeed()
    {
        float targetSpeed = _input == Vector3.zero ? 0f : maxSpeed;
        float accel = _input == Vector3.zero ? decelerationFactor : accelerationFactor;

        _currentSpeed = Mathf.MoveTowards(
            _currentSpeed,
            targetSpeed,
            accel * Time.deltaTime
        );
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45f, 0));
        Vector3 direction = isoMatrix.MultiplyPoint3x4(_input);

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void Move()
    {
        if (_isDashing)
        {
            _characterController.Move(transform.forward * dashingSpeed * Time.deltaTime);
            return;
        }

        Vector3 movement =
            transform.forward * _currentSpeed * Time.deltaTime +
            Vector3.up * _velocity.y * Time.deltaTime;

        _characterController.Move(movement);
    }

    private void UpdateAnimator()
    {
        if (animator == null) return;

        animator.SetFloat("speed", _currentSpeed);
        animator.SetBool("isFalling", !_isGrounded && _velocity.y < 0f);
        animator.SetBool("isDashing", _isDashing);
    }

    private void GatherInput()
    {
        Vector2 moveInput = _playerInputActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(moveInput.x, 0f, moveInput.y);

        _jumpInput = _playerInputActions.Player.Jump.triggered;
        _dashInput = _playerInputActions.Player.Sprint.IsPressed();
        _attackInput = _playerInputActions.Player.Attack.triggered;
    }
}
