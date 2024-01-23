using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshLinkJump : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float gravity = -9.81f;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());

            yield return StartCoroutine(JumpTo());
        }
    }

    public bool IsOnJump()
    {
        // 현재 오브젝트의 위치가 OffMeshLink에 있는지 (true / false)
        if (navAgent.isOnOffMeshLink)
        {
            // 현재 위치에 있는 OffMeshLink 데이터
            OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;

            // 설명 : navMeshAgent.currentOffMeshLinkData.offMeshLink가
            // true이면 수동으로 생성한 OffMeshLink
            // false이면 자동으로 생성한 OffMeshLink

            // 현재 위치에 있는 OffMeshLink가 수도응로 생성한 OffMeshLink이고, 장소가 "Climb"이면
            if (linkData.linkType == OffMeshLinkType.LinkTypeJumpAcross 
                || linkData.linkType == OffMeshLinkType.LinkTypeDropDown)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator JumpTo()
    {
        navAgent.isStopped = true;

        OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currentTime = 0.0f;
        float percent = 0.0f;

        float v0 = (end - start).y - gravity;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime;

            Vector3 position = Vector3.Lerp(start, end, percent);
            position.y = start.y + (v0 * percent) + (gravity * percent * percent);
            transform.position = position;

            yield return null;
        }

        navAgent.CompleteOffMeshLink();
        navAgent.isStopped = false;
    }
}
