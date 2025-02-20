
# Plant UML

https://www.plantuml.com/plantuml/umla/xLT1KnGz4BtpA-QuNtroyMXH5WWhQe5GhAldp4mpHBB9L99PeDJ_BjdTmPxjxUbGUiIJz7jvwUdiItSEGrG-TWkpLnaLGl7Hjbtye6njm1S_zeh7V_yNi0GRYwCgQcSFla8LdN_zjhYmPt0TPr5Njpju63vMjW83jI1tfPiRNW_ZM93otdKnEllP-NWZ4dAKPguZIeIGfJHL8KfuZiut2572nuf-RPoA_Gb-yRffmE_W3GcoO7z6l29cK4b-xqiF8MJmhi208aIoDLNaYLII_CPlN6Tgy1A3VY_TbOYn5Ah6k6_rfJaSEJj4bacYugMgKmAZeAYmsd4S5JNcxGWahZvnTtOKp3odGIruIgOQgGl7oTIRjo8XKsbGBvuY_ngPiZ4SMOyf74lALPotCaFMOeh7SBBwMchz8Y5B2GnKKMPiQIsThekZEtKBa_zmwC9EhIeDYPxei0d_siFpoWcKNTEWWMKmnMZqnmOldJD5dLOTMMLSi-870gvJnw2O7mslaZjppIH4hsrJB204rS2hzJuwh3COB7domMGp7Qs_OS19nosIq7Wo8S1wJ23nVZBQ1KpLzWBKUoUh8O19LIHjD_i9bU1Xt6FfGWeDgypjnu6RraI2QH6DpbiIIWPbSg72mzkQmUOj0z2jXhSYnTZDy7L54MuxT9DGeByNXWXvDp2xCJYPoGc1pCYi2HW26TPPK_2VCCwWCoqlGHd457mQ0cLe4bw68H7Ji60IuT5voqAususOIg3mssEJ2GII2cCwWSCaGqp8C_XqY2av0ctQfErEKwEVTMMel6vJjv_UgqLh8BiDzutBz0LSBTW2VVeUXA8cNVvnEWqdVzDeSZKbuuVvka4Pz336SOziXsKCCXkE9pu6ycS8EYEzp0ZvRVwbc_nzAw2EFtTB-4ScnwPQFChEMkZV1IaPNPUkVYXycU-PJ8C8ljengSpRMaKWnVZKBThyOZ3N5Yx3zqV1zyvNC8YsBkYebt3grLB7X_FEHDqQlNKlMMWKms9Ashy_JhthR0trCmijN23FhowEfT9klcGxXMDdZ0wfsTW8VlBqPr7r_sCNtEkuav_ETN1m1p_tzxTUbc-Ai6vq9OGYEexFjaHvFJ5Ovf9KAsx98TYwMvZV



---

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

### Scene Examples 
- `Scene-GamepadInputVisualiser` 
	- Display active inputs - Gamepad
- `Scene-InputExample-Gamepad&Keyboard`
	-  Debug inputs from Gamepad and Keyboard
- `Scene-LocalMultiplayer`
	- Example using 'PlayerInputManager' to instantiate multiple players or input devices
- `Scene-LocalMultiplayerSplitScreen`
	- Example using 'PlayerInputManager' to instantiate multiple players or input devices with split screen
- `Scene-SinglePlayer-AnyDevice` 
	- Example taking input from any device connected

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


# UML
![BMS-InputManager-UML](https://github.com/user-attachments/assets/fe42eced-0880-4b6b-a046-cf969ce0eba8)
