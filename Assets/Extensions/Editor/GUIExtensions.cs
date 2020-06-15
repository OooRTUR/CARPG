using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace Extenstions.Editor
{
    public class GUIExtensions
    {
        public static void Button(string label, Action action)
        {
            if (GUILayout.Button(label))
            {
                action.Invoke();
            }
        }

        public static void PositionHandle(UnityEngine.Object obj, Type type, string propName)
        {
            PropertyInfo propInfo = type.GetProperty(propName);
            EditorGUI.BeginChangeCheck();
            Vector3 handlePosition = Handles.PositionHandle((Vector3)propInfo.GetValue(obj), Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(obj, "Change Fire Position");
                propInfo.SetValue(obj, handlePosition);
            }
        }

    }

    public class SelectionE
    {
        protected int selected = 0;
        protected string[] options;
        public event EventHandler SelectedChanged;
        public int Selected
        {
            get { return selected; }
            private set
            {
                if (selected != value)
                {
                    selected = value;
                    OnSelectedChanged();
                }
            }
        }
        protected void OnSelectedChanged()
        {
            SelectedChanged?.Invoke(this, new EventArgs());
        }
        public virtual void OnGUI()
        {
            Selected = EditorGUILayout.Popup(Selected, options);
        }

        public SelectionE(IEnumerable<string> options)
        {
            this.options = options.ToArray();
        }

        public void OnSelected_Invoke(int selected, Action action)
        {
            if (Selected == selected)
            {
                action.Invoke();
            }
        }
    }

}