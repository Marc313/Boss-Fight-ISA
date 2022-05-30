using UnityEngine;

public class PlayerMovement : Movement
{
    private Rigidbody rb;
    private FollowPlayer playerCamera;
    private PlayerCombat combat;

    public bool isDodging { get; private set; }

    private Vector3 moveDirection;

    private enum RotationMode { FREE, LOCKON, SWITCHING};
    private RotationMode rotateMode;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<FollowPlayer>();
        combat = GetComponent<PlayerCombat>();
    }

    protected override void Update()
    {
        base.Update();
        if (!isInteracting)
        {
            HandleMoveInput();
        }
    }

    private void OnEnable()
    {
        combat.OnBlockingStart += SetRotationModeLocked;
        combat.OnBlockingEnd += SetRotationModeFree;
    }

    private void OnDisable()
    {
        combat.OnBlockingStart -= SetRotationModeLocked;
        combat.OnBlockingEnd -= SetRotationModeFree;
    }

    private void HandleMoveInput()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");

        vertInput = ClampMoveInput(vertInput);
        horInput = ClampMoveInput(horInput);

        UpdateAnimationValues(vertInput, horInput);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DodgeStart();
        }
        else
        {
            if (vertInput != 0 || horInput != 0)
            {
                Move(vertInput, horInput);
            }
        }
        //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void Move(float vert, float hor)
    {
        //Vector3 inputDirection = Vector3.forward * vert + Vector3.right * hor;
        moveDirection = (playerCamera.transform.forward * vert + playerCamera.transform.right * hor).normalized;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        rb.position += movement;

        float inputMovementAngle = Vector3.Angle(transform.forward, moveDirection);
        anim.SetFloat("InputMovementAngle", inputMovementAngle);
        Debug.Log("Horizontal: " + hor);

        // Rotation
        if (rotateMode == RotationMode.FREE)
        {
            transform.rotation = GetFreeRotation();
        } else if (rotateMode == RotationMode.LOCKON)
        {
            transform.rotation = GetLockOnRotation();
        }
    }

    private void DodgeStart()
    {
        isInteracting = true;
        isDodging = true;
        anim.SetBool("IsDodging", isDodging);
    }

    public void OnDodgeOver()
    {
        isInteracting = false;
        isDodging = false;
        anim.SetBool("IsDodging", isDodging);
    }

    private void SetRotationModeLocked()
    {
        rotateMode = RotationMode.SWITCHING;
        Quaternion oldRotation = GetFreeRotation();
        Quaternion targetRotation = GetLockOnRotation();
        StartCoroutine(this.RotateTowardsSlowly(oldRotation, targetRotation, () => rotateMode = RotationMode.LOCKON));
    }

    private void SetRotationModeFree()
    {
        rotateMode = RotationMode.SWITCHING;
        Quaternion oldRotation = GetLockOnRotation();
        Quaternion targetRotation = GetFreeRotation();
        StartCoroutine(this.RotateTowardsSlowly(oldRotation, targetRotation, () => rotateMode = RotationMode.FREE));
    }

    private Quaternion GetFreeRotation()
    {
        return transform.rotation = Quaternion.LookRotation(moveDirection);
    }

    private Quaternion GetLockOnRotation()
    {
        return transform.rotation = playerCamera.targetLookRotation;
    }

    private float ClampMoveInput(float input)
    {
        if (input > 0 && input <= 0.55f) input = 0.5f;
        else if (input < 0 && input >= -0.55f) input = 0.5f;
        else if (input > 0.55f) input = 1;
        else if (input < -0.55f) input = -1;

        return input;
    }

    private void UpdateAnimationValues(float vert, float hor)
    {
        anim.SetFloat("Vertical", vert, 0.1f, Time.deltaTime);
        anim.SetFloat("Horizontal", Mathf.Abs(hor), 0.1f, Time.deltaTime);
    }
}
