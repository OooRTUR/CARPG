using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
