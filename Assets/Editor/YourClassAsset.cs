using UnityEngine;
using UnityEditor;

public class YourClassAsset
{
	[MenuItem("Assets/Create/$$$$")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateAsset<GunPartPositionData>();
		
	}
}