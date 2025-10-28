using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    public NetworkVariable<int> HP = new();
    public NetworkVariable<int> Attack = new();
    public NetworkVariable<int> Speed = new();
    public NetworkVariable<int> Mana = new();

    public void ApplyJobStats(Jobs job)
    {
        if (!IsServer) return; // 서버에서만 스탯 초기화
        HP.Value = job.baseHP;
        Attack.Value = job.baseAttackDamage;
        Speed.Value = job.baseSpeed;
        Mana.Value = job.baseMana;

        Debug.Log($"[{OwnerClientId}] {job.jobName} 스탯 적용 완료! HP={HP.Value}, ATK={Attack.Value}");
    }
}
