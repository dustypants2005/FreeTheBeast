using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._scripts
{
    public class PlayerControllerBase_ : NetworkBehaviour
    {
        public CharacterMovement CharacterMovement { get; set; }
        public CharacterConfig Config { get; set; }
        public GunAttack GunAttack { get; set; }

        void Awake()
        {
            CharacterMovement = new CharacterMovement();
            CharacterMovement.Init(isLocalPlayer, GetComponent<CharacterController>());
            Config = new CharacterConfig();
            GunAttack = new GunAttack();
            GunAttack.SetGun(transform);

        }

        void Update()
        {
            if (!isLocalPlayer) return;   // if we are not this player, we should not control that player.     
            CharacterMovement.UpdateMovement(Config.amIrooted, Config.inControl);
        }

        public void Attack()
        {
            GunAttack.Fire();
        }

        public void QuickAttack()
        {
            throw new NotImplementedException();
        }

        public void Mobility()
        {
            throw new NotImplementedException();
        }

        public void Utility()
        {
            throw new NotImplementedException();
        }

        public void Ultimate()
        {
            throw new NotImplementedException();
        }
    }
}

