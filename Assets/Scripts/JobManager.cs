using UnityEngine;

public class JobManager : MonoBehaviour
{
    public static JobManager Instance;
    public Jobs[] allJobs;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Jobs GetJobByName(string name)
    {
        foreach (var job in allJobs)
        {
            if (job.jobName == name)
                return job;
        }
        Debug.LogWarning($"Job '{name}' not found!");
        return null;
    }
}
