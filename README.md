# BMS-InputManager

## Unity Event-Based Input Manager

**Version: v1.1.0**

This repository provides an event-driven input management system for Unity using the **Unity Input System**. It simplifies input handling by exposing events for various gamepad controls, allowing developers to subscribe and react to inputs efficiently.

The input action map is created for gamepad primarily. Keyboard bindings are added but require custom mapping and configuration depending on your input control requirements.

![InputManagerGamepad_01](https://github.com/user-attachments/assets/7410817e-5667-4fe1-a700-73e12c11f392)

---

## Features

- Event-driven input handling for gamepads.
- Supports all standard controller inputs (buttons, sticks, triggers, and D-pad).
- Simple subscription model for listening to input events.
- Frame-precise button state tracking via `InputActionState` (pressed, held, released).
- High-level `InputManager` aggregates all states in one place.
- Debugging utilities to log inputs in real-time.
- Real-time gamepad visualiser scene included.

---

## Architecture

The system is structured in four layers:

| Layer | Class | Responsibility |
|---|---|---|
| Event Distribution | `InputHandler` | Reads from `PlayerInput`, fires 50+ C# events |
| State Aggregation | `InputManager` | Subscribes to `InputHandler`, maintains `InputActionState` per button |
| State Tracking | `InputActionState` | Frame-precise pressed / held / released detection |
| Debug & Visualisation | `InputDebugger`, `InputVisualiser` | Logging and real-time visual feedback |

Example scripts (`InputJumpExample`, `InputMoveExample`, `InputEventSubscriptionExample`) demonstrate how to consume the system in gameplay code.

# UML
UML to be updated!

---

## Scene Examples

| Scene | Description |
|---|---|
| `Scene-GamepadInputVisualiser` | Displays active gamepad inputs in real-time |
| `Scene-InputExample-Gamepad&Keyboard` | Debug inputs from gamepad and keyboard |
| `Scene-LocalMultiplayer` | Uses `PlayerInputManager` to handle multiple players / devices |
| `Scene-LocalMultiplayerSplitScreen` | Multiple players with split-screen using `PlayerInputManager` |
| `Scene-SinglePlayer-AnyDevice` | Accepts input from any connected device |

---

## Getting Started

### 1. Setup

Ensure you have the **Unity Input System** package installed:

1. Open **Package Manager** (`Window > Package Manager`).
2. Search for **Input System** and install it.
3. Go to `Edit > Project Settings > Player` and set **Active Input Handling** to `Both` or `Input System Package`.

### 2. Adding the Input Handler

1. Attach the `InputHandler` script to a **GameObject** in your scene.
2. The `InputHandler` requires a `PlayerInput` component on the same GameObject — assign your **Input Action Asset** (`InputSystem_Actions_BMS.inputactions`) to it.
3. Optionally add `InputManager` to the same GameObject; it will auto-assign the `InputHandler` reference and aggregate all button states.

### 3. Subscribing to Input Events

Subscribe to events from `InputHandler` in `OnEnable` and unsubscribe in `OnDisable`.

```csharp
using UnityEngine;

public class InputDebugger : MonoBehaviour
{
    private void OnEnable()
    {
        InputHandler.OnButtonSouth += () => Debug.Log("Button South Pressed");
        InputHandler.OnLeftStick += (Vector2 input) => Debug.Log($"Left Stick: {input}");
    }

    private void OnDisable()
    {
        InputHandler.OnButtonSouth -= () => Debug.Log("Button South Pressed");
        InputHandler.OnLeftStick -= (Vector2 input) => Debug.Log($"Left Stick: {input}");
    }
}
```

See `InputEventSubscriptionExample.cs` for a complete reference covering all inputs.

### 4. Using InputManager for Frame-Precise State

`InputManager` exposes per-button `InputActionState` properties that let you query state within `Update`:

```csharp
[SerializeField] private InputManager _inputManager;

private void Update()
{
    if (_inputManager.ButtonSouth.Pressed())   // true only on the frame of press
        Jump();

    if (_inputManager.ButtonSouth.Held())      // true while held
        ChargeJump();

    if (_inputManager.ButtonSouth.Released())  // true only on the frame of release
        ReleaseJump();
}
```

### 5. Interactions (Tap / Hold / Slow Tap / Multi-Tap)

Every button-style `InputActionState` on `InputManager` also exposes higher-level interactions,
so you can just poll the ones you need from `Update`:

| Method | Returns true when |
|---|---|
| `Hold(duration = default)` | **Once**, after the button has been held past the hold time |
| `HoldActive(duration = default)` | **Every frame** while held past the hold time (charge-while-held) |
| `Tap(maxDuration = default)` | On release of a quick press |
| `SlowTap(minDuration = default)` | On release of a long press |
| `MultiTap(count = 2, maxTapDuration = default)` | When `count` quick taps occur in succession (e.g. double-tap) |

```csharp
private void Update()
{
    if (_inputManager.ButtonSouth.Tap())      Confirm();
    if (_inputManager.ButtonSouth.Hold())     OpenRadialMenu();   // one-shot
    if (_inputManager.ButtonSouth.MultiTap(2)) Dash();            // double-tap
}
```

Default timings are read from **Project Settings → Input System Package** (tap, hold, slow tap and
multi-tap delay), so they stay consistent with Unity's own interactions; each method also accepts an
optional override. These apply to all button states — face buttons, shoulders, stick presses, d-pad,
the digital stick directions, the trigger presses, and start/select. See `InputInteractionExample.cs`.

### 6. Using the Gamepad Input Visualiser

A pre-configured scene, **`Scene-GamepadInputVisualiser`**, is included to display gamepad inputs in real-time. Open this scene and run the game to see visual feedback of gamepad inputs.

---

## Events Reference

### Analog Inputs

| Input | Performed Event | Canceled Event |
|---|---|---|
| Left Stick | `OnLeftStick` | `OnLeftStickCanceled` |
| Right Stick | `OnRightStick` | `OnRightStickCanceled` |
| Left Trigger | `OnLeftTrigger` | `OnLeftTriggerCanceled` |
| Right Trigger | `OnRightTrigger` | `OnRightTriggerCanceled` |

### Face Buttons

| Input | Performed Event | Canceled Event |
|---|---|---|
| Button South | `OnButtonSouth` | `OnButtonSouthCanceled` |
| Button North | `OnButtonNorth` | `OnButtonNorthCanceled` |
| Button West | `OnButtonWest` | `OnButtonWestCanceled` |
| Button East | `OnButtonEast` | `OnButtonEastCanceled` |

### Shoulders & Triggers (Digital)

| Input | Performed Event | Canceled Event |
|---|---|---|
| Left Shoulder | `OnLeftShoulder` | `OnLeftShoulderCanceled` |
| Right Shoulder | `OnRightShoulder` | `OnRightShoulderCanceled` |
| Left Trigger Pressed | `OnLeftTriggerPressed` | `OnLeftTriggerReleased` |
| Right Trigger Pressed | `OnRightTriggerPressed` | `OnRightTriggerReleased` |

### Stick Clicks

| Input | Performed Event | Canceled Event |
|---|---|---|
| Left Stick Press | `OnLeftStickPress` | `OnLeftStickPressCanceled` |
| Right Stick Press | `OnRightStickPress` | `OnRightStickPressCanceled` |

### D-Pad

| Input | Performed Event | Canceled Event |
|---|---|---|
| D-Pad Left | `OnPadLeft` | `OnPadLeftCanceled` |
| D-Pad Right | `OnPadRight` | `OnPadRightCanceled` |
| D-Pad Up | `OnPadUp` | `OnPadUpCanceled` |
| D-Pad Down | `OnPadDown` | `OnPadDownCanceled` |

### Stick Directions (Digital)

| Input | Performed Event | Canceled Event |
|---|---|---|
| Left Stick Left | `OnLeftStickLeft` | `OnLeftStickLeftCanceled` |
| Left Stick Right | `OnLeftStickRight` | `OnLeftStickRightCanceled` |
| Left Stick Up | `OnLeftStickUp` | `OnLeftStickUpCanceled` |
| Left Stick Down | `OnLeftStickDown` | `OnLeftStickDownCanceled` |
| Right Stick Left | `OnRightStickLeft` | `OnRightStickLeftCanceled` |
| Right Stick Right | `OnRightStickRight` | `OnRightStickRightCanceled` |
| Right Stick Up | `OnRightStickUp` | `OnRightStickUpCanceled` |
| Right Stick Down | `OnRightStickDown` | `OnRightStickDownCanceled` |

### Menu Buttons

| Input | Performed Event | Canceled Event |
|---|---|---|
| Start | `OnButtonStart` | `OnButtonStartCanceled` |
| Select | `OnButtonSelect` | `OnButtonSelectCanceled` |

---

## Contributions

Feel free to fork, modify, and submit pull requests. Contributions and feedback are always welcome!

## License

This project is open-source and licensed under the MIT License.
