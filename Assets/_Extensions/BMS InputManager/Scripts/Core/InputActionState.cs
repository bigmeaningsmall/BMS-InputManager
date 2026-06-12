using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Tracks the state of a single button-style input and derives higher level interactions
/// (tap, hold, slow tap, multi-tap) entirely in code from the simple pressed/released events.
///
/// All query methods are designed to be POLLED every frame (typically from Update), exactly
/// like Pressed()/Released(). Default timings are read from the project's Input System settings
/// (Project Settings > Input System Package) so they stay consistent with Unity's own
/// interactions; every method also accepts an optional override.
/// </summary>
public class InputActionState
{
    private bool wasPressed = false;
    private bool isPressed = false;
    private int lastFramePressed = -1;

    // Timestamps for the most recent press/release edges (unscaled real time, so they behave
    // the same while the game is paused - matching Unity's built-in interactions).
    private float pressTime = -1f;
    private float releaseTime = -1f;
    private float lastReleaseTime = -1f;

    private bool holdConsumed = false;   // one-shot guard so Hold() fires once per hold
    private int tapCount = 0;            // consecutive quick taps, for MultiTap()
    private int multiTapReported = 0;    // guard so a completed multi-tap chain reports once

    // Default interaction timings, sourced from the Input System settings.
    private static float DefaultTapTime => InputSystem.settings.defaultTapTime;
    private static float DefaultHoldTime => InputSystem.settings.defaultHoldTime;
    private static float DefaultSlowTapTime => InputSystem.settings.defaultSlowTapTime;
    private static float DefaultMultiTapDelay => InputSystem.settings.multiTapDelayTime;

    private static float Now => Time.realtimeSinceStartup;

    public void SetState(bool pressed)
    {
        // Only update the previous state at the start of a new frame
        if (Time.frameCount != lastFramePressed) {
            wasPressed = isPressed;
            lastFramePressed = Time.frameCount;
        }

        // Detect the actual transitions to stamp the interaction timings.
        if (pressed && !isPressed) // press edge
        {
            pressTime = Now;
            holdConsumed = false;

            // Count quick consecutive taps for multi-tap detection.
            if (lastReleaseTime >= 0f && pressTime - lastReleaseTime <= DefaultMultiTapDelay)
                tapCount++;
            else
                tapCount = 1;

            multiTapReported = 0;
        }
        else if (!pressed && isPressed) // release edge
        {
            releaseTime = Now;
            lastReleaseTime = releaseTime;
        }

        // Always update the current state
        isPressed = pressed;
    }

    // Returns true continuously while the button is held down
    public bool Held() => isPressed;

    // Returns true ONLY on the frame when button transitions from not pressed to pressed
    public bool Pressed() => isPressed && !wasPressed && Time.frameCount == lastFramePressed;

    // Returns true ONLY on the frame when button transitions from pressed to not pressed
    public bool Released() => !isPressed && wasPressed && Time.frameCount == lastFramePressed;

    /// <summary>
    /// One-shot hold: returns true a single time once the button has been held for at least
    /// <paramref name="duration"/> seconds (defaults to the Input System hold time).
    /// </summary>
    public bool Hold(float duration = -1f)
    {
        if (duration < 0f) duration = DefaultHoldTime;
        if (isPressed && !holdConsumed && Now - pressTime >= duration)
        {
            holdConsumed = true;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Continuous hold: returns true every frame while the button has been held for at least
    /// <paramref name="duration"/> seconds. Useful for "charge while held" behaviour.
    /// </summary>
    public bool HoldActive(float duration = -1f)
    {
        if (duration < 0f) duration = DefaultHoldTime;
        return isPressed && Now - pressTime >= duration;
    }

    /// <summary>
    /// Tap: returns true on the release frame when the press lasted no longer than
    /// <paramref name="maxDuration"/> seconds (defaults to the Input System tap time).
    /// </summary>
    public bool Tap(float maxDuration = -1f)
    {
        if (maxDuration < 0f) maxDuration = DefaultTapTime;
        return Released() && releaseTime - pressTime <= maxDuration;
    }

    /// <summary>
    /// Slow tap: returns true on the release frame when the press was held for at least
    /// <paramref name="minDuration"/> seconds (defaults to the Input System slow tap time).
    /// </summary>
    public bool SlowTap(float minDuration = -1f)
    {
        if (minDuration < 0f) minDuration = DefaultSlowTapTime;
        return Released() && releaseTime - pressTime >= minDuration;
    }

    /// <summary>
    /// Multi-tap: returns true once on the release that completes <paramref name="count"/> quick
    /// taps in succession, where each tap is shorter than <paramref name="maxTapDuration"/> and the
    /// gap between taps is within the Input System multi-tap delay.
    /// </summary>
    public bool MultiTap(int count = 2, float maxTapDuration = -1f)
    {
        if (maxTapDuration < 0f) maxTapDuration = DefaultTapTime;
        if (Released() && tapCount >= count && tapCount != multiTapReported
            && releaseTime - pressTime <= maxTapDuration)
        {
            multiTapReported = tapCount;
            return true;
        }
        return false;
    }

    // Reset the state (useful when enabling/disabling input)
    public void Reset()
    {
        wasPressed = false;
        isPressed = false;
        lastFramePressed = -1;

        pressTime = -1f;
        releaseTime = -1f;
        lastReleaseTime = -1f;
        holdConsumed = false;
        tapCount = 0;
        multiTapReported = 0;
    }
}
