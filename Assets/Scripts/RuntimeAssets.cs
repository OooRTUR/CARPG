using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using BuilderConfiguration = VehicleBuilder.Configuration.BuilderConfiguration;
using VehicleBuilder;

public class RuntimeAssets : SingletonObject<RuntimeAssets>
{
    public List<VehiclePartContext> bodyParts;
    public List<VehiclePartContext> headParts;
    public List<VehiclePartContext> gunParts;

    private new void Awake()
    {
        base.Awake();
        var config = ScriptableObject.CreateInstance<BuilderConfiguration>();
        foreach(var partPath in config.GetPartPaths("Body"))
        {
            bodyParts.Add(new VehiclePartContext(partPath, typeof(BodyBuildData)));
        }

        //bodyParts = config.LoadAssets("Body").ToList();
        //headParts = config.LoadAssets("Head").ToList();
        //gunParts = config.LoadAssets("Gun").ToList();
    }

    public override void OnSingletonDestroyed()
    {
        throw new System.Exception($"There is another instance of RuntimeAssets attached to {gameObject.name}");
    }
}