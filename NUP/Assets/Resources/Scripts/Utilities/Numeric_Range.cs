//======== Neverway 2022 Project Script | Written by Arthur Aka Liz ===========
// 
// Purpose: 
//          Contain a float that's clamped between a min & max and can be added
//          and subtracted within that range
// Applied to: 
// Notes: 
//          AddValue and SubtractValue functions can be passed negative and
//          positive values respectively
//
//=============================================================================

using UnityEngine;

public class Numeric_Range : MonoBehaviour
{
    //=-----------------=
    // Public Variables
    //=-----------------=
    public float maximum; // Define an upper limit to the value value
    public float minimum; // Define a lower limit to the value value
    [Header("READ-ONLY")]
    public float current; // Store the current value value


    //=-----------------=
    // Private Variables
    //=-----------------=
    
    
    //=-----------------=
    // Reference Variables
    //=-----------------=


    //=-----------------=
    // Mono Functions
    //=-----------------=
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public Numeric_Range(float minimum = 0, float maximum = 100, float current = float.NaN)
    {
        // if (minimum < maximum) use this value if true : use this value if false
        // ? ends the if statement
        this.minimum = (minimum < maximum) ? minimum : maximum; // Clamp minimum value from being greater than the maximum
        this.maximum = (minimum < maximum) ? maximum : minimum; // Clamp maximum value from being less than the minimum
        this.current = (float.IsNaN(current)) ? this.maximum : current; // Assign maximum value as default current value if no current value is specified
    }

    // Add a user defined amount to the current value, and clamp the value between the upper and lower limits
    public static Numeric_Range operator +(Numeric_Range a, float _amount) // _amount is b (operator = a+b)
    {
        // Add the values
        var result = a.current + _amount;
        // Clamp current value from exceeding the upper limit
        if (result > a.maximum) result = a.maximum;
        // Clamp current value from exceeding the lower limit
        else if (result < a.minimum) result = a.minimum;
        // Return the result as the new current value
        return new Numeric_Range(a.minimum, a.maximum, result);
    }
    
    // Subtract a user defined amount to the current value, and clamp the value between the upper and lower limits
    public static Numeric_Range operator -(Numeric_Range a, float _amount) // _amount is b (operator = a+b)
    {
        // Subtract the values
        var result = a.current - _amount;
        // Clamp current value from exceeding the upper limit
        if (result > a.maximum) result = a.maximum;
        // Clamp current value from exceeding the lower limit
        else if (result < a.minimum) result = a.minimum;
        // Return the result as the new current value
        return new Numeric_Range(a.minimum, a.maximum, result);
    }
    
    // Add two numeric ranges together
    public static Numeric_Range operator +(Numeric_Range a, Numeric_Range b)
    {
        return a + b;
    }
}

