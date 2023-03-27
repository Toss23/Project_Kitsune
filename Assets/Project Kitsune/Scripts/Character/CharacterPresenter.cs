using UnityEngine;

[RequireComponent(typeof(CharacterView))]
public class CharacterPresenter : MonoBehaviour
{
    [SerializeField] private CharacterInfo _characterInfo;
    [SerializeField] private Joystick _joystick;

    private ICharacterView _characterView;
    private ICharacter _character;

    private void Awake()
    {
        _characterView = GetComponent<CharacterView>();
        _character = new Character();
        
        //_characterView.SpawnCharacter(_characterInfo.Prefab);

        Enable();
    }

    private void Enable()
    {
        Controlable controlable = _character.Controlable;
        _joystick.OnActive += (angle, deltaTime) => controlable.Move(angle, deltaTime);
        controlable.OnMove += (position) => _characterView.SetPosition(position);
    }
}