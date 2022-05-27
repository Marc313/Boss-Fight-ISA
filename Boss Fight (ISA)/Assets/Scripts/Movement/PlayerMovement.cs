using UnityEngine;

public class PlayerMovement : Movement
{
    private Rigidbody rb;
    private FollowPlayer playerCamera;

    public bool isDodging { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<FollowPlayer>();
    }

    protected override void Update()
    {
        base.Update();
        if (!isInteracting)
        {
            HandleMoveInput();
        }
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
        Vector3 moveDirection = (playerCamera.transform.forward * vert + playerCamera.transform.right * hor).normalized;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        rb.position += movement;

        if(playerCamera.mode == FollowPlayer.CameraMode.FREE)
        {
            transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0);
        } else
        {
            transform.rotation = playerCamera.targetLookRotation;
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
