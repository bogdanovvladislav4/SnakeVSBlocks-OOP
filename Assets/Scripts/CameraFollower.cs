using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.z = Target.position.z;
        transformPosition.x = Target.position.x;
        transformPosition.y = Target.position.y + 20;
        transform.position = transformPosition;
    }
}
