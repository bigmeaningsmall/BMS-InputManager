using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputHandler inputHandler; // Reference to the InputHandler script

    #region Cached Button Delegates
    private Action _onButtonSouthPressed,      _onButtonSouthCanceled;
    private Action _onButtonNorthPressed,      _onButtonNorthCanceled;
    private Action _onButtonEastPressed,       _onButtonEastCanceled;
    private Action _onButtonWestPressed,       _onButtonWestCanceled;
    private Action _onLeftShoulderPressed,     _onLeftShoulderCanceled;
    private Action _onRightShoulderPressed,    _onRightShoulderCanceled;
    private Action _onLeftStickPressPressed,   _onLeftStickPressCanceled;
    private Action _onRightStickPressPressed,  _onRightStickPressCanceled;
    private Action _onPadLeftPressed,          _onPadLeftCanceled;
    private Action _onPadRightPressed,         _onPadRightCanceled;
    private Action _onPadUpPressed,            _onPadUpCanceled;
    private Action _onPadDownPressed,          _onPadDownCanceled;
    private Action _onLeftStickLeftPressed,    _onLeftStickLeftCanceled;
    private Action _onLeftStickRightPressed,   _onLeftStickRightCanceled;
    private Action _onLeftStickUpPressed,      _onLeftStickUpCanceled;
    private Action _onLeftStickDownPressed,    _onLeftStickDownCanceled;
    private Action _onRightStickLeftPressed,   _onRightStickLeftCanceled;
    private Action _onRightStickRightPressed,  _onRightStickRightCanceled;
    private Action _onRightStickUpPressed,     _onRightStickUpCanceled;
    private Action _onRightStickDownPressed,   _onRightStickDownCanceled;
    private Action _onButtonStartPressed,      _onButtonStartCanceled;
    private Action _onButtonSelectPressed,     _onButtonSelectCanceled;
    private Action _onLeftTriggerPressed,      _onLeftTriggerReleased;
    private Action _onRightTriggerPressed,     _onRightTriggerReleased;
    #endregion

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
        
        // this unscbscribe setup had to be added as events we're not unsubscribing so we would have a memory leak and event being duplicated when creating inputs
        
        // Cache and subscribe button delegates - Face Buttons
        inputHandler.OnButtonSouth         += _onButtonSouthPressed      = () => ButtonSouth.SetState(true); 
        inputHandler.OnButtonSouthCanceled += _onButtonSouthCanceled     = () => ButtonSouth.SetState(false);
        inputHandler.OnButtonNorth         += _onButtonNorthPressed      = () => ButtonNorth.SetState(true);
        inputHandler.OnButtonNorthCanceled += _onButtonNorthCanceled     = () => ButtonNorth.SetState(false);
        inputHandler.OnButtonEast          += _onButtonEastPressed       = () => ButtonEast.SetState(true);
        inputHandler.OnButtonEastCanceled  += _onButtonEastCanceled      = () => ButtonEast.SetState(false);
        inputHandler.OnButtonWest          += _onButtonWestPressed       = () => ButtonWest.SetState(true);
        inputHandler.OnButtonWestCanceled  += _onButtonWestCanceled      = () => ButtonWest.SetState(false);

        // Shoulders
        inputHandler.OnLeftShoulder          += _onLeftShoulderPressed    = () => LeftShoulder.SetState(true);
        inputHandler.OnLeftShoulderCanceled  += _onLeftShoulderCanceled   = () => LeftShoulder.SetState(false);
        inputHandler.OnRightShoulder         += _onRightShoulderPressed   = () => RightShoulder.SetState(true);
        inputHandler.OnRightShoulderCanceled += _onRightShoulderCanceled  = () => RightShoulder.SetState(false);

        // Stick Presses
        inputHandler.OnLeftStickPress          += _onLeftStickPressPressed   = () => LeftStickPress.SetState(true);
        inputHandler.OnLeftStickPressCanceled  += _onLeftStickPressCanceled  = () => LeftStickPress.SetState(false);
        inputHandler.OnRightStickPress         += _onRightStickPressPressed  = () => RightStickPress.SetState(true);
        inputHandler.OnRightStickPressCanceled += _onRightStickPressCanceled = () => RightStickPress.SetState(false);

        // D-Pad
        inputHandler.OnPadLeft        += _onPadLeftPressed   = () => PadLeft.SetState(true);
        inputHandler.OnPadLeftCanceled += _onPadLeftCanceled = () => PadLeft.SetState(false);
        inputHandler.OnPadRight        += _onPadRightPressed  = () => PadRight.SetState(true);
        inputHandler.OnPadRightCanceled += _onPadRightCanceled = () => PadRight.SetState(false);
        inputHandler.OnPadUp           += _onPadUpPressed    = () => PadUp.SetState(true);
        inputHandler.OnPadUpCanceled   += _onPadUpCanceled   = () => PadUp.SetState(false);
        inputHandler.OnPadDown         += _onPadDownPressed  = () => PadDown.SetState(true);
        inputHandler.OnPadDownCanceled += _onPadDownCanceled = () => PadDown.SetState(false);

        // Left Stick Directions
        inputHandler.OnLeftStickLeft         += _onLeftStickLeftPressed    = () => LeftStickLeft.SetState(true);
        inputHandler.OnLeftStickLeftCanceled += _onLeftStickLeftCanceled   = () => LeftStickLeft.SetState(false);
        inputHandler.OnLeftStickRight         += _onLeftStickRightPressed  = () => LeftStickRight.SetState(true);
        inputHandler.OnLeftStickRightCanceled += _onLeftStickRightCanceled = () => LeftStickRight.SetState(false);
        inputHandler.OnLeftStickUp            += _onLeftStickUpPressed     = () => LeftStickUp.SetState(true);
        inputHandler.OnLeftStickUpCanceled    += _onLeftStickUpCanceled    = () => LeftStickUp.SetState(false);
        inputHandler.OnLeftStickDown          += _onLeftStickDownPressed   = () => LeftStickDown.SetState(true);
        inputHandler.OnLeftStickDownCanceled  += _onLeftStickDownCanceled  = () => LeftStickDown.SetState(false);

        // Right Stick Directions
        inputHandler.OnRightStickLeft          += _onRightStickLeftPressed   = () => RightStickLeft.SetState(true);
        inputHandler.OnRightStickLeftCanceled  += _onRightStickLeftCanceled  = () => RightStickLeft.SetState(false);
        inputHandler.OnRightStickRight         += _onRightStickRightPressed  = () => RightStickRight.SetState(true);
        inputHandler.OnRightStickRightCanceled += _onRightStickRightCanceled = () => RightStickRight.SetState(false);
        inputHandler.OnRightStickUp            += _onRightStickUpPressed     = () => RightStickUp.SetState(true);
        inputHandler.OnRightStickUpCanceled    += _onRightStickUpCanceled    = () => RightStickUp.SetState(false);
        inputHandler.OnRightStickDown          += _onRightStickDownPressed   = () => RightStickDown.SetState(true);
        inputHandler.OnRightStickDownCanceled  += _onRightStickDownCanceled  = () => RightStickDown.SetState(false);

        // Start / Select
        inputHandler.OnButtonStart          += _onButtonStartPressed   = () => ButtonStart.SetState(true);
        inputHandler.OnButtonStartCanceled  += _onButtonStartCanceled  = () => ButtonStart.SetState(false);
        inputHandler.OnButtonSelect         += _onButtonSelectPressed  = () => ButtonSelect.SetState(true);
        inputHandler.OnButtonSelectCanceled += _onButtonSelectCanceled = () => ButtonSelect.SetState(false);

        // Trigger Presses
        inputHandler.OnLeftTriggerPressed  += _onLeftTriggerPressed  = () => LeftTriggerPressed.SetState(true);
        inputHandler.OnLeftTriggerReleased += _onLeftTriggerReleased = () => LeftTriggerPressed.SetState(false);
        inputHandler.OnRightTriggerPressed  += _onRightTriggerPressed  = () => RightTriggerPressed.SetState(true);
        inputHandler.OnRightTriggerReleased += _onRightTriggerReleased = () => RightTriggerPressed.SetState(false);
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
        
        // Unsubscribe button delegates - Face Buttons
        inputHandler.OnButtonSouth         -= _onButtonSouthPressed;
        inputHandler.OnButtonSouthCanceled -= _onButtonSouthCanceled;
        inputHandler.OnButtonNorth         -= _onButtonNorthPressed;
        inputHandler.OnButtonNorthCanceled -= _onButtonNorthCanceled;
        inputHandler.OnButtonEast          -= _onButtonEastPressed;
        inputHandler.OnButtonEastCanceled  -= _onButtonEastCanceled;
        inputHandler.OnButtonWest          -= _onButtonWestPressed;
        inputHandler.OnButtonWestCanceled  -= _onButtonWestCanceled;

        // Shoulders
        inputHandler.OnLeftShoulder          -= _onLeftShoulderPressed;
        inputHandler.OnLeftShoulderCanceled  -= _onLeftShoulderCanceled;
        inputHandler.OnRightShoulder         -= _onRightShoulderPressed;
        inputHandler.OnRightShoulderCanceled -= _onRightShoulderCanceled;

        // Stick Presses
        inputHandler.OnLeftStickPress          -= _onLeftStickPressPressed;
        inputHandler.OnLeftStickPressCanceled  -= _onLeftStickPressCanceled;
        inputHandler.OnRightStickPress         -= _onRightStickPressPressed;
        inputHandler.OnRightStickPressCanceled -= _onRightStickPressCanceled;

        // D-Pad
        inputHandler.OnPadLeft         -= _onPadLeftPressed;
        inputHandler.OnPadLeftCanceled -= _onPadLeftCanceled;
        inputHandler.OnPadRight         -= _onPadRightPressed;
        inputHandler.OnPadRightCanceled -= _onPadRightCanceled;
        inputHandler.OnPadUp            -= _onPadUpPressed;
        inputHandler.OnPadUpCanceled    -= _onPadUpCanceled;
        inputHandler.OnPadDown          -= _onPadDownPressed;
        inputHandler.OnPadDownCanceled  -= _onPadDownCanceled;

        // Left Stick Directions
        inputHandler.OnLeftStickLeft          -= _onLeftStickLeftPressed;
        inputHandler.OnLeftStickLeftCanceled  -= _onLeftStickLeftCanceled;
        inputHandler.OnLeftStickRight         -= _onLeftStickRightPressed;
        inputHandler.OnLeftStickRightCanceled -= _onLeftStickRightCanceled;
        inputHandler.OnLeftStickUp            -= _onLeftStickUpPressed;
        inputHandler.OnLeftStickUpCanceled    -= _onLeftStickUpCanceled;
        inputHandler.OnLeftStickDown          -= _onLeftStickDownPressed;
        inputHandler.OnLeftStickDownCanceled  -= _onLeftStickDownCanceled;

        // Right Stick Directions
        inputHandler.OnRightStickLeft          -= _onRightStickLeftPressed;
        inputHandler.OnRightStickLeftCanceled  -= _onRightStickLeftCanceled;
        inputHandler.OnRightStickRight         -= _onRightStickRightPressed;
        inputHandler.OnRightStickRightCanceled -= _onRightStickRightCanceled;
        inputHandler.OnRightStickUp            -= _onRightStickUpPressed;
        inputHandler.OnRightStickUpCanceled    -= _onRightStickUpCanceled;
        inputHandler.OnRightStickDown          -= _onRightStickDownPressed;
        inputHandler.OnRightStickDownCanceled  -= _onRightStickDownCanceled;

        // Start / Select
        inputHandler.OnButtonStart          -= _onButtonStartPressed;
        inputHandler.OnButtonStartCanceled  -= _onButtonStartCanceled;
        inputHandler.OnButtonSelect         -= _onButtonSelectPressed;
        inputHandler.OnButtonSelectCanceled -= _onButtonSelectCanceled;

        // Trigger Presses
        inputHandler.OnLeftTriggerPressed  -= _onLeftTriggerPressed;
        inputHandler.OnLeftTriggerReleased -= _onLeftTriggerReleased;
        inputHandler.OnRightTriggerPressed  -= _onRightTriggerPressed;
        inputHandler.OnRightTriggerReleased -= _onRightTriggerReleased;
        
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