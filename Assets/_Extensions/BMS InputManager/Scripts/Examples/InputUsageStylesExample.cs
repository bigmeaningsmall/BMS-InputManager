using UnityEngine;

/// <summary>
///  Side-by-side example of the TWO ways to consume the asset, so you can pick a style:
///
///  1) EVENT style (via InputHandler) - clean, decoupled, "push" reactions. Subscribe in OnEnable,
///     unsubscribe in OnDisable. Great for discrete actions (jump, confirm, UI) and keeps logic out
///     of Update. Cannot express the time-based interactions (Tap/Hold/SlowTap/MultiTap), and you
///     must manage subscribe/unsubscribe to avoid leaks.
///
///  2) POLLING style (via InputManager / InputActionState) - "pull" every frame in Update. This is a
///     superset: it has Pressed()/Held()/Released() AND the interactions. Leak-proof (nothing to
///     unsubscribe), but all the input lives in Update.
///
///  Rule of thumb: default to polling for gameplay entities (it does everything and can't leak), and
///  use events for discrete, decoupled, or UI-style reactions. Mixing both - as below - is fine.
///
///  IMPORTANT: poll the edges (Pressed/Released/Tap/...) in Update, NOT FixedUpdate. FixedUpdate can
///  run zero or several times per frame and would miss or double them. For physics, read/cache the
///  input here and apply forces in FixedUpdate.
/// </summary>
public class InputUsageStylesExample : MonoBehaviour
{
    public InputHandler inputHandler; // Source of the C# events (event style)
    public InputManager inputManager; // Source of the polled InputActionState (polling style)

    // Cached from the stick event so it can be used later (the event -> cache -> use pattern).
    private Vector2 cachedLeftStick;

    private void Awake()
    {
        // Auto-assign if both are on the same GameObject.
        if (inputHandler == null) inputHandler = GetComponent<InputHandler>();
        if (inputManager == null) inputManager = GetComponent<InputManager>();
    }

    // =====================================================================================
    // 1) EVENT STYLE - subscribe with NAMED methods (never lambdas, so -= actually removes them).
    // =====================================================================================
    private void OnEnable()
    {
        if (inputHandler == null) return;

        inputHandler.OnButtonSouth += HandleJump;          // discrete: fire once on press
        inputHandler.OnButtonSouthCanceled += HandleJumpReleased;
        inputHandler.OnLeftStick += HandleLeftStick;       // continuous: cache the value
        inputHandler.OnLeftStickCanceled += HandleLeftStickCanceled;
    }

    private void OnDisable()
    {
        if (inputHandler == null) return;

        // Must mirror OnEnable exactly - same named methods - or the handlers leak.
        inputHandler.OnButtonSouth -= HandleJump;
        inputHandler.OnButtonSouthCanceled -= HandleJumpReleased;
        inputHandler.OnLeftStick -= HandleLeftStick;
        inputHandler.OnLeftStickCanceled -= HandleLeftStickCanceled;
    }

    private void HandleJump()
    {
        // Clean place for a discrete reaction. Don't do physics directly here - if you need forces,
        // set a flag and apply it in FixedUpdate.
        Debug.Log("[Event] ButtonSouth pressed -> Jump");
    }

    private void HandleJumpReleased()
    {
        Debug.Log("[Event] ButtonSouth released");
    }

    private void HandleLeftStick(Vector2 input)
    {
        // Cache now, use later (in Update/FixedUpdate). Events fire at input-processing time, not
        // during your game loop, so caching keeps movement in step with the rest of your logic.
        cachedLeftStick = input;
    }

    private void HandleLeftStickCanceled()
    {
        cachedLeftStick = Vector2.zero;
    }

    // =====================================================================================
    // 2) POLLING STYLE - read state every frame. This is where the interactions live.
    // =====================================================================================
    private void Update()
    {
        // Example of consuming the cached event value during the game loop.
        if (cachedLeftStick != Vector2.zero)
            Debug.Log($"[Event->cache] using LeftStick = {cachedLeftStick:F2}");

        if (inputManager == null) return;

        InputActionState button = inputManager.ButtonSouth;

        // Polled equivalents of the events above (same ButtonSouth, just pulled instead of pushed):
        if (button.Pressed())  Debug.Log("[Poll] ButtonSouth Pressed");
        if (button.Released()) Debug.Log("[Poll] ButtonSouth Released");

        // Interactions - ONLY available by polling (no event exists for these):
        if (button.Tap())        Debug.Log("[Poll] ButtonSouth Tap");
        if (button.SlowTap())    Debug.Log("[Poll] ButtonSouth SlowTap");
        if (button.Hold())       Debug.Log("[Poll] ButtonSouth Hold (one-shot)");
        if (button.HoldActive()) Debug.Log("[Poll] ButtonSouth charging...");
        if (button.MultiTap(2))  Debug.Log("[Poll] ButtonSouth Double tap");
    }
}
