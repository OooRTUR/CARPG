using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class RuntimeAssets: SingletonObject<RuntimeAssets>
{
    public List<GameObject> bodyParts;
    public List<GameObject> headParts;
    public List<GameObject> gunParts;

    private new void Awake()
    {
        base.Awake();
        BuilderConfiguration config = ScriptableObject.CreateInstance<BuilderConfiguration>();

        bodyParts = config.LoadAssets("Body").ToList();
        headParts = config.LoadAssets("Head").ToList();
        gunParts = config.LoadAssets("Gun").ToList();
    }

    public override void OnSingletonDestroyed()
    {
        throw new System.Exception($"There is another instance of RuntimeAssets attached to {gameObject.name}");
    }
}

