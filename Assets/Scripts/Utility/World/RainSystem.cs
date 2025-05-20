using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : MonoBehaviour
{
    public Transform Rain;
    public Transform Player;

    private Vector3 offSet = new Vector3(0, 50, 0);
    public float followSpeed;

    private void Update()
    {
        FollowRain();
    }

    private void FollowRain()
    {
        Vector3 targetPos = Player.position + offSet;
        Rain.position = Vector3.Lerp(Rain.position, targetPos, followSpeed * Time.deltaTime);
    }
}
