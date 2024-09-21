using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static Action OnAnimationFirstEvent;
    public static Action OnAnimationSecondEvent;
    public static Action OnAnimationThirdEvent;
    public static Action OnAnimationEndEvent;
    public void AnimationEndEvent()
    {
        OnAnimationEndEvent?.Invoke();
    }

    public void AnimationFirstEvent()
    {
        OnAnimationFirstEvent?.Invoke();
    }

    public void AnimationSecondEvent()
    {
        OnAnimationSecondEvent?.Invoke();
    }

    public void AnimationThirdEvent()
    {
        OnAnimationThirdEvent?.Invoke();
    }

    public void ClearAllEvents()
    {
        OnAnimationFirstEvent = null;
        OnAnimationSecondEvent = null;
        OnAnimationThirdEvent = null;
        OnAnimationEndEvent = null;
    }
}
