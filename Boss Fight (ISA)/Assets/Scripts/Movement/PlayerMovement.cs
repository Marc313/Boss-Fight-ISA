using UnityEngine;

public class PlayerMovement : Movement
{
    public Transform EnemyLockOn;
    
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
        GameManager.OnStateChange += OnGameStateChange;
        combat.OnBlockingStart += SetRotationModeLocked;
        combat.OnBlockingEnd += SetRotationModeFree;
    }

    private void OnDisable()
    {
        GameManager.OnStateChange -= OnGameStateChange;
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
    }

    private void Move(float vert, float hor)
    {
        moveDirection = (playerCamera.transform.forward * vert + playerCamera.transform.right * hor).normalized;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        rb.position += movement;

        float inputMovementAngle = Vector3.Angle(transform.forward, moveDirection);
        anim.SetFloat("InputMovementAngle", inputMovementAngle);

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
        return Quaternion.LookRotation(moveDirection);
    }

    private Quaternion GetLockOnRotation()
    {
        Quaternion cameraRotation = playerCamera.targetLookRotation;
        cameraRotation.x = 0;
        cameraRotation.z = 0;
        return cameraRotation;
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

    private void OnGameStateChange(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.WON)
        {
            StopAllCoroutines();
            anim.SetTrigger("Dance");
        }
    }
}
