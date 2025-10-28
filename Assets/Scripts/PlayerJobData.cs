using Unity.Netcode;
using Unity.Collections;
using UnityEngine;

public class PlayerJobData : NetworkBehaviour
{
    public NetworkVariable<FixedString64Bytes> jobName = new("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public Jobs currentJob;

    public override void OnNetworkSpawn()
    {
        jobName.OnValueChanged += OnJobChanged;

        if (!IsServer) return;
        // 초기 직업 설정 (원하면 기본 직업 지정 가능)
        jobName.Value = "None";
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetJobServerRpc(string newJob)
    {
        jobName.Value = newJob;
    }

    private void OnJobChanged(FixedString64Bytes oldValue, FixedString64Bytes newValue)
    {
        currentJob = JobManager.Instance.GetJobByName(newValue.ToString());
        Debug.Log($"[{OwnerClientId}] 직업 변경됨 → {newValue}");

        var stats = GetComponent<PlayerStats>();
        if (stats != null && currentJob != null)
            stats.ApplyJobStats(currentJob);
    }
}
