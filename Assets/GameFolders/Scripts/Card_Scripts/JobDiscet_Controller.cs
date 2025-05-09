using System;
using TMPro;
using UnityEngine;

public class JobDiscet_Controller : MonoBehaviour
{
    [SerializeField] public JobDiscet_Data _jobDiscetData;

    [SerializeField] private TMP_Text _displayName;

    public void InsertData(JobDiscet_Data _insertedData)
    {
        _jobDiscetData = _insertedData;
        _displayName.text = _jobDiscetData._JobName;
    }
}
