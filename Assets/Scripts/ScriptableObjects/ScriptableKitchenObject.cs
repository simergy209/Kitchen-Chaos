using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableKitchenObjects : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string objectName;

    public GameObject GetPrefab()
    {
        return prefab;
    }
}
