using UnityEngine;

public class DarkWave : HitAbility
{
    private float _currentRadius = 1;
    private bool _needScaleDown = false;

    protected override void OnUpdateAbility(float deltaTime)
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
                DestroyAbility();
            }
        }

        transform.localScale = new Vector3(_currentRadius, _currentRadius) * AbilityData.Scale.Get(Level);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}