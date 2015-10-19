using UnityEngine;
using Assets.HexGridAlgorithms.Scripts;

namespace Assets.HexGridAlgorithms
{
    public static class HexGenerator
    {
        public static Vector2 HexSize { get; private set; }

        static HexGenerator()
        {
            HexSize = GetHexSize();
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
            var triangles = new int[18];

            //Top
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;

            //Bottom
            triangles[3] = 0;
            triangles[4] = 3;
            triangles[5] = 2;

            //Bottom Left
            triangles[6] = 0;
            triangles[7] = 4;
            triangles[8] = 3;

            //Bottom Right
            triangles[9] = 0;
            triangles[10] = 5;
            triangles[11] = 4;

            //Top Right
            triangles[12] = 0;
            triangles[13] = 6;
            triangles[14] = 5;

            //Top Left
            triangles[15] = 0;
            triangles[16] = 1;
            triangles[17] = 6;

            //***********************************************************
            //										Set UV's
            //***********************************************************
            var uv = new Vector2[7];

            // Center
            uv[0] = new Vector2(.5f, .5f);
            uv[1] = new Vector2(1f, .5f); // Top Left
            uv[2] = new Vector2(.75f, .935f); // Top Right
            uv[3] = new Vector2(.25f, 0.935f); // Bottom Right
            uv[4] = new Vector2(0, 0.5f); // Bottom Left
            uv[5] = new Vector2(.25f, .065f);  // Center Bottom Left
            uv[6] = new Vector2(.75f, .065f); // Bottom Left

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

        public static void SetHexInfo(int x, int y, Transform hex, TerrainTypes terrainType)
        {
            var newX = Mathf.CeilToInt(x - (y / 2));
            var newZ = -(newX + y);

            hex.GetComponent<HexData>().HexPosition = new Vector3(newX, y, newZ);
            hex.name = ("(" + newX + ", " + y + ", " + newZ + ")");
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

