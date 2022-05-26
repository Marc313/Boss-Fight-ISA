using UnityEngine;

public class PlayerMovement : Movement
{
    private Rigidbody rb;
    private Camera cam;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    protected override void Update()
    {
        base.Update();
        if (!isInteracting)
        {
            HandleMoveInput();

            if (GameManager.state != GameManager.GameState.FIGHT)
                isInteracting = true;
        }
    }

    private void HandleMoveInput()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");

        vertInput = ClampMoveInput(vertInput);
        horInput = ClampMoveInput(horInput);

        UpdateAnimationValues(vertInput, horInput);

        if (vertInput != 0 || horInput != 0)
            Move(vertInput, horInput);

        //rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void Move(float vert, float hor)
    {
        Vector3 moveDirection = (cam.transform.forward * vert + cam.transform.right * hor).normalized;
        Vector3 movement = moveDirection * currentMoveSpeed * Time.deltaTime;

        rb.position += movement;

        transform.rotation = Quaternion.Euler(0, Quaternion.LookRotation(moveDirection).eulerAngles.y, 0);
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
