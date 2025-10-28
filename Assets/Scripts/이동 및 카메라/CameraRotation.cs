using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class CameraRotation : NetworkBehaviour
{
    [Header("카메라 회전 설정")]
    public float mouseSensitivity = 100f;

    private Transform playerBody;
    private float xRotation = 0f;
    private bool isActive = false; // 현재 회전 기능 활성 상태

    void Awake()
    {
        // 씬 변경 감지 이벤트 등록
        NetworkManager.Singleton.SceneManager.OnSceneEvent += HandleSceneEvent;
    }

    void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.SceneManager.OnSceneEvent -= HandleSceneEvent;
    }

    void Start()
    {
        playerBody = transform.parent;

        if (!IsOwner)
        {
            GetComponent<Camera>().enabled = false;
            enabled = false;
            return;
        }

        // 초기 씬이 Robby면 비활성화
        CheckScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (!IsOwner || !isActive) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void HandleSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete &&
            sceneEvent.ClientId == NetworkManager.Singleton.LocalClientId)
        {
            // 씬이 완전히 로드된 뒤 확인
            CheckScene(sceneEvent.SceneName);
        }
    }

    private void CheckScene(string sceneName)
    {
        if (sceneName == "Lobby") // 로비 씬 이름에 맞게 수정
        {
            isActive = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else // 인게임 씬이면 자동 활성화
        {
            isActive = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
