using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class JobSelectUI : MonoBehaviour
{
    public Button musketeerButton;
    public Button archerButton;
    public Button mageButton;

    private PlayerJobData localPlayerJob;

    void Start()
    {
        musketeerButton.onClick.AddListener(() => SelectJob("Musketeer"));
        archerButton.onClick.AddListener(() => SelectJob("Archer"));
        mageButton.onClick.AddListener(() => SelectJob("Mage"));
    }

    void SelectJob(string job)
    {
        if (localPlayerJob == null)
        {
            var playerObjs = FindObjectsByType<PlayerJobData>(FindObjectsSortMode.None);
            foreach (var obj in playerObjs)
            {
                if (obj.IsOwner)
                {
                    localPlayerJob = obj;
                    break;
                }
            }
        }

        if (localPlayerJob != null)
        {
            localPlayerJob.SetJobServerRpc(job);
            Debug.Log($"직업 선택: {job}");
        }
        else
        {
            Debug.LogWarning("로컬 플레이어를 찾을 수 없습니다.");
        }
    }
}
