using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class ClippableObject : MonoBehaviour
{

    public void Start()
    {
        var sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;

        //Only should enable one keyword. If you want to enable any one of them, you actually need to disable the others. 
        //This may be a bug...
        sharedMaterial.DisableKeyword("CLIP_ONE");
        sharedMaterial.EnableKeyword("CLIP_TWO");
        sharedMaterial.DisableKeyword("CLIP_THREE");

        sharedMaterial.SetVector("_planePos1", plane1Position + transform.position);
        sharedMaterial.SetVector("_planeNorm1", Quaternion.Euler(plane1Rotation) * Vector3.up);

        sharedMaterial.SetVector("_planePos2", plane2Position + transform.position);
        sharedMaterial.SetVector("_planeNorm2", Quaternion.Euler(plane2Rotation) * Vector3.up);
    }

    //preview size for the planes. Shown when the object is selected.
    public float planePreviewSize = 5.0f;

    //Positions and rotations for the planes. The rotations will be converted into normals to be used by the shaders.
    public Vector3 plane1Position = Vector3.zero;
    public Vector3 plane1Rotation = new Vector3(0, 0, 0);

    public Vector3 plane2Position = Vector3.zero;
    public Vector3 plane2Rotation = new Vector3(0, 90, 90);

    //Only used for previewing a plane. Draws diagonals and edges of a limited flat plane.
    private void DrawPlane(Vector3 position, Vector3 euler)
    {
        var forward = Quaternion.Euler(euler) * Vector3.forward;
        var left = Quaternion.Euler(euler) * Vector3.left;

        var forwardLeft = position + forward * planePreviewSize * 0.5f + left * planePreviewSize * 0.5f;
        var forwardRight = forwardLeft - left * planePreviewSize;
        var backRight = forwardRight - forward * planePreviewSize;
        var backLeft = forwardLeft - forward * planePreviewSize;

        Gizmos.DrawLine(position, forwardLeft);
        Gizmos.DrawLine(position, forwardRight);
        Gizmos.DrawLine(position, backRight);
        Gizmos.DrawLine(position, backLeft);

        Gizmos.DrawLine(forwardLeft, forwardRight);
        Gizmos.DrawLine(forwardRight, backRight);
        Gizmos.DrawLine(backRight, backLeft);
        Gizmos.DrawLine(backLeft, forwardLeft);
    }

    private void OnDrawGizmosSelected()
    {
        DrawPlane(plane1Position + transform.position, plane1Rotation);
        DrawPlane(plane2Position + transform.position, plane2Rotation);
    }

    //Ideally the planes do not need to be updated every frame, but we'll just keep the logic here for simplicity purposes.
    public void Update()
    {

    }

    public void update()
    {
        plane1Position = new Vector3(0, 0, 0);
        plane2Position = new Vector3(0, planePreviewSize, 0);
    }
}