using System;

public class EnemySpawner
{
    public event Action<IUnitPresenter> SpawnEnemy;

    private EnemySpawnerInfo _info;
    private float _time = 0;
    private float[] _timers;
    private bool _freeze;

    public EnemySpawner(EnemySpawnerInfo info)
    {
        _info = info;
        _timers = new float[info.SpawnRules.Length];
        _freeze = false;
    }

    public void Freeze(bool state)
    {
        _freeze = state;
    }

    public void Update(float deltaTime)
    {
        if (_freeze == false)
        {
            _time += deltaTime;

            SpawnRule[] rules = _info.SpawnRules;
            for (int i = 0; i < rules.Length; i++)
            {
                if (rules[i].Enemy != null)
                {
                    if (_time >= rules[i].StartTime & _time <= rules[i].EndTime)
                    {
                        _timers[i] += deltaTime;

                        float delta = (_time - rules[i].StartTime) / (rules[i].EndTime - rules[i].StartTime);
                        float loopTime = rules[i].LoopTimeStart + (rules[i].LoopTimeEnd - rules[i].LoopTimeStart) * delta;

                        while (_timers[i] >= loopTime)
                        {
                            _timers[i] -= loopTime;

                            int count = (int)(rules[i].CountStart + (rules[i].CountEnd - rules[i].CountStart) * delta);
                            for (int j = 0; j < count; j++)
                            {
                                SpawnEnemy?.Invoke(rules[i].Enemy);
                            }
                        }
                    }
                }
            }
        }
    }
}