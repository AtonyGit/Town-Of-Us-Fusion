using MiraAPI.Utilities.Assets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiraAPI.Hud;

/// <summary>
/// Class for making custom action buttons. More customizable than the default Action/Ability buttons in the base game.
/// </summary>
public abstract class CustomTouActionButton : CustomActionButton
{
    /// <summary>
    /// Gets the color for the button's text.
    /// </summary>
    public abstract Color TextColor { get; }

    /// <summary>
    /// Changes the keybind for the button, if it isn't a blank string.
    /// Options: ActionSecondary, ActionQuaternary, ActionCustom1, ActionCustom2.
    /// Usually ActionSecondary is binded to Q, and ActionQuaternary is binded to F, while the rest are not always binded.
    /// </summary>
    public abstract string ButtonKeybind { get; }

    /// <summary>
    /// This method handles the button click event. It is a wrapper for the <see cref="OnClick"/> method.
    /// This method takes into account cooldowns, effects, and uses, before calling the <see cref="OnClick"/> method.
    /// It can be overridden for custom behavior.
    /// </summary>
    public virtual void DoClick()
    {
        if (!CanUse())
        {
            return;
        }

        if (LimitedUses)
        {
            UsesLeft--;
            Button?.SetUsesRemaining(UsesLeft);
        }

        OnClick();
        Button?.SetDisabled();
        if (HasEffect)
        {
            EffectActive = true;
            Timer = EffectDuration;
        }
        else
        {
            Timer = Cooldown;
        }
    }

    /// <summary>
    /// This method is called on the PlayerControl.FixedUpdate method. It is a wrapper for the <see cref="FixedUpdate"/> method.
    /// By default, it handles the cooldown and effect timers, and sets the button to enabled or disabled.
    /// It can be overridden for custom behavior.
    /// </summary>
    /// <param name="playerControl">The local PlayerControl.</param>
    public override void FixedUpdateHandler(PlayerControl playerControl)
    {
        if (Timer >= 0)
        {
            Timer -= Time.deltaTime;
        }
        else if (HasEffect && EffectActive)
        {
            EffectActive = false;
            Timer = Cooldown;
            OnEffectEnd();
        }

        if (CanUse())
        {
            Button?.SetEnabled();
        }
        else
        {
            Button?.SetDisabled();
        }

        Button?.SetCoolDown(Timer, EffectActive ? EffectDuration : Cooldown);

        FixedUpdate(playerControl);
    }
}

/// <summary>
/// Custom action button that has a target object.
/// </summary>
/// <typeparam name="T">The type of the target object.</typeparam>
public abstract class CustomTouActionButton<T> : CustomTouActionButton where T : MonoBehaviour
{
    /// <summary>
    /// Gets or sets the target object of the button.
    /// </summary>
    public T? Target { get; protected set; }

    /// <summary>
    /// Gets the distance the player must be from the target object to use the button.
    /// </summary>
    public virtual float Distance => PlayerControl.LocalPlayer.Data.Role.GetAbilityDistance();

    /// <summary>
    /// Determines if the target object is valid.
    /// </summary>
    /// <param name="target">The target object being checked.</param>
    /// <returns>True if the target object is valid, false otherwise.</returns>
    public virtual bool IsTargetValid(T? target)
    {
        return target;
    }

    /// <summary>
    /// The method used to get the target object.
    /// </summary>
    /// <returns>The target object or null if it isn't found.</returns>
    public abstract T? GetTarget();

    /// <summary>
    /// Sets the outline of the target object.
    /// </summary>
    /// <param name="active">Should the outline be active.</param>
    public abstract void SetOutline(bool active);

    /// <inheritdoc />
    public override bool CanUse()
    {
        var newTarget = GetTarget();
        if (newTarget != Target)
        {
            SetOutline(false);
        }

        Target = IsTargetValid(newTarget) ? newTarget : null;
        SetOutline(true);

        return base.CanUse() && Target;
    }
}
