using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public ScriptableKitchenObjects input;
    public ScriptableKitchenObjects output;
    public int maxFryingProgress;

}
