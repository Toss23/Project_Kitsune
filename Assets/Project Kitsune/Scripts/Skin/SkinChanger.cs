using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[ExecuteAlways]
public class SkinChanger : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset _spriteLibraryAsset;
    [SerializeField] private string _selectedCategory;
    [SerializeField] private SpriteResolver[] _spriteResolvers;

    public SpriteLibraryAsset SpriteLibraryAsset => _spriteLibraryAsset;

    private void Update()
    {
        if (_spriteLibraryAsset != null & _spriteResolvers != null)
        {
            List<string> categories = new List<string>();
            categories.AddRange(_spriteLibraryAsset.GetCategoryNames());
            if (categories.Contains(_selectedCategory))
            {
                foreach (SpriteResolver spriteResolver in _spriteResolvers)
                {
                    if (spriteResolver != null)
                    {
                        spriteResolver.SetCategoryAndLabel(_selectedCategory, spriteResolver.GetLabel());
                    }
                }
            }
        }
    }
}