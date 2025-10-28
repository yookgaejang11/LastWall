using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;   // 이동 속도
    public float jumpForce = 5f;   // 점프 힘
    private Rigidbody rb;          // 리지드바디 컴포넌트
    private bool isGrounded;       // 바닥에 닿아 있는지 판별

    void Start()
    {
        rb = GetComponent<Rigidbody>();   // 리지드바디 가져오기
        if (rb == null)
        {
            Debug.LogError(" Rigidbody가 없습니다! PlayerPrefab에 Rigidbody를 추가하세요.");

        }
    }
    void Update()
    {
        // 내 캐릭터가 아니면 입력을 받지 않음
        if (!IsOwner || !IsSpawned) return;

        // 이동 입력 처리 (WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World); // 캐릭터 이동

        // 점프 (바닥에 있을 때만 가능)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿았는지 체크 (태그가 "Ground"일 때)
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
