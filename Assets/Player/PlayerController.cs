using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    public bool canCameraMove = true;
    [Range(0, 30)] public float mySpeed = 15f;
    [Range(0, 5)] public float vSpeed = 2f;
    [Range(0, 5)] public float hSpeed = 2f;

    [SerializeField] private Camera myCamera;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private bool enableBob = true;
    [SerializeField, Range(0, 10f)] private float bobAmplitude = 5f;
    [SerializeField, Range(0, 30f)] private float bobFrequency = 10f;

    public bool lightActive;
    public bool lightFixed;
    public Vector3 lightOrigin;
    public Vector3 lightDirection;
    public Vector3 cameraOrigin;
    public Vector3 cameraDirection;

    private Vector3 startPos;

    private CharacterController characterController;
    
    private LightController lightController;
    private InteractController interactController;

    private float yaw;
    private float pitch;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        lightController = GetComponent<LightController>();
        interactController = GetComponent<InteractController>();

        lightController.Initialize(this);
        interactController.Initialize(this);

        startPos = myCamera.transform.localPosition;

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        Vector3 input = cameraHolder.transform.right * Input.GetAxisRaw("Horizontal") +
                        cameraHolder.transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 moveDir = transform.TransformDirection(input.normalized);
        
        if (canCameraMove)
        {
            yaw += hSpeed * Input.GetAxis("Mouse X");
            pitch -= vSpeed * Input.GetAxis("Mouse Y");
            cameraHolder.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
        else
        {
            yaw = cameraHolder.transform.eulerAngles.y;
            pitch = cameraHolder.transform.eulerAngles.x;
        }

        if (canMove)
        {
            enableBob = input != Vector3.zero;
            CheckMotion();
            ResetMotion();
            characterController.Move(moveDir * mySpeed * Time.deltaTime);
            characterController.Move(Physics.gravity);
        }

        lightController.ManagedUpdate();
        interactController.ManagedUpdate();
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * bobFrequency) * (bobAmplitude / 1000);
        pos.x = 0;
        return pos;
    }

    private void PlayerMotion(Vector3 motion)
    {
        myCamera.transform.localPosition += motion;
    }

    private void CheckMotion()
    {
        if (!characterController.isGrounded) return;
        if (!enableBob) return;

        GameManager.SoundManager.PlaySE("footstep");

        PlayerMotion(FootStepMotion());
    }

    private void ResetMotion()
    {
        if ((myCamera.transform.localPosition - startPos).magnitude < 0.01f) return;
        myCamera.transform.localPosition =
            Vector3.Lerp(myCamera.transform.localPosition, startPos, Time.deltaTime * 10);
    }
}