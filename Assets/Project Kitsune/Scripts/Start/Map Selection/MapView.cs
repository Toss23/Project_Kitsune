using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private StartContext _startContext;

    private List<MapElementView> _elements;

    private void Awake()
    {
        _elements = new List<MapElementView>();
        _elements.AddRange(GetComponentsInChildren<MapElementView>());

        if (_startContext != null)
        {
            foreach (MapElementView element in _elements)
            {
                element.SetContext(_startContext);
            }
        }
    }
}