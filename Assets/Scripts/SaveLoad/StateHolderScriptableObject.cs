using UnityEditor;
using UnityEngine;

namespace Game
{
    public abstract class StateHolderScriptableObject : ScriptableObject
    {
        public abstract void SetData(object data);
    }

}