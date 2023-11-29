using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputController _inputController;
    public InputController.PlayerInteractionsActions PlayerInteractions { get; private set; }
    
    private void Awake()
    {
        _inputController = new InputController();
        
        PlayerInteractions = _inputController.PlayerInteractions;
    }
    private void OnEnable()
    {
        _inputController.Enable();
        PlayerInteractions.Enable();
    }
    private void OnDisable()
    {
        _inputController.Disable();
        PlayerInteractions.Disable();
    }
}
