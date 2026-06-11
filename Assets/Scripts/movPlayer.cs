using UnityEngine;
using UnityEngine.InputSystem;

public class movPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    private Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                input.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                input.x = 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                input.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                input.y = -1f;
        }

        if (Gamepad.current != null)
        {
            Vector2 gamepadValue = Gamepad.current.leftStick.ReadValue();
            if (gamepadValue.sqrMagnitude > 0.01f)
                input = gamepadValue;
        }

        movement = new Vector3(input.x, 0f, input.y).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
