using System;

public abstract class Attribute
{
    public event Action OnMinimum;
    public event Action OnMaximum;

    public float Value { get; protected set; }
    public float Minimum { get; protected set; }
    public float Maximum { get; protected set; }

    public virtual void Set(float value)
    {
        Value = value;
        Normalize();
    }

    public virtual float Add(float value)
    {
        Value += value;
        return Normalize();
    }

    public virtual float GetPercent()
    {
        return Value / Maximum * 100;
    }

    public override string ToString()
    {
        return Value + " / " + Maximum;
    }

    private float Normalize()
    {
        float excess = 0;

        if (Value <= Minimum)
        {
            Value = Minimum;
            OnMinimum?.Invoke();
        }

        if (Value >= Maximum)
        {
            excess = Value - Maximum;
            Value = Maximum;
            OnMaximum?.Invoke();
        }

        return excess;
    }
}