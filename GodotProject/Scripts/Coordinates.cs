using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public static class Coordinates
{
    public static Vector3 ToScreenSpace(Vector3 point, Vector2 cameraPosition)
    {
        // rotate into world space
        Vector3 originToPoint = point - Vector3.Zero;

        point = GetWorldSpaceToScreenSpaceMatrix().Inverse().Xform(originToPoint);

        // translate for the camera
        point.x -= cameraPosition.x;
        point.y -= cameraPosition.y;

        return point;
    }

    private static Basis GetWorldSpaceToScreenSpaceMatrix()
    {
        Basis rotXAxis = new Basis();
        rotXAxis[0, 0] = 1.0f;
        rotXAxis[0, 1] = 0.0f;
        rotXAxis[0, 2] = 0.0f;
        rotXAxis[1, 0] = 0.0f;
        rotXAxis[1, 1] = Mathf.Cos(Mathf.Deg2Rad(45.0f));
        rotXAxis[1, 2] = Mathf.Sin(Mathf.Deg2Rad(45.0f));
        rotXAxis[2, 0] = 0.0f;
        rotXAxis[2, 1] = -1.0f * Mathf.Sin(Mathf.Deg2Rad(45.0f));
        rotXAxis[2, 2] = Mathf.Cos(Mathf.Deg2Rad(45.0f));

        Basis rotZAxis = new Basis();
        rotZAxis[0, 0] = Mathf.Cos(Mathf.Deg2Rad(45.0f));
        rotZAxis[0, 1] = Mathf.Sin(Mathf.Deg2Rad(45.0f));
        rotZAxis[0, 2] = 0.0f;
        rotZAxis[1, 0] = -1.0f * Mathf.Sin(Mathf.Deg2Rad(45.0f));
        rotZAxis[1, 1] = Mathf.Cos(Mathf.Deg2Rad(45.0f));
        rotZAxis[1, 2] = 0.0f;
        rotZAxis[2, 0] = 0.0f;
        rotZAxis[2, 1] = 0.0f;
        rotZAxis[2, 2] = 1.0f;

        return rotZAxis * rotXAxis;
    }
}
