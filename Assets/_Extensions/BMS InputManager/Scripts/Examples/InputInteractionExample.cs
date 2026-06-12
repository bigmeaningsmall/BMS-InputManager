using UnityEngine;

/// <summary>
///  Example of the polled interaction API on InputActionState (exposed through InputManager).
///  Every button-style input gains these queries automatically, so you can just read the ones
///  you need from Update:
///
///   - Pressed()      : true only on the frame the button goes down
///   - Released()     : true only on the frame the button goes up
///   - Held()         : true every frame while down
///   - Hold()         : true ONCE after the button has been held past the hold time
///   - HoldActive()   : true every frame while held past the hold time (e.g. charge-while-held)
///   - Tap()          : true on release of a quick press
///   - SlowTap()      : true on release of a long press
///   - MultiTap(n)    : true when n quick taps happen in succession (e.g. double-tap)
///
///  Default timings come from Project Settings > Input System Package; each method also takes an
///  optional duration/count override.
///
///  The same calls work on any button state on InputManager (face buttons, shoulders, stick
///  presses, d-pad, the digital stick directions, the trigger presses, start/select). Below are
///  three worked examples - a face button, a trigger, and a stick.
/// </summary>
public class InputInteractionExample : MonoBehaviour
{
    public InputManager inputManager; // Reference to the InputManager script

    private void Awake()
    {
        // Auto-assign if the InputManager is on the same GameObject.
        if (inputManager == null)
        {
            inputManager = GetComponent<InputManager>();
        }
    }

    private void Update()
    {
        if (inputManager == null) return;

        ButtonExample();
        TriggerExample();
        StickExample();
    }

    // ------------------------------------------------------------------------
    // 1) A face button - the full set of interactions on a single button.
    // ------------------------------------------------------------------------
    private void ButtonExample()
    {
        InputActionState button = inputManager.ButtonSouth;

        // --- Basic edges -----------------------------------------------------
        if (button.Pressed())  Debug.Log("ButtonSouth: Pressed");
        if (button.Released()) Debug.Log("ButtonSouth: Released");

        // --- Hold ------------------------------------------------------------
        // One-shot: fires a single time once the hold threshold is crossed.
        if (button.Hold()) Debug.Log("ButtonSouth: Hold (one-shot)");

        // Continuous: true every frame while held past the threshold.
        if (button.HoldActive()) Debug.Log("ButtonSouth: charging...");

        // --- Tap / Slow tap --------------------------------------------------
        if (button.Tap())     Debug.Log("ButtonSouth: Tap (quick press)");
        if (button.SlowTap()) Debug.Log("ButtonSouth: Slow tap (long press then release)");

        // --- Multi-tap -------------------------------------------------------
        if (button.MultiTap(2)) Debug.Log("ButtonSouth: Double tap");

        // Example of an override: treat a 0.75s hold as a "long hold".
        if (button.Hold(0.75f)) Debug.Log("ButtonSouth: Long hold (0.75s)");
    }

    // ------------------------------------------------------------------------
    // 2) A trigger. The digital press state (LeftTriggerPressed) is an
    //    InputActionState, so it gets the same interactions as any button.
    //    The raw analog pull (0..1) is read separately from LeftTriggerInput.
    // ------------------------------------------------------------------------
    private void TriggerExample()
    {
        InputActionState trigger = inputManager.LeftTriggerPressed;

        // --- Basic edges -----------------------------------------------------
        if (trigger.Pressed())  Debug.Log("LeftTrigger: Pressed");
        if (trigger.Released()) Debug.Log("LeftTrigger: Released");

        // --- Hold ------------------------------------------------------------
        if (trigger.Hold())       Debug.Log("LeftTrigger: Hold (one-shot) - e.g. charge a shot");
        if (trigger.HoldActive()) Debug.Log("LeftTrigger: charging...");

        // --- Tap / Slow tap --------------------------------------------------
        if (trigger.Tap())     Debug.Log("LeftTrigger: Tap (quick squeeze)");
        if (trigger.SlowTap()) Debug.Log("LeftTrigger: Slow tap (long squeeze then release)");

        // --- Multi-tap -------------------------------------------------------
        if (trigger.MultiTap(2)) Debug.Log("LeftTrigger: Double tap");

        // Raw analog value (how far the trigger is pulled, 0..1).
        float pull = inputManager.LeftTriggerInput;
        if (pull > 0f) Debug.Log($"LeftTrigger: analog pull = {pull:F2}");
    }

    // ------------------------------------------------------------------------
    // 3) A stick. Each direction is exposed as its own digital InputActionState
    //    (LeftStickUp/Down/Left/Right), so directions also get the interactions -
    //    e.g. a double-tap of a direction makes a natural "dash". The raw analog
    //    vector is read separately from LeftStickInput.
    // ------------------------------------------------------------------------
    private void StickExample()
    {
        InputActionState stickUp = inputManager.LeftStickUp;

        // --- Basic edges -----------------------------------------------------
        if (stickUp.Pressed())  Debug.Log("LeftStickUp: Pressed");
        if (stickUp.Released()) Debug.Log("LeftStickUp: Released");

        // --- Hold ------------------------------------------------------------
        if (stickUp.Hold())       Debug.Log("LeftStickUp: Held up (one-shot)");
        if (stickUp.HoldActive()) Debug.Log("LeftStickUp: holding up...");

        // --- Tap / Slow tap --------------------------------------------------
        if (stickUp.Tap())     Debug.Log("LeftStickUp: Tap up");
        if (stickUp.SlowTap()) Debug.Log("LeftStickUp: Slow tap up");

        // --- Multi-tap -------------------------------------------------------
        if (stickUp.MultiTap(2)) Debug.Log("LeftStickUp: Double tap up - dash!");

        // Raw analog stick vector (-1..1 on each axis).
        Vector2 move = inputManager.LeftStickInput;
        if (move != Vector2.zero) Debug.Log($"LeftStick: analog vector = {move:F2}");
    }
}
