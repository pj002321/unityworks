using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // 플레이어 Transform
    [SerializeField] private float mouseSensitivity = 100f; // 마우스 감도
    [SerializeField] private float headHeight = 0.8f; // 머리 높이 오프셋 (플레이어 중심에서)
    [SerializeField] private float minPitch = -80f; // 최소 상하 회전 각도
    [SerializeField] private float maxPitch = 80f; // 최대 상하 회전 각도
    private float currentPitch = 0f; 
    private float currentYaw = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = player.position + Vector3.up * headHeight;
        transform.SetParent(player); 
    }

    // LateUpdate는 모든 Update 호출 후 호출되며, 카메라 회전에 적합
    void LateUpdate()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 회전 각도 업데이트
        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // 카메라 상하 회전 (X축)
        transform.localRotation = Quaternion.Euler(currentPitch, 0, 0);

        // 플레이어 좌우 회전 (Y축)
        player.rotation = Quaternion.Euler(0, currentYaw, 0);
    }
}