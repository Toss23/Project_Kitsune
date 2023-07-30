using UnityEngine;
using UnityEngine.U2D.Animation;

public class SkinАccessory : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset _spriteLibraryAsset;
    [SerializeField] private string _selectedCategory;

    public SpriteLibraryAsset SpriteLibraryAsset => _spriteLibraryAsset;
    public string SelectedCategory => _selectedCategory;
}
