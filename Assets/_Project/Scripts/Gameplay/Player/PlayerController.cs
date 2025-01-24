using MonkeyBusiness.Input;
using UnityEngine;

namespace MonkeyBusiness.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Private Fields

        private PlayerInputs _inputs;

        #endregion

        
        #region Mono Methods

        public void Awake()
        {
            _inputs = PlayerInputs.Instance;
        }


        #endregion

    }
}
