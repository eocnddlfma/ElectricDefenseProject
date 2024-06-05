using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputUtil : MonoSingleton<InputUtil>
    ,InputControl.IPlayerActions, InputControl.IUIActions
{
   private InputControl _inputControl;
   public bool PlayerEnabled
   {

      set
      {
         if (_inputControl is null)
            InitInputControl();

         if(value)
            _inputControl.Player.Enable();
         else
            _inputControl.Player.Disable();

      }
   }

   public bool UIEnabled
   {
      set
      {
         if (_inputControl is null)
            InitInputControl();

         if (value)
            _inputControl.UI.Enable();
         else
            _inputControl.UI.Disable();

      }
   }



   public static bool isClick;
   private static Vector2 mousePosition;
   public static Vector2 MousePosition
   {
      get => mousePosition;
      set
      {
         Mouse mouse = Mouse.current;
         mouse.WarpCursorPosition(value);
      }
   }
   public static Vector2 moveDirection;

   public Action<bool> OnClickEvent;
   public Action<Vector2> OnMouseMoveEvent;
   public Action<Vector2> OnMoveEvent;

   

   private void Awake()
   {
      InitInputControl();
      MousePosition = Vector2.zero;
   }

   private void InitInputControl()
   {
      _inputControl = new InputControl();
      _inputControl.UI.SetCallbacks(this);
      _inputControl.Player.SetCallbacks(this);

      UIEnabled = true;
      PlayerEnabled = true;
   }
   
   public void OnClick(InputAction.CallbackContext context)
   {
      if (context.performed)
      {
         isClick = true;
         OnClickEvent?.Invoke(true);
      }
      else if (context.canceled)
      {
         isClick = false;
         OnClickEvent?.Invoke(false);
      }

   }

   public void OnMouseMove(InputAction.CallbackContext context)
   {
      mousePosition = context.ReadValue<Vector2>();
      OnMouseMoveEvent?.Invoke(mousePosition);
   }

   public void OnMove(InputAction.CallbackContext context)
   {
      if (context.performed)
      {
         moveDirection = context.ReadValue<Vector2>();
         OnMoveEvent?.Invoke(mousePosition);
      }
      else
      {
         moveDirection = Vector3.zero;
      }
   }

}
