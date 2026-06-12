using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

/// <summary>
///  SPLIT-SCREEN CAMERA SETUP (companion to Scene-LocalMultiplayerSplitScreen).
///
///  This is camera code, not input code - the BMS InputManager handles input only. It's included
///  because local split-screen needs per-player camera work that has to happen at runtime: every
///  player is spawned from the SAME prefab, so you can't give each one a unique Cinemachine channel
///  in the editor (all clones would share it). This assigns that channel by player index on spawn.
///
///  Put this on the ROOT of the player prefab (e.g. PlayerPrefabExampleCameraGroup). The prefab is
///  expected to contain, as children:
///    - a Camera with a CinemachineBrain   (the view that renders this player's split)
///    - a CinemachineCamera (vcam)          (the follow camera, Tracking Target = this player's body)
///    - a PlayerInput                       (drives input; also tells PlayerInputManager the viewport)
///
///  Why it's needed: without unique channels, every Brain's Channel Mask = "Everything", so every
///  Brain sees every vcam and the most recently spawned player hijacks all of them. Giving player N
///  its own channel makes Brain N only follow vcam N.
/// </summary>
[DisallowMultipleComponent]
public class PlayerCameraChannelExample : MonoBehaviour
{
    [Header("References (auto-found in children if left empty)")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineCamera vcam;

    private void Awake()
    {
        // Auto-wire from within this prefab instance so there are no broken references per clone.
        if (playerInput == null) playerInput = GetComponentInChildren<PlayerInput>(true);
        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>(true);
        if (brain == null && playerCamera != null) brain = playerCamera.GetComponent<CinemachineBrain>();
        if (vcam == null) vcam = GetComponentInChildren<CinemachineCamera>(true);

        // Tell PlayerInputManager which camera belongs to this player so it can lay out the
        // split-screen viewport rect for it. (Can also be set on the PlayerInput "Camera" field.)
        if (playerInput != null && playerCamera != null)
            playerInput.camera = playerCamera;
    }

    private void Start()
    {
        // playerIndex is assigned by PlayerInputManager during spawn, so it's ready by Start.
        int index = playerInput != null ? playerInput.playerIndex : 0;

        // OutputChannels is a [Flags] enum: Default = bit 0, Channel01 = bit 1, Channel02 = bit 2...
        // Shift by index + 1 so player 0 -> Channel01, player 1 -> Channel02, etc. (skips Default).
        OutputChannels channel = (OutputChannels)(1 << (index + 1));

        if (vcam != null)  vcam.OutputChannel = channel;   // this player's vcam broadcasts on its channel
        if (brain != null) brain.ChannelMask = channel;    // this player's Brain listens to only that channel
    }
}
