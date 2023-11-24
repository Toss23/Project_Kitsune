using System;

public abstract class Attribute
{
    public event Action OnMinimum;
    public event Action OnMaximum;
    public event Action<float> OnChanged;
    public event Action<float> OnMultiplierChanged;

    private float _value;

    public float Value 
    {
        get 
        {
            float value = _value;
            if (ClampOnChange() == false)
            {
                value = Clamp()[0];
            }
            return value * Multiplier;
        }
        protected set
        {
            _value = value;
        }
    }

    public float Minimum { get; protected set; }
    public float Maximum { get; protected set; }
    public float Multiplier { get; protected set; } = 1;

    private float _valueDefault = 0;
    private float _minimumDefault = 0;
    private float _maximumDefault = 0;

    protected virtual void SaveDefault()
    {
        _valueDefault = _value;
        _minimumDefault = Minimum;
        _maximumDefault = Maximum;
    }

    public virtual void ResetToDefault()
    {
        _value = _valueDefault;
        Minimum = _minimumDefault;
        Maximum = _maximumDefault;
        Multiplier = 1;
    }

    public virtual float Set(float value)
    {
        _value = value;
        float excess = Clamp()[1];
        if (ClampOnChange())
        {
            ApplyClamp();
        }
        OnChanged?.Invoke(Value);
        return excess;
    }

    public virtual float Add(float value)
    {
        if (value > 0)
        {
            _value += value;
            float excess = Clamp()[1];
            if (ClampOnChange())
            {
                ApplyClamp();
            }
            return excess;
        }
        OnChanged?.Invoke(Value);
        return 0;
    }

    public virtual float Subtract(float value)
    {
        if (value > 0)
        {
            _value -= value;
            float excess = Clamp()[1];
            if (ClampOnChange())
            {
                ApplyClamp();
            }
            return excess;
        }
        OnChanged?.Invoke(Value);
        return 0;
    }

    public void Multiply(float value)
    {
        Multiplier *= value;
        OnMultiplierChanged?.Invoke(value);
    }

    public void Divide(float value)
    {
        Multiplier /= value;
        OnMultiplierChanged?.Invoke(value);
    }

    protected abstract bool ClampOnChange();

    public virtual float GetPercent()
    {
        return _value / Maximum * 100;
    }

    public override string ToString()
    {
        return Math.Floor(_value) + "";
    }

    private float[] ApplyClamp()
    {
        float[] result = Clamp();
        _value = result[0];

        if (_value == Maximum)
        {
            OnMaximum?.Invoke();
        }

        if (_value == Minimum)
        {
            OnMinimum?.Invoke();
        }

        return new float[] { _value, result[1] };
    }

    private float[] Clamp()
    {
        float value = _value;
        float excess = 0;

        if (value <= Minimum)
        {
            excess = Math.Abs(_value - Minimum);
            value = Minimum;
        }

        if (value >= Maximum)
        {
            excess = Math.Abs(_value - Maximum);
            value = Maximum;
        }

        return new float[] { value, excess };
    }
}