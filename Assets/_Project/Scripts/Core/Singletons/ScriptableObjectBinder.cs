using UnityEngine;

namespace MonkeyBusiness.Core.Singletons
{
    public class ScriptableObjectBinder : MonoBehaviour
    {
        #region Serialize Fields

        [SerializeField] private ScriptableObject[] scriptableObjects;

        #endregion
    }
}
