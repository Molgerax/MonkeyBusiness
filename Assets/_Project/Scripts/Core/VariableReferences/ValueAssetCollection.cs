using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BFB.Core.VariableReferences
{
    [CreateAssetMenu(fileName = "NewValueAssetCollection", menuName = "BFB/Variables/Value Asset Collection")]
    public class ValueAssetCollection : ScriptableObject
    {
        [SerializeField] private List<ValueAsset> childAssets = new List<ValueAsset>();

        public List<ValueAsset> ValueAssets => childAssets;
        
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            childAssets = GetSubObjectOfType<ValueAsset>(this);
        }
        
        public static List<T> GetSubObjectOfType<T>(ScriptableObject asset) where T : ScriptableObject
        {
            Object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(asset));
     
            List<T> ofType = new List<T>();
     
            foreach(Object o in objs)
            {
                if(o is T)
                {
                    ofType.Add(o as T);
                }
            }
     
            return ofType;
        }

#endif
    }

}