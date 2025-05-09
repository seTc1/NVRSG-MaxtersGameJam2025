using UnityEngine;

[System.Serializable] 
public class JobRecipe 
{
    public int[] combineID;
    public string jobName;
    public JobDiscet_Data _jobData;
}

[CreateAssetMenu(fileName = "JobCombine (1)", menuName = "CardsData/JobCombine_Data")]
public class JobCombine_Data : ScriptableObject
{
    public GameObject _jobDiscetPrefab;
    public JobRecipe[] _JobRecipes;
}
