using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class newCharacterController : MonoBehaviour
{
    [Header("Movimiento")]
    public float WalkSpeed = 8f;
    public float SprintSpeed = 12f;
    public float jumpHeight = 5f;
    public float rotationSpeed = 10f;
    public float mouseSensitivity = 150f;
    public float gravity = -20f;

    [Header("Referencias")]
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController characterController;
    private Vector3 Velocity;
    private float currentSpeed;
    private Vector3 externalVelocity = Vector3.zero;

    public bool IsMoving { get; private set; }
    public bool IsGrounded { get; private set; }

    // Plataforma actual (SOLO ESTA)
    private MovingPlatform currentPlatform;

    [Header("Giro tipo Crash")]
    public float spinSpeed = 1080f;
    private bool isSpinActive = false;
    private bool canSpin = false;

    private Vector3 externalForce = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!characterController.enabled)
            return;

        HandleCameraRotation();
        CheckPlatform();     // ← Detecta plataforma antes de mover
        HandleMovement();
        HandleSpin();
        UpdateAnimator();

        if (externalForce.magnitude > 0.1f)
        {
            characterController.Move(externalForce * Time.deltaTime);
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
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
            moveDirection = (cameraTransform.forward * vertical + cameraTransform.right * horizontal);
            moveDirection.y = 0f;
            moveDirection.Normalize();

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

        // ---------- MOVIMIENTO CON PLATAFORMA ----------
        // --- MOVER AL JUGADOR CON LA PLATAFORMA ---
        if (currentPlatform != null)
        {
            characterController.Move(currentPlatform.DeltaMovement);
        }


        // Movimiento normal del jugador
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

    void CheckPlatform()
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        float rayDistance = 1.5f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance))
        {
            currentPlatform = hit.collider.GetComponent<MovingPlatform>();
        }
        else
        {
            currentPlatform = null;
        }
    }


    private void LateUpdate()
    {
        if (!characterController.isGrounded)
            currentPlatform = null;
    }


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

    public void EnableSpinAbility()
    {
        canSpin = true;
        Debug.Log("Habilidad de giro activada");
    }

    public void ApplyExternalForce(Vector3 force)
    {
        externalForce = force;
    }

    public bool IsSpinning()
    {
        return isSpinActive;
    }
}
