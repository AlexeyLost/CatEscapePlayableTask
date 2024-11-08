using System.Collections.Generic;
using UnityEngine;

namespace GAME.Scripts.Helpers
{
    /// <summary>
    /// Used just for round place guards in end card in editor.
    /// </summary>
    public class RoundPlaceObjectsComponent : MonoBehaviour
    {
        [SerializeField] private Transform _middlePointTransform;
        [SerializeField] private List<Transform> _transformsToPlace;
        [SerializeField] private float _radius = 5f;


        [ContextMenu("Place Objects")]
        public void PlaceObjects()
        {
            int count = _transformsToPlace.Count;
            float stepAngleSize = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = -360f / 2f + stepAngleSize * i;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 point = _middlePointTransform.position + direction * _radius;
                _transformsToPlace[i].position = point;
                _transformsToPlace[i].GetChild(0).transform.position = _middlePointTransform.position;
            }
        }
    }
}
