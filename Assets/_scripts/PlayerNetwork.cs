using UnityEngine.Events;
using UnityEngine.Networking;

namespace Assets._scripts
{
    public class PlayerNetwork : NetworkBehaviour
    {
        private CharacterConfig _config;

        public IPlayerController _PlayerController;
        public CharacterMovement _CharacterMovement;

        public UnityAction Attack;
        public UnityAction QuickAttack;
        public UnityAction Mobility;
        public UnityAction Ultimate;
        public UnityAction Utility;


        void Awake()
        {
            _CharacterMovement = _PlayerController.CharacterMovement;
            var characterConfig = _PlayerController.Config;
            SetConfig(ref characterConfig);
            SetAbilities(_PlayerController);
        }

        void Update()
        {
            _CharacterMovement.UpdateMovement(_config.amIrooted, _config.inControl);
        }

        void FixedUpdate()
        {
            _CharacterMovement.FixedUpdateForCamera(); // Character movement handles camera controls
        }

        protected void SetConfig(ref CharacterConfig config)
        {
            _config = config;
        }

        protected void SetAbilities(IPlayerController playerController)
        {
            Attack += playerController.Attack;
            QuickAttack += playerController.QuickAttack;
            Mobility += playerController.Mobility;
            Ultimate += playerController.Ultimate;
            Utility += playerController.Utility;
        }

        /// <summary>
        /// Adjust the player's health. Positive to heal. Negative for damage.
        /// </summary>
        /// <param name="adjustment"></param>
        [ClientRpc]
        public void AdjustHealth(float adjustment)
        {
            _config.health += adjustment;
        }

        /// <summary>
        /// Set the value for the player to be rooted.
        /// </summary>
        /// <param name="isRooted"></param>
        [ClientRpc]
        public void SetPlayerRoot(bool isRooted)
        {
            _config.amIrooted = isRooted;
        }

        /// <summary>
        /// Root or remove root 
        /// </summary>
        /// <param name="isRooted"></param>
        /// <param name="duration"></param>
        [ClientRpc]
        public void SetPlayerRootWithDuration(bool isRooted, float duration)
        {
            _config.amIrooted = isRooted;
            Invoke("RemoveRoot", duration);
        }

        /// <summary>
        /// Are we giving the player control or removing the player's control?
        /// </summary>
        /// <param name="isEnabled"></param>
        [ClientRpc]
        public void SetIsPlayerEnabled(bool isEnabled)
        {
            _config.inControl = isEnabled;
        }

        /// <summary>
        /// Remove any roots on the player
        /// </summary>
        [ClientRpc]
        public void RemoveRoot()
        {
            _config.amIrooted = false;
        }

    }
}
