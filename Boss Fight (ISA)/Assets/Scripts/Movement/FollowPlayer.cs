using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public enum CameraMode { FREE, LOCK};

    public CameraMode mode;
    public float camSmoothing;
    public float sensitivityX;
    public Vector3 Offset;
    public Transform EnemyLockOn;

    private float rotationX;
    private float rotationY;
    private Transform Player;
    private PlayerCombat playerCombat;
    public Quaternion targetLookRotation { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player = FindObjectOfType<PlayerMovement>().transform;
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
        rotationY += mouseX * sensitivityX * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }

    public void RotateToLockedTarget()
    {
        Vector3 lookDirection = EnemyLockOn.position - Player.position;
        targetLookRotation = Quaternion.LookRotation(lookDirection);
        //targetLookRotation = Quaternion.Euler(Mathf.Clamp(targetLookRotation.eulerAngles.x, -20, 20), targetLookRotation.y, targetLookRotation.z);

        transform.localRotation = targetLookRotation;
    }
}
