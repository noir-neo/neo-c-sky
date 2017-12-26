﻿using CameraSubjectSize;
using ModelViewer.Interface;
using UnityEngine;

namespace ModelViewer.Behaviour
{
    public sealed class CameraZoomOrthographicSizeWithSubjectSizeFixer : MonoBehaviour, IPinchBehaviour
    {
        [SerializeField] private CameraSubjectSizeFixer _camera;
        [SerializeField] private float speed;
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize;

        void IPinchBehaviour.OnPinch(float magnitude)
        {
            float size = _camera.orthographicSize;
            size += magnitude * -speed;
            _camera.orthographicSize = Mathf.Clamp(size, minSize, maxSize);
        }
    }
}