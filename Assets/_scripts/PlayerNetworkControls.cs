using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace Assets._scripts
{
    public class PlayerNetworkControls : NetworkManager
    {
        private CharacterConfig config;

        public PlayerNetworkControls()
        {
            config = this.gameObject.GetComponent<CharacterConfig>();
        }

        /// <summary>
        /// Positive to heal. Negative for damage.
        /// </summary>
        /// <param name="hpChange"></param>
        public void AdjustHealth(float hpChange)
        {
            config.health += hpChange;
        }
    }
}
