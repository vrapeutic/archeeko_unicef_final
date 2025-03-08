using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ExampleListener : MonoBehaviour
{
    public ButtonHandler primaryAxisClickHandler = null;
    public ButtonHandler SecondaryAxisClickHandler = null;
    public ButtonHandler GripAxisClickHandler = null;
    public ButtonHandler Trigger = null;
    //public GameObject Cube;
    public void OnEnable()
    {
        //example to run the function here
        primaryAxisClickHandler.OnButtonDown += PrintPrimaryButtonDown;
        SecondaryAxisClickHandler.OnButtonDown += PrintPrimaryButtonDown;
    }

    public void OnDisable()
    {
        primaryAxisClickHandler.OnButtonDown -= PrintPrimaryButtonDown;
        SecondaryAxisClickHandler.OnButtonDown -= PrintPrimaryButtonDown;
    }

    private void PrintPrimaryButtonDown(XRController controller)
    {
        
        //Cube.SetActive(false);
    }

    private void PrintPrimaryButtonUp(XRController controller)
    {

    }

    private void PrintPrimaryAxis(XRController controller, Vector2 value)
    {

    }

    private void PrintTrigger(XRController controller, float value)
    {

    }
    public void viewCube()
    {
       // Cube.SetActive(true);
    }
    public void HideCube()
    {
        //Cube.SetActive(false);
    }
}
