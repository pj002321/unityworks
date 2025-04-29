using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float jumpForce = 5f; // 점프 힘
    [SerializeField] private float groundCheckDistance = 0.4f; // 바닥 체크 거리
    [SerializeField] private LayerMask groundLayer; // 바닥 레이어
    private bool isGrounded; 
    private float moveX; 
    private float moveZ; 
    private bool jumpPressed; 
    private Camera mainCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main; 
    }

    // Update는 매 프레임마다 호출됩니다
    void Update()
    {
        // 이동 입력 받기
        moveX = Input.GetAxisRaw("Horizontal"); 
        moveZ = Input.GetAxisRaw("Vertical");  

        // 점프 입력 체크
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    // FixedUpdate는 고정된 시간 간격으로 호출되며, 물리 연산에 적합합니다
    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveZ + right * moveX).normalized * moveSpeed;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // 점프 입력 초기화
        jumpPressed = false;
    }
}