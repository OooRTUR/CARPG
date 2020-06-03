using UnityEditor;
using UnityEngine;

public class SomeBehaviour : MonoBehaviour
{

    public void SomeDebug()
    {
        Debug.Log("Some debug");

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class SomeScriptable : ScriptableObject
{
    [MenuItem("Examples/Some Script")]
    static public void SomeScript()
    {
        GameObject.FindObjectOfType<SomeBehaviour>().SomeDebug();
    }
}
