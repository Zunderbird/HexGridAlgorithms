using System;
using System.Collections.Generic;
using Assets.HexGridAlgorithms;

namespace Assets.MVC.Models
{
    class Model
    {
        public int MapWidthInHex { get; set; }
        public int MapHeightInHex { get; set; }

        public const int MAX_SIZE_NUMBER = 300; 

        public TerrainTypes CurrentTerrainType { get; set; }

        public delegate void HexEvent(HexEventArgs e);
        public event HexEvent HexCreated;
        public event EventHandler DeleteHexMap;
        public event EventHandler MapCreated;
        public event EventHandler TerrainTypeWasSet;
        public event HexEvent MapsCentreCoordsFound;
        public event EventHandler WidthCorrected;
        public event EventHandler HeightCorrected;
        public event EventHandler CreationMapsAdmissible;
        public event EventHandler CreationMapsInadmissible;

        private Vector3D _currentHex;
        private Vector3D _targetHex;

        private Dictionary<Vector3D, Hex> _hexMap;

        public void CreateHexMap()
        {
            if (_hexMap != null && DeleteHexMap != null) DeleteHexMap(this, EventArgs.Empty);

            _hexMap = new Dictionary<Vector3D, Hex>();

            for (var x = 0; x < MapWidthInHex; x++)
            {
                for (var y = 0; y < MapHeightInHex; y++)
                {
                    var tempVector = new Vector3D(x, y, 0);

                    _hexMap.Add(tempVector.ToHexCoord(), new Hex(CurrentTerrainType));

                    if (HexCreated != null) HexCreated(new HexEventArgs(tempVector, CurrentTerrainType));
                }
            }
            if (_hexMap.Count > 0)
                if (MapCreated != null) MapCreated(this, EventArgs.Empty);
        }

        public void SetNextTerrainType()
        {
            if ((int)CurrentTerrainType + 1 >= Enum.GetNames(typeof(TerrainTypes)).Length)
                CurrentTerrainType = 0;
            else
            {
                CurrentTerrainType++;
            }
            if (TerrainTypeWasSet != null) TerrainTypeWasSet(this, EventArgs.Empty);;
        }

        public void FindMapsCentreCoords()
        {
            var centreCoord = new Vector3D(MapWidthInHex/2, MapHeightInHex/2, 0);
            if (MapsCentreCoordsFound != null) MapsCentreCoordsFound(new HexEventArgs(centreCoord));
        }

        public void SetWidthInHex(string arg)
        {
            MapWidthInHex = CorrectInputFieldLastSymbol(arg);
            if (WidthCorrected != null) WidthCorrected(this, EventArgs.Empty);

            CheckForMapCreationAvailable();
        }

        public void SetHeightInHex(string arg)
        {
            MapHeightInHex = CorrectInputFieldLastSymbol(arg);
            if (HeightCorrected != null) HeightCorrected(this, EventArgs.Empty);

            CheckForMapCreationAvailable();
        }

        private void CheckForMapCreationAvailable()
        {
            if (MapWidthInHex > 0 && MapHeightInHex > 0)
            {
                if (CreationMapsAdmissible != null) CreationMapsAdmissible(this, EventArgs.Empty);
            }
            else if (CreationMapsInadmissible != null) CreationMapsInadmissible(this, EventArgs.Empty);
        }

        private int CorrectInputFieldLastSymbol(string arg)
        {
            var number = 0;

            if (arg.Length > 0)
            {
                var lastSymbol = arg[arg.Length - 1];

                if (!lastSymbol.IsDigit() || Convert.ToInt32(arg) > MAX_SIZE_NUMBER)
                {
                    arg = arg.Remove(arg.Length - 1);
                }
                number = (arg.Length > 0) ? Convert.ToInt32(arg) : 0;
            }
            return number;
        }
    }
}
