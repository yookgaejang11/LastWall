using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;
    public Vector3 offset;

    public override void OnNetworkSpawn()
    {
        // 자신의 클라이언트(Authority)일 경우에만 카메라 활성화
        if (IsOwner)
        {
            cameraHolder.SetActive(true);
        }
    }

    void Update()
    {
        // 오직 로컬 플레이어(Owner)만 카메라 위치를 업데이트
        if (!IsOwner) return;

        if (SceneManager.GetActiveScene().name == "Castle")
        {
            cameraHolder.transform.position = transform.position + offset;
        }
    }
}
