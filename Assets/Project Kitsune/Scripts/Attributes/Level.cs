using System;

public class Level : Attribute
{
    public event Action<float> OnExperienceChanged;
    public event Action<float> OnLevelUp;

    public bool CanGainExperience;

    private Experience _experience;

    public Level()
    {
        Minimum = 0;
        Maximum = Experience.MaximumList.Length - 1;
        Value = 1;

        CanGainExperience = true;

        _experience = new Experience((int)Value);
        _experience.Set(0);
        _experience.OnMaximum += LevelUp;
    }

    protected override bool ClampOnChange() => true;

    public void AddExperience(float value)
    {
        if (CanGainExperience == true)
        {
            float excess = value;
            while (excess > 0 && Value != Maximum)
            {
                excess = _experience.Add(excess);
            }

            OnExperienceChanged?.Invoke(_experience.Value);
        }
    }

    public void LevelUp()
    {
        Add(1);
        _experience.NextLevel((int)Value);
        OnLevelUp?.Invoke(Value);
    }

    public override float GetPercent()
    {
        return _experience.GetPercent();
    }

    public override string ToString()
    {
        return "Level: " + Value;
    }
}