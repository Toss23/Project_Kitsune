using UnityEngine;

[RequireComponent(typeof(KillCounterView))]
public class KillCounterPresenter : MonoBehaviour, IKillCounterPresenter
{
    private KillCounter _killCounter;
    private IKillCounterView _killCounterView;

    public KillCounter KillCounter => _killCounter;

    public void Init()
    {
        _killCounter = new KillCounter();
        _killCounterView = GetComponent<KillCounterView>();

        Enable();
    }

    public void Enable()
    {
        _killCounter.OnChange += _killCounterView.SetCount;
    }

    public void Disable()
    {
        _killCounter.OnChange -= _killCounterView.SetCount;
    }
}
