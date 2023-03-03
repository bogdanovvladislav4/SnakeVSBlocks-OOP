using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
 
    public Transform SnakeHead;
    public float bodyElementDiameter;

    private List<Transform> snakeBody = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();
    // Start is called before the first frame update
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        positions.Add(SnakeHead.position);
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = (SnakeHead.position - positions[0]).magnitude;

        if(distance > bodyElementDiameter)
        {
            Vector3 direction = ((Vector3)SnakeHead.position - positions[0]).normalized;
            positions.Insert(0, positions[0] + direction * bodyElementDiameter);
            positions.RemoveAt(positions.Count - 1);
            distance -= bodyElementDiameter;
        }

        for(int i = 0; i < snakeBody.Count; i++)
        {
            snakeBody[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / bodyElementDiameter);
        }
    }

    public void AddBodyElement()
    {
        Transform elementBody = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeBody.Add(elementBody);
        positions.Add(elementBody.position);
    }

    public void RemoveElementBody()
    {
        Destroy(snakeBody[0].gameObject);
        snakeBody.RemoveAt(0);
        positions.RemoveAt(1);
    }
}
