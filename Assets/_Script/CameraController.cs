using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player; // �÷��̾� Transform
    [SerializeField] private float mouseSensitivity = 100f; // ���콺 ����
    [SerializeField] private float headHeight = 0.8f; // �Ӹ� ���� ������ (�÷��̾� �߽ɿ���)
    [SerializeField] private float minPitch = -80f; // �ּ� ���� ȸ�� ����
    [SerializeField] private float maxPitch = 80f; // �ִ� ���� ȸ�� ����
    private float currentPitch = 0f; 
    private float currentYaw = 0f; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transform.position = player.position + Vector3.up * headHeight;
        transform.SetParent(player); 
    }

    // LateUpdate�� ��� Update ȣ�� �� ȣ��Ǹ�, ī�޶� ȸ���� ����
    void LateUpdate()
    {
        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ȸ�� ���� ������Ʈ
        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // ī�޶� ���� ȸ�� (X��)
        transform.localRotation = Quaternion.Euler(currentPitch, 0, 0);

        // �÷��̾� �¿� ȸ�� (Y��)
        player.rotation = Quaternion.Euler(0, currentYaw, 0);
    }
}