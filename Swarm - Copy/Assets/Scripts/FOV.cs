using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadious;
    [Range(0,360)]
    public float viewAngle;
    public string sendMessage;

    public LayerMask targetLayerMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargetsList = new List<Transform>();

    void DrawFOV()
    {
        //int stepCont = Mathf.RoundToInt(viewAngle * meshResolution);

    }

    private void Update()
    {
        TellRatFollow();
    }

    
    private void TellRatFollow()
    {
        for (int i = 0; i < visibleTargetsList.Count; i++)
        {
            visibleTargetsList[i].gameObject.SendMessage(sendMessage, true);
        }
    }
    private void Start()
    {
        //begin the co-routine that searches the cone of view
        StartCoroutine("DelaySearch",0.1f);
    }
    IEnumerator DelaySearch(float fWait)
    {
        while (true)
        {
            yield return new WaitForSeconds(fWait);
            FindVisible();
        }
    }

    void FindVisible()
    {
        //clear array of targets
        visibleTargetsList.Clear();
        //create array that stores targets and add all transforms that overlap with the sphere using the view radious and targetlayermask to only store all transforms within range and in the layer
        Collider[] visibleTargets = Physics.OverlapSphere(transform.position, viewRadious, targetLayerMask);
        for (int i = 0; i < visibleTargets.Length; i++)
        {
            Transform target = visibleTargets[i].transform;
            //create a vector from the array[i] to the player
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            // check the angle from object to player lies between the fov
            if (Vector3.Angle(transform.forward,dirToTarget) <viewAngle/2)
            {
                float dstToTarger = Vector3.Angle(transform.position, target.position);
                //fires a raycast from player to object with a length of between them stopping if it hits the obstacle mask
                if (!Physics.Raycast(transform.position,dirToTarget,dstToTarger,obstacleMask))
                {
                    visibleTargetsList.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)

    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
