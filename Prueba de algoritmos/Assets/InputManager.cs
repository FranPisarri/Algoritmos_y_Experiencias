using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else
        {
            Destroy(this);
        }
    }


    public Action<Vector2> MouseScroll;

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Input.mouseScrollDelta;
        if (dir != Vector2.zero) MouseScroll(dir);
    }
}
