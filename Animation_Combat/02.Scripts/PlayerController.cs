using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode        jumpKeyCode = KeyCode.Space;
    [SerializeField]
    private Transform      cameraTransform;
    private Movement3D     movement3D;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        Cursor.visible      = false;                 // 마우스 커서를 보이지 않게
        Cursor.lockState    = CursorLockMode.Locked; // 마우스 커서 위치 고정

        movement3D = GetComponent<Movement3D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 애니메이션 파라미터 설정(horizontal, vertical)
        playerAnimator.OnMovement(x, z);
        // 이동 속도 설정 (앞으로 이동할때만 5, 나머지는 2)
        movement3D.MoveSpeed = z > 0 ? 5.0f : 2.0f;
        // 이동 함수 호출 (카메라가 보고있는 방향을 기준으로 방향키에 따라 이동)
        movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));

        // 회전 설정 (항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        if (Input.GetKeyDown(jumpKeyCode))
        {
            playerAnimator.OnJump();
            movement3D.JumpTo();
        }

        if (Input.GetMouseButtonDown(1))
        {
            playerAnimator.OnKickAttack();
        }

        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.OnWeaponAttack();
        }
    }
}
