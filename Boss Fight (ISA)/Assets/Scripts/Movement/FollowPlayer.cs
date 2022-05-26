using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float camSmoothing;
    public float sensitivityX;
    public Vector3 Offset;

    private float rotationX;
    private float rotationY;
    private Transform Player;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();

        // Rotate the camera along with the mouse
        RotateCamera();
    }

    private void MoveToPlayer()
    {
        // Move the camera along with the player
        transform.position = Player.position + Offset;
    }

    public void RotateCamera()
    {
        // Input on the mouse X axis
        float mouseX = Input.GetAxis("Mouse X");
        //float newRotationY = transform.rotation.y + mouseX * sensitivityX * Time.deltaTime;
        rotationY += mouseX * sensitivityX * Time.deltaTime;

        //Quaternion newRotation = Quaternion.Euler(transform.rotation.x, newRotationY, transform.rotation.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, camSmoothing);

        transform.localRotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }
}
