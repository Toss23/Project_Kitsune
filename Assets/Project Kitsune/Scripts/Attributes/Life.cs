using System;

public class Life : Attribute
{
    public event Action<float> OnChanged;

    public LifeRegeneration Regeneration { get; private set; }

    private float _regenerationDefault = 0;
    private float _timer = 0;
    private float _regenerationFrequercy = 0.1f;

    public Life(float maximum, float regeneration)
    {
        Value = maximum;
        Maximum = maximum;
        Minimum = 0;
        Regeneration = new LifeRegeneration(regeneration);
        SaveDefault();
    }

    protected override bool ClampOnChange() => true;

    protected override void SaveDefault()
    {
        base.SaveDefault();
        _regenerationDefault = Regeneration.Value;
    }

    public override void ResetToDefault()
    {
        base.ResetToDefault();
        Regeneration.Set(_regenerationDefault);
    }

    public void Regenerate(float deltaTime)
    {
        if (Regeneration.Value != 0)
        {
            _timer += deltaTime;
            while (_timer >= _regenerationFrequercy)
            {
                _timer -= _regenerationFrequercy;
                Add(Regeneration.Value * _regenerationFrequercy);
                OnChanged?.Invoke(Value);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Subtract(damage);
        OnChanged?.Invoke(Value);
    }

    public void AddMaximum(float value)
    {
        if (value > 0)
        {
            Maximum += value;
            if (Maximum < 0)
            {
                Maximum = 0;
            }
        }
    }

    public void SubtractMaximum(float value)
    {
        if (value > 0)
        {
            Maximum -= value;
            if (Maximum < 0)
            {
                Maximum = 0;
            }
        }
    }
}