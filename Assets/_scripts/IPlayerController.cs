using UnityEngine;
using System.Collections;

public interface IPlayerController
{
    CharacterMovement CharacterMovement { get; set; }
    CharacterConfig Config { get; set; }

    /// <summary>
    /// Main attack. 
    /// </summary>
    void Attack();

    /// <summary>
    /// A fast attack but lower damage than the main attack.
    /// </summary>
    void QuickAttack();

    /// <summary>
    /// Ability for the player to make quick escapes. Can do damage.
    /// </summary>
    void Mobility();

    /// <summary>
    /// Abilities not limited to damage. Such as healing effects.
    /// </summary>
    void Utility();

    /// <summary>
    /// Strongest ability and longest cooldown......typically.
    /// </summary>
    void Ultimate();
}
