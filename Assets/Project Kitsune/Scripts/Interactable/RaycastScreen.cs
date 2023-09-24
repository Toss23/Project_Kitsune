using UnityEngine;

public class RaycastScreen : Clickable2D
{
    [Header("Raycast")]
    [SerializeField] private Camera _raycastCamera;
    [SerializeField] private LayerMask _layer;

    protected override void OnSingleClick()
    {
        Clickable3D clickable = Cast();
        if (clickable != null)
            clickable.OnClick();
    }

    private Clickable3D Cast()
    {
        Ray ray = _raycastCamera.ScreenPointToRay(TouchPositionUnfixed);
        if (Physics.Raycast(ray, out RaycastHit hit, _layer))
        {
            return hit.transform.GetComponent<Clickable3D>();
        }
        return null;
    }

    protected override void OnTouchDown() { }

    protected override void OnTouchUp() { }

    protected override void OnUpdate(float deltaTime) { }
}