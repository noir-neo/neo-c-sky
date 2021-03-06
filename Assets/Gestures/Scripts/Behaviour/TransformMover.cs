﻿using Gestures.Interface;
using UnityEngine;

namespace Gestures.Behaviour
{
    public sealed class TransformMover : MonoBehaviour, IDragBehaviour
    {
        [SerializeField] Transform moveRoot;
        [SerializeField] private float speed;
        [SerializeField] private Vector4 movableRage;
        [SerializeField] private int pointerCount;

        public int PointerCount => pointerCount;

        void IDragBehaviour.OnDrag(Vector2 delta)
        {
            moveRoot.Move(delta * speed);
            moveRoot.position = moveRoot.position.Clamp(movableRage);
        }
    }
}
