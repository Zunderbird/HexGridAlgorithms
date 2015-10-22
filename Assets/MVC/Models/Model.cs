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
        public delegate void TextEvent(TextEventArgs text);
        public event HexEvent HexCreated;
        public event EventHandler DeleteHexMap;
        public event EventHandler MapCreated;
        public event EventHandler TerrainTypeWasSet;
        public event HexEvent MapsCentreCoordsFound;
        public event EventHandler WidthCorrected;
        public event EventHandler HeightCorrected;
        public event EventHandler CreationMapsAdmissible;
        public event EventHandler CreationMapsInadmissible;
        public event EventHandler IlluminateTargetHex;
        public event EventHandler IlluminateCurrentHex;
        public event EventHandler SkipTargetHexIllumination;
        public event EventHandler SkipCurrentHexIllumination;
        public event HexEvent PaintHex;
        public event TextEvent UpdateDistance;

        private Vector3D ?_currentHex;
        private Vector3D ?_targetHex;

        private Dictionary<Vector3D, Hex> _hexMap;

        public void CreateHexMap()
        {
            _currentHex = null;
            _targetHex = null;

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
            MapWidthInHex = arg.ConvertToIntByLastSymbol(MAX_SIZE_NUMBER);
            if (WidthCorrected != null) WidthCorrected(this, EventArgs.Empty);

            CheckForMapCreationAvailable();
        }

        public void SetHeightInHex(string arg)
        {
            MapHeightInHex = arg.ConvertToIntByLastSymbol(MAX_SIZE_NUMBER);
            if (HeightCorrected != null) HeightCorrected(this, EventArgs.Empty);

            CheckForMapCreationAvailable();
        }

        public void SelectHex(Vector3D hexPosition)
        {
            if (_currentHex != hexPosition)
            {
                if (_currentHex != null)
                    if (SkipCurrentHexIllumination != null) SkipCurrentHexIllumination(this, EventArgs.Empty);
                if (IlluminateCurrentHex != null) IlluminateCurrentHex(this, EventArgs.Empty);

                ChangeTerrainType(hexPosition);

                _currentHex = hexPosition;
                
            }
        }

        public void HitHex(Vector3D hexPosition)
        {
            if (_targetHex != hexPosition)
            {
                if (_targetHex != null)
                    if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(this, EventArgs.Empty);

                _targetHex = hexPosition;
                if (IlluminateTargetHex != null) IlluminateTargetHex(this, EventArgs.Empty);

                if (_currentHex != null)
                {                   
                    if (UpdateDistance != null) UpdateDistance(new TextEventArgs(GenerateDistanceText()));
                }

            }
        }

        public void NoHittedHex()
        {
            if (_targetHex != null)
                if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(this, EventArgs.Empty);
            _targetHex = null;
        }

        public void ChangeTerrainType(Vector3D hexPosition)
        {
            if (_hexMap[hexPosition].Type != CurrentTerrainType)
            {
                if (PaintHex != null) PaintHex(new HexEventArgs(CurrentTerrainType));
                _hexMap[hexPosition].Type = CurrentTerrainType;
            }
        }

        private void CheckForMapCreationAvailable()
        {
            if (MapWidthInHex > 0 && MapHeightInHex > 0)
            {
                if (CreationMapsAdmissible != null) CreationMapsAdmissible(this, EventArgs.Empty);
            }
            else if (CreationMapsInadmissible != null) CreationMapsInadmissible(this, EventArgs.Empty);
        }

        private string GenerateDistanceText()
        {
            var moveDistance = HexAlgorithms.CalculateDistance(_currentHex.Value, _targetHex.Value);

            return "Current Hex : " + _currentHex + "\nTarget Hex : "
                + _targetHex + "\nDistance : " + moveDistance;
        }
    }
}
