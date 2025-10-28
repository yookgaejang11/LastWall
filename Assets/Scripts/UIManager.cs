using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : NetworkBehaviour
{
    [Header("UI References")]
    public Button gameStartButton;
    public Text playerCountText;

    private const int requiredPlayers = 1; // 4명일 때 시작 가능
    private List<ulong> connectedPlayers = new List<ulong>();

    private void Start()
    {
        gameStartButton.onClick.AddListener(OnGameStartClicked);
        gameStartButton.interactable = false; // 처음엔 비활성화

        // 호스트/클라이언트 구분
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }

        UpdatePlayerCountUI();
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (!connectedPlayers.Contains(clientId))
            connectedPlayers.Add(clientId);

        UpdatePlayerCountUI();
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (connectedPlayers.Contains(clientId))
            connectedPlayers.Remove(clientId);

        UpdatePlayerCountUI();
    }

    private void UpdatePlayerCountUI()
    {
        int playerCount = connectedPlayers.Count;
        playerCountText.text = $"Players: {playerCount}/{requiredPlayers}";

        // 호스트만 버튼 활성화 (4명일 때)
        if (IsServer)
        {
            gameStartButton.interactable = (playerCount == requiredPlayers);
        }
    }

    private void OnGameStartClicked()
    {
        if (IsServer && connectedPlayers.Count == requiredPlayers)
        {
            // 모든 클라이언트에게 씬 전환
            NetworkManager.Singleton.SceneManager.LoadScene("Castle", LoadSceneMode.Single);
        }
    }
}
