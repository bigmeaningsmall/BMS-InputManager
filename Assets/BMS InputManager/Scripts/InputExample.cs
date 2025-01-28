using UnityEngine;

public class InputExample : MonoBehaviour
{
    public InputHandler inputHandler; // Reference to the inputHandler script
    bool buttonSouthPressed = false;
        
    #region inputHandler Events Subscription
    private void OnEnable()
    {
        // Subscribe to inputHandler events
        //inputHandler.OnLeftStick += LeftStick;
        inputHandler.OnButtonSouth += ButtonSouth;


        // Subscribe to canceled events
        //inputHandler.OnButtonSouthCanceled += ButtonSouth;

    }

    private void OnDisable()
    {
        // Unsubscribe from inputHandler events
        //inputHandler.OnLeftStick -= LeftStick;
        inputHandler.OnButtonSouth -= ButtonSouth;
        
        // Unsubscribe from canceled events
        //inputHandler.OnButtonSouthCanceled -= ButtonSouth;
    }
    
    private void ButtonSouth(bool pressed)
    {
        buttonSouthPressed = pressed;
        Debug.Log("Button South Pressed!!!  : " +pressed);
    }
    
    #endregion
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key is pressed " + Input.GetKeyDown(KeyCode.Space));
        }
        
        if(buttonSouthPressed)
        {
            Debug.Log("Button South is pressed");
        }
    }
}
