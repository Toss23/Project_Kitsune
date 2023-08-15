using UnityEngine;

public class DarkWave : Ability
{
    private float _currentRadius = 1;
    private bool _needScaleDown = false;

    protected override void OnCollisionEnterWithEnemy(Unit caster, Unit target) 
    { 
    
    }

    protected override void OnCollisionStayWithEnemy(Unit caster, Unit target) 
    { 
    
    }

    protected override void OnCreateAbility(Unit caster) 
    { 
    
    }

    protected override void OnUpdateAbility(Unit caster, float deltaTime)
    {
        if (_needScaleDown == false)
        {
            _currentRadius += Time.deltaTime * Properties["WaveSpeed"];

            if (_currentRadius >= Properties["MaxRadius"])
            {
                _needScaleDown = true;
            }
        }
        else
        {
            _currentRadius -= Time.deltaTime * Properties["WaveSpeed"];

            if (_currentRadius <= 1)
            {
                Destroy();
            }
        }

        transform.localScale = new Vector3(_currentRadius, _currentRadius) * Info.Scale[Level];
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected override void OnDestroyAbility(Unit caster)
    {

    }
}