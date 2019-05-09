using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Variables
    [Header("Camera Follow")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;

    [Header("Camera Zoom")]
    [SerializeField]
    private float currentZoom = 8f;
    [SerializeField]
    private float zoomSpeed = 5f;
    [SerializeField]
    private float minZoom = 3f;
    [SerializeField]
    private float maxZoom = 12f;
    [SerializeField]
    private float pitch = 0f;

    [Header("Camera Rotation")]
    [SerializeField]
    private float rotationSpeed = 100f;
    private float rotationInput = 0f;


    // Update is called once per frame
    private void Update() {
        // Camera zoom method that can be adjusted with mouse scrollwheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Camera rotation method that can be adjusted with A and D keys
        rotationInput -= Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    }


    // LateUpdate is called after Update method
    // Tell Camera to follow/focus on the player
    private void LateUpdate() {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, rotationInput);
    }
}