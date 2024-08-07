using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam3D;
    public Camera cam2D;
    private bool _canMove;
    private bool _canClimb;
    private bool _canJump;
    private bool _canCrouch;

    private void Start()
    {
        cam2D.gameObject.SetActive(false);
        cam3D.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cam3D.gameObject.SetActive(false);
            cam2D.gameObject.SetActive(true);
            GetPlayerState();
            Player.Instance.DisableAction();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            cam2D.gameObject.SetActive(false);
            cam3D.gameObject.SetActive(true);
            SetPlayerStateBack();
        }
    }
    void GetPlayerState()
    {
        Player player = Player.Instance;
        _canClimb = player.canClimb;
        _canJump = player.canJump;
        _canCrouch = player.canCrouch;
        _canMove = player.canMove;
    }
    void SetPlayerStateBack()
    {
        Player player = Player.Instance;
        player.canClimb = _canClimb;
        player.canJump = _canJump;
        player.canCrouch = _canCrouch;
        player.canMove = _canMove;
    }
}
