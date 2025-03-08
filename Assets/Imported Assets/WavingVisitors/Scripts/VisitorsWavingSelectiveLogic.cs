using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsWavingSelectiveLogic : MonoBehaviour
{
    [SerializeField] GameEvent onVisitorsWaveInstance;
    [SerializeField] GameEvent onCompleteVisitorsWave;

    public void VisitorsWaveing()
    {
        onVisitorsWaveInstance.Raise();
    }

    public void FinishVisitorsWave()
    {
        onCompleteVisitorsWave.Raise();
    }
}
