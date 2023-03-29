using System;

public class Life : Attribute
{
    public event Action<float> OnLifeChange;

    public float Regeneration;

    private float _timer = 0;

    public Life(float value, float maximum)
    {
        Set(value);
        Maximum = maximum;
        Minimum = 0;
    }

    public void Regenerate(float deltaTime)
    {
        _timer += deltaTime;
        while (_timer >= 1)
        {
            _timer--;
            Add(Regeneration);
            OnLifeChange?.Invoke(Value);
        }
    }

    public void TakeDamage(float damage)
    {
        Add(damage);
        OnLifeChange?.Invoke(Value);
    }
}