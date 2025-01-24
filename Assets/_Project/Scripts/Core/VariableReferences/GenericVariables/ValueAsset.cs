using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BFB.Core.VariableReferences
{
    public abstract class ValueAsset : ScriptableObject
    {
        private ValueAssetCollection parentCollection;
        public ValueAssetCollection Parent => parentCollection;

        public void Initialize(ValueAssetCollection parent)
        {
            parentCollection = parent;
        }

        
        
#if UNITY_EDITOR
        public void Rename(string name)
        {
            this.name = name;
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(this);
        }
        
        public bool IsSubAsset()
        {
            return AssetDatabase.IsSubAsset(this);
        }
        
        public void Decouple()
        {
            if (!parentCollection) return;
            parentCollection.ValueAssets.Remove(this);
            AssetDatabase.RemoveObjectFromAsset(this);
            AssetDatabase.SaveAssets();
        }

        public void Delete()
        {
            if (!parentCollection) return;
            parentCollection.ValueAssets.Remove(this);
            Undo.DestroyObjectImmediate(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
    
    public abstract class ValueAsset<T> : ValueAsset
    {
        public T value;
    }
}
