using UnityEngine;

public class InputActionState : MonoBehaviour
{
    private bool wasPressed = false;
    private bool isPressed = false;
    private int lastFramePressed = -1;
    
    public void SetState(bool pressed)
    {
        // Only update the previous state at the start of a new frame
        if (Time.frameCount != lastFramePressed) {
            wasPressed = isPressed;
            lastFramePressed = Time.frameCount;
        }
        
        // Always update the current state
        isPressed = pressed;
    }
    
    // Returns true continuously while the button is held down
    public bool Held() => isPressed;
    
    // Returns true ONLY on the frame when button transitions from not pressed to pressed
    public bool Pressed() => isPressed && !wasPressed && Time.frameCount == lastFramePressed;
    
    // Returns true ONLY on the frame when button transitions from pressed to not pressed
    public bool Released() => !isPressed && wasPressed && Time.frameCount == lastFramePressed;
    
    // Reset the state (useful when enabling/disabling input)
    public void Reset()
    {
        wasPressed = false;
        isPressed = false;
        lastFramePressed = -1;
    }
}