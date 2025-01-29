using System;
using Inputs;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.Controls.Player.Fire.performed += _ => Debug.Log("FIRE PRESSED");
    }
}