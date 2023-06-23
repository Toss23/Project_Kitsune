using UnityEngine;

public class DarkWave : Ability
{
    private float _currentRadius = 1;
    private bool _needScaleDown = false;

    protected override void OnCollisionEnterWithEnemy(IUnit enemy) { }

    protected override void OnCollisionStayWithEnemy(IUnit enemy) { }

    protected override void OnCreate() { }

    protected override void OnUpdate(float deltaTime)
    {
        if (_needScaleDown == false)
        {
            _currentRadius += Time.deltaTime * Properties["WaveSpeed"];

            if (_currentRadius >= Properties["MaxRadius"])
                _needScaleDown = true;
        }
        else
        {
            _currentRadius -= Time.deltaTime * Properties["WaveSpeed"];

            if (_currentRadius <= 1)
            {
                Destroy();
            }
        }

        transform.localScale = new Vector3(_currentRadius, _currentRadius) * Info.Radius[Level];
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}