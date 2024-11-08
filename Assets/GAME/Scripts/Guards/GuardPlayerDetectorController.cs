using System;
using System.Collections.Generic;
using GAME.Scripts.Player;
using UnityEngine;

namespace GAME.Scripts.Guards
{
    /// <summary>
    /// Controller for player detector. Here generates mesh based on cones,
    /// or other form if the raycast hit something, in that case points
    /// for mesh gets from rays and objects intersection.
    /// </summary>
    public class GuardPlayerDetectorController
    {
        public event Action<Vector3> PlayerDetected;
        
        private GuardPlayerDetectorView _view;
        private Mesh _viewMesh = new();
        private bool _detecting;
        private List<Vector3> _meshPoints = new();
        private bool _onceUpdatedMeshAfterLeftAnyObjects;
            
            
        public GuardPlayerDetectorController(GuardPlayerDetectorView view)
        {
            _view = view;
            _detecting = false;
            _onceUpdatedMeshAfterLeftAnyObjects = false;
        }

        public void Init()
        {
            _viewMesh = new();
            _view.SetMesh(_viewMesh);
            CheckFieldOfView(true);
        }

        public void StartDetection()
        {
            _detecting = true;
        }

        public void StopDetection()
        {
            _detecting = false;
            _view.TurnOff();
        }
        
        public void LateUpdate(float deltaTime)
        {
            if (!_detecting) return;
            
            CheckFieldOfView();
        }

        void CheckFieldOfView(bool forceUpdateMesh = false)
        {
            bool needUpdateMesh = false;
            bool playerDetected = false;
            Vector3 playerPosition = Vector3.zero;
            int stepCount = _view.SegmentsCount;
            float stepAngleSize = _view.ViewAngle / stepCount;
            _meshPoints.Clear();
            _meshPoints.Add(Vector3.zero);

            for (int i = 0; i <= stepCount; i++)
            {
                float angle = -_view.ViewAngle / 2 + stepAngleSize * i;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;

                RaycastHit hit;
                Vector3 endPoint;
                if (Physics.Raycast(_view.transform.position, _view.transform.rotation * direction, out hit, _view.ViewRadius))
                {
                    needUpdateMesh = true;
                    _onceUpdatedMeshAfterLeftAnyObjects = false;
                    endPoint = _view.transform.InverseTransformPoint(hit.point);
                    if (!playerDetected && hit.collider.TryGetComponent(out PlayerView playerView))
                    {
                        playerDetected = true;
                        playerPosition = playerView.transform.position;
                    }
                }
                else
                {
                    endPoint = direction * _view.ViewRadius;
                    
                    // for better performance we are not updating mesh every frame,
                    // just when ray hit something and one time after last hit
                    if (!needUpdateMesh && !_onceUpdatedMeshAfterLeftAnyObjects)
                    {
                        _onceUpdatedMeshAfterLeftAnyObjects = true;
                        needUpdateMesh = true;
                    }
                }
        
                _meshPoints.Add(endPoint);        
            }

            if (needUpdateMesh || forceUpdateMesh) UpdateMesh();
            
            if (playerDetected)
            {
                PlayerDetected?.Invoke(playerPosition);
            }
        }

        private void UpdateMesh()
        {
            int vertexCount = _meshPoints.Count;
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            for (int i = 0; i < vertexCount; i++)
            {
                vertices[i] = _meshPoints[i];
                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            _viewMesh.Clear();
            _viewMesh.vertices = vertices;
            _viewMesh.triangles = triangles;
            _viewMesh.RecalculateNormals();
        }
    }
}