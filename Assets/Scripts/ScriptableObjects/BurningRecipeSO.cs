using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public ScriptableKitchenObjects input;
    public ScriptableKitchenObjects output;
    public int maxBurningProgress;

}
