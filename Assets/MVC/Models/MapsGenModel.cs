using System;
using System.Collections.Generic;
using System.IO;
using Assets.HexGridAlgorithms;
using LitJson;

namespace Assets.MVC.Models
{
    class MapsGenModel 
    {
        public int MapWidthInHex { get; set; }
        public int MapHeightInHex { get; set; }

        public const int MAX_SIZE_NUMBER = 300; 

        public TerrainTypes CurrentTerrainType { get; set; }
  
        public event EventHandler DeleteHexMap;
        public event EventHandler MapCreated;
        public event EventHandler TerrainTypeWasSet;
        public event EventHandler WidthCorrected;
        public event EventHandler HeightCorrected;
        public event EventHandler CreationMapsAdmissible;
        public event EventHandler CreationMapsInadmissible;
        public event EventHandler LoadingNextStage;

        public delegate void HexEvent(HexEventArgs e);
        public event HexEvent HexCreated;
        public event HexEvent MapsCentreCoordsFound;
        public event HexEvent IlluminateTargetHex;
        public event HexEvent IlluminateCurrentHex;
        public event HexEvent SkipTargetHexIllumination;
        public event HexEvent SkipCurrentHexIllumination;
        public event HexEvent PaintHex;

        public delegate void TextEvent(TextEventArgs text);
        public event TextEvent UpdateDistance;

        private HexCoord ?_currentHex;
        private HexCoord ?_targetHex;

        private Dictionary<HexCoord, Hex> _hexMap;

        public void CreateHexMap()
        {
            _currentHex = null;
            _targetHex = null;

            if (_hexMap != null && DeleteHexMap != null) DeleteHexMap(this, EventArgs.Empty);

            _hexMap = new Dictionary<HexCoord, Hex>();

            for (var x = 0; x < MapWidthInHex; x++)
            {
                for (var y = 0; y < MapHeightInHex; y++)
                {
                    var cubeCoord = new Point(x, y);
                    var hexCoord = cubeCoord.ToHexCoord();

                    _hexMap.Add(cubeCoord.ToHexCoord(), new Hex(CurrentTerrainType));

                    if (HexCreated != null) HexCreated(new HexEventArgs(cubeCoord, hexCoord, CurrentTerrainType));
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
            var centreCoord = new Point(MapWidthInHex/2, MapHeightInHex/2);
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

        public void SelectHex(HexCoord hexPosition)
        {
            if (_currentHex != hexPosition)
            {
                //if (_currentHex != null)
                //    if (SkipCurrentHexIllumination != null) SkipCurrentHexIllumination(new HexEventArgs(_currentHex.Value));

                _currentHex = hexPosition;

                //if (IlluminateCurrentHex != null) IlluminateCurrentHex(new HexEventArgs(_currentHex.Value));

                if (UpdateDistance != null) UpdateDistance(new TextEventArgs(GenerateDistanceText()));

                ChangeTerrainType(_currentHex.Value);       
            }
        }

        public void HitHex(HexCoord hexPosition)
        {
            if (_targetHex != hexPosition)
            {
                if (_targetHex != null)
                    if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(new HexEventArgs(_targetHex.Value));

                _targetHex = hexPosition;

                if (IlluminateTargetHex != null) IlluminateTargetHex(new HexEventArgs(_targetHex.Value));

                if (_currentHex != null)
                {                   
                    if (UpdateDistance != null) UpdateDistance(new TextEventArgs(GenerateDistanceText()));
                }

            }
        }

        public void SkipHittedHex()
        {
            if (_targetHex != null)
                if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(new HexEventArgs(_targetHex.Value));
            _targetHex = null;
        }

        public void SkipSelectedHex()
        {
            if (_currentHex != null)
                if (SkipCurrentHexIllumination != null) SkipCurrentHexIllumination(new HexEventArgs(_currentHex.Value));
            _currentHex = null;
        }

        public void ChangeTerrainType(HexCoord hexPosition)
        {
            if (_hexMap[hexPosition].Type != CurrentTerrainType)
            {
                if (PaintHex != null) PaintHex(new HexEventArgs(hexPosition, CurrentTerrainType));
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
            if (_currentHex != null && _targetHex != null)
            {
                var moveDistance = HexAlgorithms.CalculateDistance(_currentHex.Value, _targetHex.Value);

                return "Current Hex : " + _currentHex + "\nTarget Hex : "
                       + _targetHex + "\nDistance : " + moveDistance;
            }
            return "Current Hex :\nTarget Hex :\nDistance : ";
        }

        public void SaveMap()
        {
            using (var sw = new StreamWriter(@"Assets/HexMap.json"))
            {
                foreach (var hex in _hexMap)
                {
                    sw.WriteLine(JsonMapper.ToJson(hex));
                }
            }
        }

        public void NextStage()
        {
            if (LoadingNextStage != null) LoadingNextStage(this, EventArgs.Empty);
        }
    }
}
