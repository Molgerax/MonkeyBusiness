using System;
using UnityEngine;

namespace BFB.Core.VariableReferences
{
    [Serializable, CreateAssetMenu(fileName = "Bool", menuName = "BFB/Variables/Bool")]
    public class BoolAsset : ValueAsset<bool>
    {
    }
}
