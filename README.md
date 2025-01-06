# BMS-InputManager

## Unity Event-Based Input Manager

This repository provides an event-driven input management system for Unity using the **Unity Input System**. It simplifies input handling by exposing events for various gamepad controls, allowing developers to subscribe and react to inputs efficiently.

The input action map is created for gamepad primarily. Keyboard binding are added but require custom mapping and configuration depending on your input control requirements.

![InputManagerGamepad_01](https://github.com/user-attachments/assets/7410817e-5667-4fe1-a700-73e12c11f392)



## Features

- Event-driven input handling for gamepads.
- Supports all standard controller inputs (buttons, sticks, triggers, and D-pad).
- Simple subscription model for listening to input events.
- Debugging utilities to log inputs in real-time.
- A visualisation scene (`Scene-GamepadInputVisualiser`) to display active inputs.

## Getting Started

### 1. Setup

Ensure you have the **Unity Input System** package installed:

1. Open **Package Manager** (`Window > Package Manager`).
2. Search for **Input System** and install it.
3. Enable the new Input System by going to `Edit > Project Settings > Player` and changing the **Active Input Handling** to `Both` or `Input System Package`.

### 2. Adding the Input Handler

1. Attach the `InputHandler` script to a **GameObject** in your scene.
2. Assign your **Input Action Asset** in the `InputHandler` component (replace `Player` with the actual action map name in your asset).
3. The script will automatically subscribe to relevant Unity Input System actions and invoke corresponding events.

### 3. Subscribing to Input Events

To listen for input events, subscribe to them in any script.

#### Example: Debugging Input Events

```csharp
using UnityEngine;

public class InputDebugger : MonoBehaviour
{
    private void OnEnable()
    {
        InputHandler.OnButtonSouth += () => Debug.Log("Button South Pressed");
        InputHandler.OnLeftStick += (Vector2 input) => Debug.Log($"Left Stick Moved: {input}");
    }

    private void OnDisable()
    {
        InputHandler.OnButtonSouth -= () => Debug.Log("Button South Pressed");
        InputHandler.OnLeftStick -= (Vector2 input) => Debug.Log($"Left Stick Moved: {input}");
    }
}
```

## Events List

| Input              | Event Name                |
|--------------------|-------------------------|
| Left Stick        | `OnLeftStick`            |
| Right Stick       | `OnRightStick`           |
| Button South      | `OnButtonSouth`          |
| Button North      | `OnButtonNorth`          |
| Button West       | `OnButtonWest`           |
| Button East       | `OnButtonEast`           |
| Left Trigger      | `OnLeftTrigger`          |
| Right Trigger     | `OnRightTrigger`         |
| Left Trigger Pressed | `OnLeftTriggerPressed` |
| Right Trigger Pressed | `OnRightTriggerPressed` |
| Left Shoulder     | `OnLeftShoulder`         |
| Right Shoulder    | `OnRightShoulder`        |
| Left Stick Press  | `OnLeftStickPress`       |
| Right Stick Press | `OnRightStickPress`      |
| D-Pad Left        | `OnPadLeft`              |
| D-Pad Right       | `OnPadRight`             |
| D-Pad Up          | `OnPadUp`                |
| D-Pad Down        | `OnPadDown`              |
| Left Stick Left   | `OnLeftStickLeft`        |
| Left Stick Right  | `OnLeftStickRight`       |
| Left Stick Up     | `OnLeftStickUp`          |
| Left Stick Down   | `OnLeftStickDown`        |
| Right Stick Left  | `OnRightStickLeft`       |
| Right Stick Right | `OnRightStickRight`      |
| Right Stick Up    | `OnRightStickUp`         |
| Right Stick Down  | `OnRightStickDown`       |
| Start Button      | `OnButtonStart`          |
| Select Button     | `OnButtonSelect`         |

### Canceled Events

| Input              | Event Name                |
|--------------------|-------------------------|
| Left Stick        | `OnLeftStickCanceled`    |
| Right Stick       | `OnRightStickCanceled`   |
| Button South      | `OnButtonSouthCanceled`  |
| Button North      | `OnButtonNorthCanceled`  |
| Button West       | `OnButtonWestCanceled`   |
| Button East       | `OnButtonEastCanceled`   |
| Left Trigger      | `OnLeftTriggerCanceled`  |
| Right Trigger     | `OnRightTriggerCanceled` |
| Left Trigger Released | `OnLeftTriggerReleased` |
| Right Trigger Released | `OnRightTriggerReleased` |
| Left Shoulder     | `OnLeftShoulderCanceled` |
| Right Shoulder    | `OnRightShoulderCanceled` |
| Left Stick Press  | `OnLeftStickPressCanceled` |
| Right Stick Press | `OnRightStickPressCanceled` |
| D-Pad Left        | `OnPadLeftCanceled`      |
| D-Pad Right       | `OnPadRightCanceled`     |
| D-Pad Up          | `OnPadUpCanceled`        |
| D-Pad Down        | `OnPadDownCanceled`      |
| Left Stick Left   | `OnLeftStickLeftCanceled` |
| Left Stick Right  | `OnLeftStickRightCanceled` |
| Left Stick Up     | `OnLeftStickUpCanceled`  |
| Left Stick Down   | `OnLeftStickDownCanceled` |
| Right Stick Left  | `OnRightStickLeftCanceled` |
| Right Stick Right | `OnRightStickRightCanceled` |
| Right Stick Up    | `OnRightStickUpCanceled` |
| Right Stick Down  | `OnRightStickDownCanceled` |
| Start Button      | `OnButtonStartCanceled`  |
| Select Button     | `OnButtonSelectCanceled` |

### 4. Using the Gamepad Input Visualiser

A pre-configured scene, **`Scene-GamepadInputVisualiser`**, is included to display gamepad inputs in real-time. Open this scene and run the game to see visual feedback of gamepad inputs.

## Contributions

Feel free to fork, modify, and submit pull requests. Contributions and feedback are always welcome!

## License

This project is open-source and licensed under the MIT License.


