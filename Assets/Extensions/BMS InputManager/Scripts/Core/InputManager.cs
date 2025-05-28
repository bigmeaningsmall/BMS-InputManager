using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputHandler inputHandler; // Reference to the InputHandler script
    
    #region Action States for Buttons
    public InputActionState ButtonSouth { get; private set; } = new InputActionState();
    public InputActionState ButtonNorth { get; private set; } = new InputActionState();
    public InputActionState ButtonEast { get; private set; } = new InputActionState();
    public InputActionState ButtonWest { get; private set; } = new InputActionState();
    
    public InputActionState LeftShoulder { get; private set; } = new InputActionState();
    public InputActionState RightShoulder { get; private set; } = new InputActionState();
    
    public InputActionState LeftStickPress { get; private set; } = new InputActionState();
    public InputActionState RightStickPress { get; private set; } = new InputActionState();
    
    public InputActionState PadLeft { get; private set; } = new InputActionState();
    public InputActionState PadRight { get; private set; } = new InputActionState();
    public InputActionState PadUp { get; private set; } = new InputActionState();
    public InputActionState PadDown { get; private set; } = new InputActionState();
    
    public InputActionState LeftStickLeft { get; private set; } = new InputActionState();
    public InputActionState LeftStickRight { get; private set; } = new InputActionState();
    public InputActionState LeftStickUp { get; private set; } = new InputActionState();
    public InputActionState LeftStickDown { get; private set; } = new InputActionState();
    
    public InputActionState RightStickLeft { get; private set; } = new InputActionState();
    public InputActionState RightStickRight { get; private set; } = new InputActionState();
    public InputActionState RightStickUp { get; private set; } = new InputActionState();
    public InputActionState RightStickDown { get; private set; } = new InputActionState();
    
    public InputActionState ButtonStart { get; private set; } = new InputActionState();
    public InputActionState ButtonSelect { get; private set; } = new InputActionState();
    
    public InputActionState LeftTriggerPressed { get; private set; } = new InputActionState();
    public InputActionState RightTriggerPressed { get; private set; } = new InputActionState();
    #endregion

    #region Analog Input Properties
    private Vector2 leftStickInput;
    public Vector2 LeftStickInput
    {
        get { return leftStickInput; }
        private set { leftStickInput = value; }
    }

    private Vector2 rightStickInput;
    public Vector2 RightStickInput
    {
        get { return rightStickInput; }
        private set { rightStickInput = value; }
    }
    
    private float leftTriggerInput;
    public float LeftTriggerInput
    {
        get { return leftTriggerInput; }
        private set { leftTriggerInput = value; }
    }
    
    private float rightTriggerInput;
    public float RightTriggerInput
    {
        get { return rightTriggerInput; }
        private set { rightTriggerInput = value; }
    }
    #endregion
    
    private void Awake()
    {
        // Auto-assign InputHandler if it's on the same GameObject
        if(gameObject.GetComponent<InputHandler>() && inputHandler == null)
        {
            inputHandler = gameObject.GetComponent<InputHandler>();
        }
    }
    
    private void OnEnable()
    {
        // Subscribe to analog input events
        inputHandler.OnLeftStick += HandleLeftStick;
        inputHandler.OnLeftStickCanceled += HandleLeftStickCanceled;
        inputHandler.OnRightStick += HandleRightStick;
        inputHandler.OnRightStickCanceled += HandleRightStickCanceled;
        inputHandler.OnLeftTrigger += HandleLeftTrigger;
        inputHandler.OnLeftTriggerCanceled += HandleLeftTriggerCanceled;
        inputHandler.OnRightTrigger += HandleRightTrigger;
        inputHandler.OnRightTriggerCanceled += HandleRightTriggerCanceled;
        
        // Subscribe to button events - Face Buttons
        inputHandler.OnButtonSouth += () => ButtonSouth.SetState(true);
        inputHandler.OnButtonSouthCanceled += () => ButtonSouth.SetState(false);
        inputHandler.OnButtonNorth += () => ButtonNorth.SetState(true);
        inputHandler.OnButtonNorthCanceled += () => ButtonNorth.SetState(false);
        inputHandler.OnButtonEast += () => ButtonEast.SetState(true);
        inputHandler.OnButtonEastCanceled += () => ButtonEast.SetState(false);
        inputHandler.OnButtonWest += () => ButtonWest.SetState(true);
        inputHandler.OnButtonWestCanceled += () => ButtonWest.SetState(false);
        
        // Subscribe to button events - Shoulders
        inputHandler.OnLeftShoulder += () => LeftShoulder.SetState(true);
        inputHandler.OnLeftShoulderCanceled += () => LeftShoulder.SetState(false);
        inputHandler.OnRightShoulder += () => RightShoulder.SetState(true);
        inputHandler.OnRightShoulderCanceled += () => RightShoulder.SetState(false);
        
        // Subscribe to button events - Stick Presses
        inputHandler.OnLeftStickPress += () => LeftStickPress.SetState(true);
        inputHandler.OnLeftStickPressCanceled += () => LeftStickPress.SetState(false);
        inputHandler.OnRightStickPress += () => RightStickPress.SetState(true);
        inputHandler.OnRightStickPressCanceled += () => RightStickPress.SetState(false);
        
        // Subscribe to button events - D-Pad
        inputHandler.OnPadLeft += () => PadLeft.SetState(true);
        inputHandler.OnPadLeftCanceled += () => PadLeft.SetState(false);
        inputHandler.OnPadRight += () => PadRight.SetState(true);
        inputHandler.OnPadRightCanceled += () => PadRight.SetState(false);
        inputHandler.OnPadUp += () => PadUp.SetState(true);
        inputHandler.OnPadUpCanceled += () => PadUp.SetState(false);
        inputHandler.OnPadDown += () => PadDown.SetState(true);
        inputHandler.OnPadDownCanceled += () => PadDown.SetState(false);
        
        // Subscribe to button events - Left Stick Directions
        inputHandler.OnLeftStickLeft += () => LeftStickLeft.SetState(true);
        inputHandler.OnLeftStickLeftCanceled += () => LeftStickLeft.SetState(false);
        inputHandler.OnLeftStickRight += () => LeftStickRight.SetState(true);
        inputHandler.OnLeftStickRightCanceled += () => LeftStickRight.SetState(false);
        inputHandler.OnLeftStickUp += () => LeftStickUp.SetState(true);
        inputHandler.OnLeftStickUpCanceled += () => LeftStickUp.SetState(false);
        inputHandler.OnLeftStickDown += () => LeftStickDown.SetState(true);
        inputHandler.OnLeftStickDownCanceled += () => LeftStickDown.SetState(false);
        
        // Subscribe to button events - Right Stick Directions
        inputHandler.OnRightStickLeft += () => RightStickLeft.SetState(true);
        inputHandler.OnRightStickLeftCanceled += () => RightStickLeft.SetState(false);
        inputHandler.OnRightStickRight += () => RightStickRight.SetState(true);
        inputHandler.OnRightStickRightCanceled += () => RightStickRight.SetState(false);
        inputHandler.OnRightStickUp += () => RightStickUp.SetState(true);
        inputHandler.OnRightStickUpCanceled += () => RightStickUp.SetState(false);
        inputHandler.OnRightStickDown += () => RightStickDown.SetState(true);
        inputHandler.OnRightStickDownCanceled += () => RightStickDown.SetState(false);
        
        // Subscribe to button events - Start/Select
        inputHandler.OnButtonStart += () => ButtonStart.SetState(true);
        inputHandler.OnButtonStartCanceled += () => ButtonStart.SetState(false);
        inputHandler.OnButtonSelect += () => ButtonSelect.SetState(true);
        inputHandler.OnButtonSelectCanceled += () => ButtonSelect.SetState(false);
        
        // Subscribe to button events - Trigger Presses
        inputHandler.OnLeftTriggerPressed += () => LeftTriggerPressed.SetState(true);
        inputHandler.OnLeftTriggerReleased += () => LeftTriggerPressed.SetState(false);
        inputHandler.OnRightTriggerPressed += () => RightTriggerPressed.SetState(true);
        inputHandler.OnRightTriggerReleased += () => RightTriggerPressed.SetState(false);
    }

    private void OnDisable()
    {
        // Unsubscribe from analog input events
        inputHandler.OnLeftStick -= HandleLeftStick;
        inputHandler.OnLeftStickCanceled -= HandleLeftStickCanceled;
        inputHandler.OnRightStick -= HandleRightStick;
        inputHandler.OnRightStickCanceled -= HandleRightStickCanceled;
        inputHandler.OnLeftTrigger -= HandleLeftTrigger;
        inputHandler.OnLeftTriggerCanceled -= HandleLeftTriggerCanceled;
        inputHandler.OnRightTrigger -= HandleRightTrigger;
        inputHandler.OnRightTriggerCanceled -= HandleRightTriggerCanceled;
        
        // Unsubscribe from button events - Face Buttons
        inputHandler.OnButtonSouth -= () => ButtonSouth.SetState(true);
        inputHandler.OnButtonSouthCanceled -= () => ButtonSouth.SetState(false);
        inputHandler.OnButtonNorth -= () => ButtonNorth.SetState(true);
        inputHandler.OnButtonNorthCanceled -= () => ButtonNorth.SetState(false);
        inputHandler.OnButtonEast -= () => ButtonEast.SetState(true);
        inputHandler.OnButtonEastCanceled -= () => ButtonEast.SetState(false);
        inputHandler.OnButtonWest -= () => ButtonWest.SetState(true);
        inputHandler.OnButtonWestCanceled -= () => ButtonWest.SetState(false);
        
        // Unsubscribe from button events - Shoulders
        inputHandler.OnLeftShoulder -= () => LeftShoulder.SetState(true);
        inputHandler.OnLeftShoulderCanceled -= () => LeftShoulder.SetState(false);
        inputHandler.OnRightShoulder -= () => RightShoulder.SetState(true);
        inputHandler.OnRightShoulderCanceled -= () => RightShoulder.SetState(false);
        
        // Unsubscribe from button events - Stick Presses
        inputHandler.OnLeftStickPress -= () => LeftStickPress.SetState(true);
        inputHandler.OnLeftStickPressCanceled -= () => LeftStickPress.SetState(false);
        inputHandler.OnRightStickPress -= () => RightStickPress.SetState(true);
        inputHandler.OnRightStickPressCanceled -= () => RightStickPress.SetState(false);
        
        // Unsubscribe from button events - D-Pad
        inputHandler.OnPadLeft -= () => PadLeft.SetState(true);
        inputHandler.OnPadLeftCanceled -= () => PadLeft.SetState(false);
        inputHandler.OnPadRight -= () => PadRight.SetState(true);
        inputHandler.OnPadRightCanceled -= () => PadRight.SetState(false);
        inputHandler.OnPadUp -= () => PadUp.SetState(true);
        inputHandler.OnPadUpCanceled -= () => PadUp.SetState(false);
        inputHandler.OnPadDown -= () => PadDown.SetState(true);
        inputHandler.OnPadDownCanceled -= () => PadDown.SetState(false);
        
        // Unsubscribe from button events - Left Stick Directions
        inputHandler.OnLeftStickLeft -= () => LeftStickLeft.SetState(true);
        inputHandler.OnLeftStickLeftCanceled -= () => LeftStickLeft.SetState(false);
        inputHandler.OnLeftStickRight -= () => LeftStickRight.SetState(true);
        inputHandler.OnLeftStickRightCanceled -= () => LeftStickRight.SetState(false);
        inputHandler.OnLeftStickUp -= () => LeftStickUp.SetState(true);
        inputHandler.OnLeftStickUpCanceled -= () => LeftStickUp.SetState(false);
        inputHandler.OnLeftStickDown -= () => LeftStickDown.SetState(true);
        inputHandler.OnLeftStickDownCanceled -= () => LeftStickDown.SetState(false);
        
        // Unsubscribe from button events - Right Stick Directions
        inputHandler.OnRightStickLeft -= () => RightStickLeft.SetState(true);
        inputHandler.OnRightStickLeftCanceled -= () => RightStickLeft.SetState(false);
        inputHandler.OnRightStickRight -= () => RightStickRight.SetState(true);
        inputHandler.OnRightStickRightCanceled -= () => RightStickRight.SetState(false);
        inputHandler.OnRightStickUp -= () => RightStickUp.SetState(true);
        inputHandler.OnRightStickUpCanceled -= () => RightStickUp.SetState(false);
        inputHandler.OnRightStickDown -= () => RightStickDown.SetState(true);
        inputHandler.OnRightStickDownCanceled -= () => RightStickDown.SetState(false);
        
        // Unsubscribe from button events - Start/Select
        inputHandler.OnButtonStart -= () => ButtonStart.SetState(true);
        inputHandler.OnButtonStartCanceled -= () => ButtonStart.SetState(false);
        inputHandler.OnButtonSelect -= () => ButtonSelect.SetState(true);
        inputHandler.OnButtonSelectCanceled -= () => ButtonSelect.SetState(false);
        
        // Unsubscribe from button events - Trigger Presses
        inputHandler.OnLeftTriggerPressed -= () => LeftTriggerPressed.SetState(true);
        inputHandler.OnLeftTriggerReleased -= () => LeftTriggerPressed.SetState(false);
        inputHandler.OnRightTriggerPressed -= () => RightTriggerPressed.SetState(true);
        inputHandler.OnRightTriggerReleased -= () => RightTriggerPressed.SetState(false);
        
        // Reset all action states when disabling
        ResetAllInputStates();
    }
    
    #region Analog Input Handlers
    private void HandleLeftStick(Vector2 input)
    {
        LeftStickInput = input;
    }
    
    private void HandleLeftStickCanceled()
    {
        LeftStickInput = Vector2.zero;
    }
    
    private void HandleRightStick(Vector2 input)
    {
        RightStickInput = input;
    }
    
    private void HandleRightStickCanceled()
    {
        RightStickInput = Vector2.zero;
    }
    
    private void HandleLeftTrigger(float input)
    {
        LeftTriggerInput = input;
    }
    
    private void HandleLeftTriggerCanceled()
    {
        LeftTriggerInput = 0f;
    }
    
    private void HandleRightTrigger(float input)
    {
        RightTriggerInput = input;
    }
    
    private void HandleRightTriggerCanceled()
    {
        RightTriggerInput = 0f;
    }
    #endregion
    
    #region Utility Methods
    /// <summary>
    /// Reset all input action states. Useful when changing scenes or disabling input.
    /// </summary>
    public void ResetAllInputStates()
    {
        ButtonSouth.Reset();
        ButtonNorth.Reset();
        ButtonEast.Reset();
        ButtonWest.Reset();
        LeftShoulder.Reset();
        RightShoulder.Reset();
        LeftStickPress.Reset();
        RightStickPress.Reset();
        PadLeft.Reset();
        PadRight.Reset();
        PadUp.Reset();
        PadDown.Reset();
        LeftStickLeft.Reset();
        LeftStickRight.Reset();
        LeftStickUp.Reset();
        LeftStickDown.Reset();
        RightStickLeft.Reset();
        RightStickRight.Reset();
        RightStickUp.Reset();
        RightStickDown.Reset();
        ButtonStart.Reset();
        ButtonSelect.Reset();
        LeftTriggerPressed.Reset();
        RightTriggerPressed.Reset();
        
        // Reset analog inputs
        LeftStickInput = Vector2.zero;
        RightStickInput = Vector2.zero;
        LeftTriggerInput = 0f;
        RightTriggerInput = 0f;
    }
    #endregion
}