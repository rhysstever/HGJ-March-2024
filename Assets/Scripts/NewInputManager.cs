using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{

    NewInputs inputs;
    Queue<int> inputQueue;
    const int MAX_QUEUE_SIZE = 10;

    // Start is called before the first frame update
    void Start()
    {
        inputs = new NewInputs();
        inputQueue = new Queue<int>(MAX_QUEUE_SIZE + 1);

        inputs.Player.DirectionalInput.performed += onDirection;
        inputs.Player.Enable();
    }

    private void onDirection(InputAction.CallbackContext context)
    {
        Debug.Log("Direction button pressed!");
        var readVec = context.ReadValue<Vector2>();
        if (readVec.x == -1)
        {
            if (readVec.y == -1)
            {
                inputQueue.Enqueue(1);
            } 
            else if (readVec.y == 0)
            {
                inputQueue.Enqueue(4);
            } 
            else if (readVec.y == 1)
            {
                inputQueue.Enqueue(7);
            }
        }
        else if (readVec.x == 0) 
        {
            if (readVec.y == -1)
            {
                inputQueue.Enqueue(2);
            } 
            else if (readVec.y == 1)
            {
                inputQueue.Enqueue(8);
            }
        } 
        else if (readVec.x == 1)
        {
            if (readVec.y == -1)
            {
                inputQueue.Enqueue(3);
            }
            else if (readVec.y == 0)
            {
                inputQueue.Enqueue(6);
            }
            else if (readVec.y == 1)
            {
                inputQueue.Enqueue(9);
            }
        } 
        else {
            Debug.Log("Directional Vector out of range! Vector read was {readVec}");
            return;
        }

        if (inputQueue.Count > MAX_QUEUE_SIZE)
        {
            inputQueue.Dequeue();
        }

        Debug.Log("Direction Pressed! Current input queue: " + string.Join(",", inputQueue.ToArray()));
        
    }
}
