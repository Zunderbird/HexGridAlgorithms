using UnityEngine;
using Assets.Scripts;

namespace Assets.HexGridAlgorithms
{
    public static class HexGenerator
    {
        public static Vector2 HexSize { get; private set; }
        public static Vector2 DistanceBetweenHex { get; set; }

        static HexGenerator()
        {
            HexSize = GetHexSize();
            DistanceBetweenHex = new Vector2(2f, 1.5f);
        }

        public static GameObject MakeHex()
        {
            var mesh = new Mesh();

            var vertices = new Vector3[7];

            //Center
            vertices[0] = Vector3.zero;
            vertices[1] = GetVertexPos(30);
            vertices[2] = GetVertexPos(90);
            vertices[3] = GetVertexPos(150);
            vertices[4] = GetVertexPos(210);
            vertices[5] = GetVertexPos(270);
            vertices[6] = GetVertexPos(330);

            //***********************************************************
            //										Build Triangles
            //***********************************************************
            var triangles = new int[]
            {
                0, 2, 1, //Top
                0, 3, 2, //Bottom
                0, 4, 3, //Bottom Left
                0, 5, 4, //Bottom Right
                0, 6, 5, //Top Right
                0, 1, 6  //Top Left
            };          

            //***********************************************************
            //										Set UV's
            //***********************************************************
            var uv = new Vector2[7];

            // Center
            uv[0] = new Vector2(0.5f, 0.5f);
            uv[1] = new Vector2(1f, 0.5f); // Top Left
            uv[2] = new Vector2(0.75f, 0.935f); // Top Right
            uv[3] = new Vector2(0.25f, 0.935f); // Bottom Right
            uv[4] = new Vector2(0, 0.5f); // Bottom Left
            uv[5] = new Vector2(0.25f, 0.065f);  // Center Bottom Left
            uv[6] = new Vector2(0.75f, 0.065f); // Bottom Left

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
            mesh.RecalculateNormals();

            var hex = new GameObject("New Hex");

            hex.AddComponent<MeshRenderer>();
            hex.AddComponent<MeshFilter>();
            hex.GetComponent<MeshFilter>().mesh = mesh;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            hex.AddComponent<MeshCollider>();

            var material = Object.Instantiate((Material)Resources.Load("Hex Material"));

            hex.GetComponent<Renderer>().material = material;
            hex.AddComponent<HexData>();

            return hex;
        }

        public static void SetHexInfo(int x, int y, Transform hex)
        {
            var newX = Mathf.CeilToInt(x - (y / 2));
            var newZ = -(newX + y);

            hex.GetComponent<HexData>().HexPosition = new Vector3(newX, y, newZ);
            hex.name = ("(" + newX + ", " + y + ", " + newZ + ")");
        }

        public static Vector3 CorrelateCoordWithMap(Vector3D hexCoord)
        {
            return CorrelateCoordWithMap(hexCoord, new Vector3(0, 0));
        }

        public static Vector3 CorrelateCoordWithMap(Vector3D hexCoord, Vector3 previousCoord)
        {
            var posY = hexCoord.Y * (HexSize.y * DistanceBetweenHex.y);
            var posX = hexCoord.X * (HexSize.x * DistanceBetweenHex.x);

            posX += (hexCoord.Y % 2 == 0) ? HexSize.x : 0;
            return new Vector3(posX, posY) + previousCoord;
        }

        private static Vector3 GetVertexPos(float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            var newPos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
            return newPos;
        }

        private static Vector2 GetHexSize()
        {
            var temp = MakeHex();

            var bounds = temp.GetComponent<Renderer>().bounds;
            var extent = new Vector2(bounds.extents.x, bounds.extents.y);

            Object.Destroy(temp);
            return extent;
        }
    }
}

