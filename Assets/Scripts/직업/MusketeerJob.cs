using UnityEngine;

[CreateAssetMenu(fileName = "MusketeerJob", menuName = "Jobs/MusketeerJob")]
public class MusketeerJob : Jobs
{
    private void OnEnable()
    {
        jobName = "Musketeer";

        baseHP = 100;
        baseAttackDamage = 50;
        baseAttackSpeed = 3;
        baseSpeed = 10;
        baseMana = 10;

        soulBonusMultiplier = 1f;
    }
}
