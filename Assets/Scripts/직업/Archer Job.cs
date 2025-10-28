using UnityEngine;

[CreateAssetMenu(fileName = "Archer", menuName = "Jobs/Archer")]
public class ArcherJob : Jobs
{
    private void OnEnable()
    {
        jobName = "Archer";

        baseHP = 70;
        baseAttackDamage = 25;
        baseAttackSpeed = 1;
        baseSpeed = 20;
        baseMana = 40;

        soulBonusMultiplier = 1f;
    }
}
