using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HeadBuilder : MonoBehaviour
{
    private GameObject attachedGun;
    private GunPartPositionData gunPositionData;

    void Build()
    {

         
    }
}

[CreateAssetMenu]
public class GunPartPositionData : ScriptableObject, ISerializationCallbackReceiver
{
    public float Value;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        
    }
}
