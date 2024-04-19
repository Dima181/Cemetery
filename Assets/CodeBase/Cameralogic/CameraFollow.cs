using System.Collections;
using UnityEngine;

namespace Assets.CodeBase.Cameralogic
{
    public class CameraFollow : MonoBehaviour
    {
        public float RotationAngleX;
        public float Distanse;
        public float OffsetY;
        
        [SerializeField]
        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX, 180, 0);

            Vector3 position = rotation * new Vector3(0, 0, -Distanse) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position; 
        }

        public void Follow(Transform following) => 
            _following = following;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += OffsetY;

            return followingPosition;
        }
    }
}