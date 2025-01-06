using UnityEngine;

public class InputDebugger : MonoBehaviour
{
    [SerializeField] private bool debugAnalogInputs = true; // Toggle for analog input debug messages
    [SerializeField] private bool debugButtonInputs = true; // Toggle for button input debug messages

    #region InputHandler Events Subscription
    private void OnEnable()
    {
        // Subscribe to InputHandler events
        InputHandler.OnLeftStick += LeftStick;
        InputHandler.OnRightStick += RightStick;
        InputHandler.OnButtonSouth += ButtonSouth;
        InputHandler.OnButtonWest += ButtonWest;
        InputHandler.OnButtonNorth += ButtonNorth;
        InputHandler.OnButtonEast += ButtonEast;
        InputHandler.OnLeftTrigger += LeftTrigger;
        InputHandler.OnRightTrigger += RightTrigger;
        InputHandler.OnLeftTriggerPressed += LeftTriggerPress;
        InputHandler.OnRightTriggerPressed += RightTriggerPress;
        InputHandler.OnLeftShoulder += LeftShoulder;
        InputHandler.OnRightShoulder += RightShoulder;
        InputHandler.OnLeftStickPress += LeftStickPress;
        InputHandler.OnRightStickPress += RightStickPress;
        InputHandler.OnPadLeft += PadLeft;
        InputHandler.OnPadRight += PadRight;
        InputHandler.OnPadUp += PadUp;
        InputHandler.OnPadDown += PadDown;
        InputHandler.OnLeftStickLeft += LeftStickLeft;
        InputHandler.OnLeftStickRight += LeftStickRight;
        InputHandler.OnLeftStickUp += LeftStickUp;
        InputHandler.OnLeftStickDown += LeftStickDown;
        InputHandler.OnRightStickLeft += RightStickLeft;
        InputHandler.OnRightStickRight += RightStickRight;
        InputHandler.OnRightStickUp += RightStickUp;
        InputHandler.OnRightStickDown += RightStickDown;
        InputHandler.OnButtonStart += ButtonStart;
        InputHandler.OnButtonSelect += ButtonSelect;

        // Subscribe to canceled events
        InputHandler.OnLeftStickCanceled += LeftStickCanceled;
        InputHandler.OnRightStickCanceled += RightStickCanceled;
        InputHandler.OnButtonSouthCanceled += ButtonSouthCanceled;
        InputHandler.OnButtonWestCanceled += ButtonWestCanceled;
        InputHandler.OnButtonNorthCanceled += ButtonNorthCanceled;
        InputHandler.OnButtonEastCanceled += ButtonEastCanceled;
        InputHandler.OnLeftTriggerCanceled += LeftTriggerCanceled;
        InputHandler.OnRightTriggerCanceled += RightTriggerCanceled;
        InputHandler.OnLeftTriggerReleased += LeftTriggerReleased;
        InputHandler.OnRightTriggerReleased += RightTriggerReleased;
        InputHandler.OnLeftShoulderCanceled += LeftShoulderCanceled;
        InputHandler.OnRightShoulderCanceled += RightShoulderCanceled;
        InputHandler.OnLeftStickPressCanceled += LeftStickPressCanceled;
        InputHandler.OnRightStickPressCanceled += RightStickPressCanceled;
        InputHandler.OnPadLeftCanceled += PadLeftCanceled;
        InputHandler.OnPadRightCanceled += PadRightCanceled;
        InputHandler.OnPadUpCanceled += PadUpCanceled;
        InputHandler.OnPadDownCanceled += PadDownCanceled;
        InputHandler.OnLeftStickLeftCanceled += LeftStickLeftCanceled;
        InputHandler.OnLeftStickRightCanceled += LeftStickRightCanceled;
        InputHandler.OnLeftStickUpCanceled += LeftStickUpCanceled;
        InputHandler.OnLeftStickDownCanceled += LeftStickDownCanceled;
        InputHandler.OnRightStickLeftCanceled += RightStickLeftCanceled;
        InputHandler.OnRightStickRightCanceled += RightStickRightCanceled;
        InputHandler.OnRightStickUpCanceled += RightStickUpCanceled;
        InputHandler.OnRightStickDownCanceled += RightStickDownCanceled;
        InputHandler.OnButtonStartCanceled += ButtonStartCanceled;
        InputHandler.OnButtonSelectCanceled += ButtonSelectCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe from InputHandler events
        InputHandler.OnLeftStick -= LeftStick;
        InputHandler.OnRightStick -= RightStick;
        InputHandler.OnButtonSouth -= ButtonSouth;
        InputHandler.OnButtonWest -= ButtonWest;
        InputHandler.OnButtonNorth -= ButtonNorth;
        InputHandler.OnButtonEast -= ButtonEast;
        InputHandler.OnLeftTrigger -= LeftTrigger;
        InputHandler.OnRightTrigger -= RightTrigger;
        InputHandler.OnLeftTriggerPressed -= LeftTriggerPress;
        InputHandler.OnRightTriggerPressed -= RightTriggerPress;
        InputHandler.OnLeftShoulder -= LeftShoulder;
        InputHandler.OnRightShoulder -= RightShoulder;
        InputHandler.OnLeftStickPress -= LeftStickPress;
        InputHandler.OnRightStickPress -= RightStickPress;
        InputHandler.OnPadLeft -= PadLeft;
        InputHandler.OnPadRight -= PadRight;
        InputHandler.OnPadUp -= PadUp;
        InputHandler.OnPadDown -= PadDown;
        InputHandler.OnLeftStickLeft -= LeftStickLeft;
        InputHandler.OnLeftStickRight -= LeftStickRight;
        InputHandler.OnLeftStickUp -= LeftStickUp;
        InputHandler.OnLeftStickDown -= LeftStickDown;
        InputHandler.OnRightStickLeft -= RightStickLeft;
        InputHandler.OnRightStickRight -= RightStickRight;
        InputHandler.OnRightStickUp -= RightStickUp;
        InputHandler.OnRightStickDown -= RightStickDown;
        InputHandler.OnButtonStart -= ButtonStart;
        InputHandler.OnButtonSelect -= ButtonSelect;

        // Unsubscribe from canceled events
        InputHandler.OnLeftStickCanceled -= LeftStickCanceled;
        InputHandler.OnRightStickCanceled -= RightStickCanceled;
        InputHandler.OnButtonSouthCanceled -= ButtonSouthCanceled;
        InputHandler.OnButtonWestCanceled -= ButtonWestCanceled;
        InputHandler.OnButtonNorthCanceled -= ButtonNorthCanceled;
        InputHandler.OnButtonEastCanceled -= ButtonEastCanceled;
        InputHandler.OnLeftTriggerCanceled -= LeftTriggerCanceled;
        InputHandler.OnRightTriggerCanceled -= RightTriggerCanceled;
        InputHandler.OnLeftTriggerReleased -= LeftTriggerReleased;
        InputHandler.OnRightTriggerReleased -= RightTriggerReleased;
        InputHandler.OnLeftShoulderCanceled -= LeftShoulderCanceled;
        InputHandler.OnRightShoulderCanceled -= RightShoulderCanceled;
        InputHandler.OnLeftStickPressCanceled -= LeftStickPressCanceled;
        InputHandler.OnRightStickPressCanceled -= RightStickPressCanceled;
        InputHandler.OnPadLeftCanceled -= PadLeftCanceled;
        InputHandler.OnPadRightCanceled -= PadRightCanceled;
        InputHandler.OnPadUpCanceled -= PadUpCanceled;
        InputHandler.OnPadDownCanceled -= PadDownCanceled;
        InputHandler.OnLeftStickLeftCanceled -= LeftStickLeftCanceled;
        InputHandler.OnLeftStickRightCanceled -= LeftStickRightCanceled;
        InputHandler.OnLeftStickUpCanceled -= LeftStickUpCanceled;
        InputHandler.OnLeftStickDownCanceled -= LeftStickDownCanceled;
        InputHandler.OnRightStickLeftCanceled -= RightStickLeftCanceled;
        InputHandler.OnRightStickRightCanceled -= RightStickRightCanceled;
        InputHandler.OnRightStickUpCanceled -= RightStickUpCanceled;
        InputHandler.OnRightStickDownCanceled -= RightStickDownCanceled;
        InputHandler.OnButtonStartCanceled -= ButtonStartCanceled;
        InputHandler.OnButtonSelectCanceled -= ButtonSelectCanceled;
    }
    #endregion

    private void DebugLog(string message, bool isAnalog)
    {
        if ((isAnalog && debugAnalogInputs) || (!isAnalog && debugButtonInputs))
        {
            Debug.Log(message);
        }
    }

    //Input event handlers
    private void LeftStick(Vector2 input)
    {
        DebugLog($"LeftStick with input: {input}", true);
    }

    private void RightStick(Vector2 input)
    {
        DebugLog($"RightStick with input: {input}", true);
    }

    private void ButtonSouth()
    {
        DebugLog("ButtonSouth pressed!", false);
    }

    private void ButtonWest()
    {
        DebugLog("ButtonWest pressed!", false);
    }

    private void ButtonNorth()
    {
        DebugLog("ButtonNorth pressed!", false);
    }

    private void ButtonEast()
    {
        DebugLog("ButtonEast pressed!", false);
    }

    private void LeftTrigger(float input)
    {
        DebugLog($"LeftTrigger with input: {input}", true);
    }

    private void RightTrigger(float input)
    {
        DebugLog($"RightTrigger with input: {input}", true);
    }
    
    private void LeftTriggerPress()
    {
        DebugLog("LeftTrigger pressed!", false);
    }
    
    private void RightTriggerPress()
    {
        DebugLog("RightTrigger pressed!", false);
    }

    private void LeftShoulder()
    {
        DebugLog("LeftShoulder pressed!", false);
    }

    private void RightShoulder()
    {
        DebugLog("RightShoulder pressed!", false);
    }

    private void LeftStickPress()
    {
        DebugLog("LeftStickPress pressed!", false);
    }

    private void RightStickPress()
    {
        DebugLog("RightStickPress pressed!", false);
    }

    private void PadLeft()
    {
        DebugLog("PadLeft pressed!", false);
    }

    private void PadRight()
    {
        DebugLog("PadRight pressed!", false);
    }

    private void PadUp()
    {
        DebugLog("PadUp pressed!", false);
    }

    private void PadDown()
    {
        DebugLog("PadDown pressed!", false);
    }

    private void LeftStickLeft()
    {
        DebugLog("LeftStickLeft pressed!", false);
    }

    private void LeftStickRight()
    {
        DebugLog("LeftStickRight pressed!", false);
    }

    private void LeftStickUp()
    {
        DebugLog("LeftStickUp pressed!", false);
    }

    private void LeftStickDown()
    {
        DebugLog("LeftStickDown pressed!", false);
    }

    private void RightStickLeft()
    {
        DebugLog("RightStickLeft pressed!", false);
    }

    private void RightStickRight()
    {
        DebugLog("RightStickRight pressed!", false);
    }

    private void RightStickUp()
    {
        DebugLog("RightStickUp pressed!", false);
    }

    private void RightStickDown()
    {
        DebugLog("RightStickDown pressed!", false);
    }

    private void ButtonStart()
    {
        DebugLog("ButtonStart pressed!", false);
    }

    private void ButtonSelect()
    {
        DebugLog("ButtonSelect pressed!", false);
    }

    // Canceled event handlers
    private void LeftStickCanceled()
    {
        DebugLog("LeftStick canceled!", true);
    }

    private void RightStickCanceled()
    {
        DebugLog("RightStick canceled!", true);
    }

    private void ButtonSouthCanceled()
    {
        DebugLog("ButtonSouth canceled!", false);
    }

    private void ButtonWestCanceled()
    {
        DebugLog("ButtonWest canceled!", false);
    }

    private void ButtonNorthCanceled()
    {
        DebugLog("ButtonNorth canceled!", false);
    }

    private void ButtonEastCanceled()
    {
        DebugLog("ButtonEast canceled!", false);
    }

    private void LeftTriggerCanceled()
    {
        DebugLog("LeftTrigger canceled!", false);
    }

    private void RightTriggerCanceled()
    {
        DebugLog("RightTrigger canceled!", false);
    }
    
    private void LeftTriggerReleased()
    {
        DebugLog("LeftTrigger released!", false);
    }
    
    private void RightTriggerReleased()
    {
        DebugLog("RightTrigger released!", false);
    }

    private void LeftShoulderCanceled()
    {
        DebugLog("LeftShoulder canceled!", false);
    }

    private void RightShoulderCanceled()
    {
        DebugLog("RightShoulder canceled!", false);
    }

    private void LeftStickPressCanceled()
    {
        DebugLog("LeftStickPress canceled!", false);
    }

    private void RightStickPressCanceled()
    {
        DebugLog("RightStickPress canceled!", false);
    }

    private void PadLeftCanceled()
    {
        DebugLog("PadLeft canceled!", false);
    }

    private void PadRightCanceled()
    {
        DebugLog("PadRight canceled!", false);
    }

    private void PadUpCanceled()
    {
        DebugLog("PadUp canceled!", false);
    }

    private void PadDownCanceled()
    {
        DebugLog("PadDown canceled!", false);
    }

    private void LeftStickLeftCanceled()
    {
        DebugLog("LeftStickLeft canceled!", false);
    }

    private void LeftStickRightCanceled()
    {
        DebugLog("LeftStickRight canceled!", false);
    }

    private void LeftStickUpCanceled()
    {
        DebugLog("LeftStickUp canceled!", false);
    }

    private void LeftStickDownCanceled()
    {
        DebugLog("LeftStickDown canceled!", false);
    }

    private void RightStickLeftCanceled()
    {
        DebugLog("RightStickLeft canceled!", false);
    }

    private void RightStickRightCanceled()
    {
        DebugLog("RightStickRight canceled!", false);
    }

    private void RightStickUpCanceled()
    {
        DebugLog("RightStickUp canceled!", false);
    }

    private void RightStickDownCanceled()
    {
        DebugLog("RightStickDown canceled!", false);
    }

    private void ButtonStartCanceled()
    {
        DebugLog("ButtonStart canceled!", false);
    }

    private void ButtonSelectCanceled()
    {
        DebugLog("ButtonSelect canceled!", false);
    }
}