using UnityEngine;

namespace MonkeyBusiness.Gameplay.Components
{
    public class StayUpright : MonoBehaviour
    {
        void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
