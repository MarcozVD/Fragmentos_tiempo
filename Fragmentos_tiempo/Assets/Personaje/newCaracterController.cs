using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class newCharacterController : MonoBehaviour
{
    [Header("Movimiento")]
    public float WalkSpeed = 4f;
    public float SprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float rotationSpeed = 10f;
    public float mouseSensitivity = 1f;
    public float gravity = -20f;

    [Header("Referencias")]
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController characterController;
    private Vector3 Velocity;
    private float currentSpeed;
    private float yaw;
    private Vector3 externalVelocity = Vector3.zero;

    public bool IsMoving { get; private set; }
    public bool IsGrounded { get; private set; }

    // 🔹 Variables para plataforma
    private Transform currentPlatform;
    private Vector3 lastPlatformPosition;

    // 🔹 Giro tipo Crash
    [Header("Giro tipo Crash")]
    public float spinSpeed = 1080f; // velocidad del giro (grados/seg)
    private bool isSpinActive = false;
    private bool canSpin = false; // solo se activa al recoger la jeringa

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
        HandleSpin();
        UpdateAnimator();

        // Aplicar fuerza externa (como rebote)
        if (externalForce.magnitude > 0.1f)
        {
            characterController.Move(externalForce * Time.deltaTime);
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;
        cameraTransform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        IsGrounded = characterController.isGrounded;

        if (IsGrounded && Velocity.y < 0)
            Velocity.y = -2f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        IsMoving = inputDirection.magnitude > 0.1f;

        Vector3 moveDirection = Vector3.zero;

        if (IsMoving)
        {
            moveDirection = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f) * inputDirection;

            // Solo rotamos si no está girando
            if (!isSpinActive)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isSprinting ? SprintSpeed : WalkSpeed;
        }
        else
        {
            currentSpeed = 0f;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator?.SetBool("isJumping", true);
        }

        Velocity.y += gravity * Time.deltaTime;

        Vector3 finalMovement = (moveDirection * currentSpeed + externalVelocity) * Time.deltaTime;
        finalMovement.y += Velocity.y * Time.deltaTime;

        if (currentPlatform != null)
        {
            Vector3 platformMovement = currentPlatform.position - lastPlatformPosition;
            finalMovement += platformMovement;
            lastPlatformPosition = currentPlatform.position;
        }

        characterController.Move(finalMovement);

        if (IsGrounded && Velocity.y < 0f)
            animator?.SetBool("isJumping", false);
    }

    void UpdateAnimator()
    {
        float speedPercent = IsMoving ? (currentSpeed == SprintSpeed ? 1f : 0.5f) : 0f;
        animator?.SetFloat("Speed", speedPercent, 0.1f, Time.deltaTime);
        animator?.SetBool("IsGrounded", IsGrounded);
        animator?.SetFloat("VerticalSpeed", Velocity.y);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponent<MovingPlatform>() != null)
        {
            if (Vector3.Dot(hit.normal, Vector3.up) > 0.5f)
            {
                currentPlatform = hit.collider.transform;
                lastPlatformPosition = currentPlatform.position;
            }
        }
    }

    private void LateUpdate()
    {
        if (!IsGrounded || (currentPlatform != null && Vector3.Distance(transform.position, currentPlatform.position) > 5f))
        {
            currentPlatform = null;
        }
    }

    // 🔹 Giro tipo Crash
    void HandleSpin()
    {
        if (canSpin && Input.GetKey(KeyCode.E))
        {
            isSpinActive = true;
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            isSpinActive = false;
        }
    }

    // 🔹 Habilitar giro
    public void EnableSpinAbility()
    {
        canSpin = true;
        Debug.Log("Habilidad de giro activada");
    }

    private Vector3 externalForce = Vector3.zero;
    public void ApplyExternalForce(Vector3 force)
    {
        externalForce = force;
    }

    // 🔹 Método público para que JumpKill detecte si está girando
    public bool IsSpinning()
    {
        return isSpinActive;
    }
}
