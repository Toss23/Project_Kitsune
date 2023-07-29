using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinАccessory : MonoBehaviour
{
    [SerializeField] private string _selectedSkin;

    public string SelectedSkin => _selectedSkin;
}
