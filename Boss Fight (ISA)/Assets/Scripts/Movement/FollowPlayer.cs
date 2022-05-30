using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public enum CameraMode { FREE, LOCK};

    public CameraMode mode;
    public float camSmoothing;
    public float sensitivityX;
    public Vector3 Offset;

    private float rotationX;
    private float rotationY;
    private Transform Player;
    private Transform LockTarget;
    private PlayerCombat playerCombat;
    public Quaternion targetLookRotation { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player = FindObjectOfType<PlayerMovement>().transform;
        LockTarget = FindObjectOfType<EnemyAIFSM>()?.transform;
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    private void OnEnable()
    {
        playerCombat.OnBlockingStart += SwitchToBlockingCamera;
        playerCombat.OnBlockingEnd += SwitchToFreeCamera;
    }
    private void OnDisable()
    {
        playerCombat.OnBlockingStart -= SwitchToBlockingCamera;
        playerCombat.OnBlockingEnd -= SwitchToFreeCamera;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();

        if (mode == CameraMode.FREE)
        {
            // Rotate the camera along with the mouse
            RotateCameraAlongMouse();
        } else
        {
            RotateToLockedTarget();
        }
    }

    public void SwitchCameraMode()
    {
        switch (mode)
        {
            case CameraMode.FREE:
                mode = CameraMode.LOCK;
                break;
            case CameraMode.LOCK:
                mode = CameraMode.FREE;
                break;
            default:
                break;
        }
    }
    public void SwitchCameraModeTo(CameraMode cameraMode)
    {
        mode = cameraMode;
    }

    public void SwitchToBlockingCamera()
    {
        SwitchCameraModeTo(CameraMode.LOCK);
    }
    public void SwitchToFreeCamera()
    {
        SwitchCameraModeTo(CameraMode.FREE);
    }

    private void MoveToPlayer()
    {
        // Move the camera along with the player
        transform.position = Player.position + Offset;
    }

    public void RotateCameraAlongMouse()
    {
        // Input on the mouse X axis
        float mouseX = Input.GetAxis("Mouse X");
        //float newRotationY = transform.rotation.y + mouseX * sensitivityX * Time.deltaTime;
        rotationY += mouseX * sensitivityX * Time.deltaTime;

        //Quaternion newRotation = Quaternion.Euler(transform.rotation.x, newRotationY, transform.rotation.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, camSmoothing);

        transform.localRotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }

    public void RotateToLockedTarget()
    {
        Vector3 lookDirection = LockTarget.position - Player.position;
        targetLookRotation = Quaternion.LookRotation(lookDirection);
        transform.localRotation = targetLookRotation;

        // Thanks to Sebastian Graves for the addition
        // lookDirection = LockTarget.position - cameraPi.position;
    }
}
