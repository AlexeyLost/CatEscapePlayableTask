using UnityEngine;

namespace GAME.Scripts.Guards
{
    public class GuardPlayerDetectorView : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [field: SerializeField] public int SegmentsCount { get; private set; }
        [field: SerializeField] public float ViewAngle { get; private set; }
        [field: SerializeField] public float ViewRadius { get; private set; }

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}