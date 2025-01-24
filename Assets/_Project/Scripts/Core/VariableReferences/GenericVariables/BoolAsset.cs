using System;
using UnityEngine;

namespace MonkeyBusiness.Core.VariableReferences.GenericVariables
{
    [Serializable, CreateAssetMenu(fileName = "Bool", menuName = "BFB/Variables/Bool")]
    public class BoolAsset : ValueAsset<bool>
    {
    }
}
