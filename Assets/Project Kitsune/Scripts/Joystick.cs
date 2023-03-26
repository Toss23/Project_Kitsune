using UnityEngine;

public class Joystick : MonoBehaviour
{
    public float Angle { get; private set; }
    public Direction Direction { get; private set; }

    [SerializeField] private Transform _stick;
    [SerializeField] private Canvas _canvas;

    private Vector3 _screenSize;

    private void Awake()
    {
        float width = Screen.width / _canvas.scaleFactor;
        float height = Screen.height / _canvas.scaleFactor;
        _screenSize = new Vector3(width, height);
    }

    private void Update()
    {
        Vector2 stickPosition = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Input.mousePosition - 0.5f * _screenSize;
            Vector2 joystickPosition = transform.localPosition;
            stickPosition = mousePosition - joystickPosition;
            stickPosition = Vector2.ClampMagnitude(stickPosition, 200);
        }

        _stick.localPosition = stickPosition;
    }
}