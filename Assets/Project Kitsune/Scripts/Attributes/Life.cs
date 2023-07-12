using System;

public class Life : Attribute
{
    public event Action<float> OnChanged;

    public float Regeneration { get; private set; }

    private float _timer = 0;
    private float _regenerationFrequercy = 0.1f;

    public Life(float maximum, float regeneration)
    {
        Value = maximum;
        Maximum = maximum;
        Minimum = 0;
        Regeneration = regeneration;
    }

    public void Regenerate(float deltaTime)
    {
        if (Regeneration != 0)
        {
            _timer += deltaTime;
            while (_timer >= _regenerationFrequercy)
            {
                _timer -= _regenerationFrequercy;
                Add(Regeneration * _regenerationFrequercy);
                OnChanged?.Invoke(Value);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Add(damage);
        OnChanged?.Invoke(Value);
    }
}