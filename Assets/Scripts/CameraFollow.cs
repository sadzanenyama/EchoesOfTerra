using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followTime;

    private Vector3 velocity = Vector3.zero;

    public float yOffset = 60f;
    public float scrollSpeed = 10f;
    public float maxYOffset = 80f;
    public float minYOffset = 40f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, followTime);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        yOffset -= scrollInput * scrollSpeed;
        yOffset = Mathf.Clamp(yOffset, minYOffset, maxYOffset);

        offset = new Vector3(offset.x, yOffset, offset.z);
    }
}
