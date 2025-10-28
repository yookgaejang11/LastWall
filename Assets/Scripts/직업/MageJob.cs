using UnityEngine;

[CreateAssetMenu(fileName = "MageJob", menuName = "Jobs/MageJob")]
public class MageJob : Jobs
{
    private void OnEnable()
    {
        jobName = "Mage";

        baseHP = 50;
        baseAttackDamage = 30;
        baseAttackSpeed = 5;
        baseSpeed = 10;
        baseMana = 100;

        soulBonusMultiplier = 1f;
    }
}
