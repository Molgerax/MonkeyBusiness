using System;
using UnityEngine;

namespace BFB.Core.VariableReferences
{
    [Serializable, CreateAssetMenu(fileName = "Int", menuName = "BFB/Variables/Int")]
    public class IntAsset : ValueAsset<int>
    {
    }
}
