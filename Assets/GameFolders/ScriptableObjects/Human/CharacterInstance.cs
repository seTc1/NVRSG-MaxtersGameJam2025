using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CharacterInstance
{
    public HumanData Data { get; private set; }
    public float BurnoutTimer { get; private set; }
    public float IdleTimer { get; private set; }
    public bool IsWorking { get; set; }
    
    public Sprite _humanSprite { get; private set; }
    

    
    private readonly IEffectSpawner _effectSpawner;

    public CharacterInstance(HumanData data, IEffectSpawner effectSpawner)
    {
        Data = data;
        _effectSpawner = effectSpawner;
        BurnoutTimer = data.burnoutTimerMax + Random.Range(-10, 10);
        IdleTimer = data.idleTimerMax + Random.Range(-10, 10);
        _humanSprite = data._humanSprite;
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
                _effectSpawner.SpawnEffect(0);
                ResetIdleTimer();
            }
        }
        else
        {
            IdleTimer -= dt;
            if (IdleTimer < 0f)
            {
                IdleTimer = 0f;
                _effectSpawner.SpawnEffect(2);
            }
            
            
        }
    }

    public void AssignJob(JobDiscet_Data job)
    {
        _effectSpawner.SpawnEffect(1);
        IsWorking = true;

        // 1) Сумма абсолютных разниц по трём характеристикам
        float diffSum =
            Mathf.Abs(Data.responsibility - job.Responsibility)
            + Mathf.Abs(Data.communication   - job.Communication)
            + Mathf.Abs(Data.stressResistance - job.StressResistance);

        // 2) Нормировка (максимальная разница = 3 * 9)
        const float maxDiff = 27f;
        float similarity = 1f - diffSum / maxDiff;

        // 3) Мультипликатор от 0.5 до 2 (чем выше схожесть – тем больше время)
        float multiplier = Mathf.Lerp(0.5f, 2f, similarity);

        BurnoutTimer = Data.burnoutTimerMax * multiplier;
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