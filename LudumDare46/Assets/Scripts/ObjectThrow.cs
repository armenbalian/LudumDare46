using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Grenade grenade;
    Rigidbody grenadeRigidBody;

    [SerializeField]
    Transform target;

    // maximum vertical displacement
    // must be higher than the target height
    [SerializeField]
    float h = 25; // 25 - default

    [SerializeField]
    float gravity = -18; // -18 - default

    [SerializeField]
    bool debugPath;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
            throw new System.Exception("ObjectThrow - Need the player transform !");

        if (grenade == null)
            throw new System.Exception("ObjectThrow - objectToThrow is null !");

        grenadeRigidBody = grenade.GetComponent<Rigidbody>();
        if (grenadeRigidBody == null)
            throw new System.Exception("ObjectThrow - objectToThrow need a RigidBody");

        if (target == null)
            throw new System.Exception("ObjectThrow - objectToThrow need a target");
    }

    // Update is called once per frame
    void Update()
    {
        if (!grenade.GetIsActive())
        {
            Vector3 newPos = playerTransform.position;
            newPos.x++;
            transform.position = newPos;
            grenade.transform.position = newPos;
        }

        if (debugPath)
        {
            DrawPath();
        }
    }

    public void Launch()
    {
        if (grenade.GetIsActive())
        {
            return;
        }

        grenade.SetActive(true);

        Physics.gravity = Vector3.up * gravity;
        grenadeRigidBody.velocity = CalculateLaunchData().initialVelocity;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - grenadeRigidBody.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - grenadeRigidBody.position.x, 0, target.position.z - grenadeRigidBody.position.z);

        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityYZ = displacementXZ / time;

        return new LaunchData(velocityYZ + velocityY * -Mathf.Sign(gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();

        Vector3 previousDrawPoint = grenadeRigidBody.position;

        int resolution = 30;
        for (int i = 0; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = grenadeRigidBody.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
