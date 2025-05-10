using System;
using Random = UnityEngine.Random;

public class CharacterInstance
{
    public HumanData Data { get; private set; }
    public float BurnoutTimer { get; private set; }
    public float IdleTimer { get; private set; }
    public bool IsWorking { get; set; }

    public CharacterInstance(HumanData data)
    {
        Data = data;
        BurnoutTimer = data.burnoutTimerMax + Random.Range(-10, 10);
        IdleTimer = data.idleTimerMax + Random.Range(-10, 10);
        
        IsWorking = false;
    }

    public void Tick(float dt)
    {
        if (IsWorking)
        {
            BurnoutTimer -= dt;
            if (BurnoutTimer <= 0f)
            {
                BurnoutTimer = 0f;
                IsWorking = false;
                ResetIdleTimer();
            }
        }
        else
        {
            IdleTimer -= dt;
            if (IdleTimer < 0f)
                IdleTimer = 0f;
        }
    }

    public void AssignJob()
    {
        IsWorking = true;
        BurnoutTimer = Data.burnoutTimerMax;
    }
    public void AssignIdle()
    {
        IsWorking = false;
        ResetIdleTimer();
    }

    public void ResetIdleTimer()
    {
        IdleTimer = Data.idleTimerMax;
    }

    public float Priority => IdleTimer;
}