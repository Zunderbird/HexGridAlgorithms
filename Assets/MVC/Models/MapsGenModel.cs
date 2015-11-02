using System;
using System.Collections.Generic;
using Assets.HexGridAlgorithms;
using Assets.MVC.HexAlgorithmsEventArgs;


namespace Assets.MVC.Models
{
    class MapsGenModel : BaseModel
    {
        public TerrainTypes CurrentTerrainType { get; set; }

        public event EventHandler TerrainTypeWasSet;
        public event EventHandler WidthCorrected;
        public event EventHandler HeightCorrected;
        public event EventHandler CreationMapsAdmissible;
        public event EventHandler CreationMapsInadmissible;
        public event EventHandler DeleteHexMap;
        public event EventHandler MapCreated;

        public delegate void HexEvent(object sender, HexEventArgs e);
        public event HexEvent HexCreated;

        public delegate void HexCoordEvent(object sender, HexCoordEventArgs e);
        public event HexCoordEvent IlluminateTargetHex;
        public event HexCoordEvent IlluminateCurrentHex;
        public event HexCoordEvent SkipTargetHexIllumination;
        public event HexCoordEvent SkipCurrentHexIllumination;

        public delegate void TerrainEvent(object sender, TerrainEventArgs e);
        public event TerrainEvent PaintHex;

        public delegate void TextEvent(TextEventArgs text);
        public event TextEvent UpdateDistance;

        public const int MAX_SIZE_NUMBER = 300;

        public void CreateHexMap()
        {
            CurrentHex = null;
            TargetHex = null;

            if (HexMap != null && DeleteHexMap != null) DeleteHexMap(this, EventArgs.Empty);

            HexMap = new Dictionary<HexCoord, Hex>();

            for (var x = 0; x < MapWidthInHex; x++)
            {
                for (var y = 0; y < MapHeightInHex; y++)
                {
                    var cubeCoord = new Point(x, y);
                    var hexCoord = cubeCoord.ToHexCoord();

                    HexMap.Add(cubeCoord.ToHexCoord(), new Hex(CurrentTerrainType));

                    if (HexCreated != null) HexCreated(this, new HexEventArgs(cubeCoord, hexCoord, CurrentTerrainType));
                }
            }
            if (HexMap.Count > 0)
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
            if (CurrentHex != hexPosition)
            {
                //if (CurrentHex != null)
                //    if (SkipCurrentHexIllumination != null) SkipCurrentHexIllumination(new HexCoordEventArgs(CurrentHex.Value));

                CurrentHex = hexPosition;

                //if (IlluminateCurrentHex != null) IlluminateCurrentHex(new HexCoordEventArgs(CurrentHex.Value));

                if (UpdateDistance != null) UpdateDistance(new TextEventArgs(GenerateDistanceText()));

                ChangeTerrainType(CurrentHex.Value);       
            }
        }

        public void HitHex(HexCoord hexPosition)
        {
            if (TargetHex != hexPosition)
            {
                if (TargetHex != null)
                    if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(this, new HexCoordEventArgs(TargetHex.Value));

                TargetHex = hexPosition;

                if (IlluminateTargetHex != null) IlluminateTargetHex(this, new HexCoordEventArgs(TargetHex.Value));

                if (CurrentHex != null)
                {                   
                    if (UpdateDistance != null) UpdateDistance(new TextEventArgs(GenerateDistanceText()));
                }

            }
        }

        public void SkipHittedHex()
        {
            if (TargetHex != null)
                if (SkipTargetHexIllumination != null) SkipTargetHexIllumination(this, new HexCoordEventArgs(TargetHex.Value));
            TargetHex = null;
        }

        public void SkipSelectedHex()
        {
            if (CurrentHex != null)
                if (SkipCurrentHexIllumination != null) SkipCurrentHexIllumination(this, new HexCoordEventArgs(CurrentHex.Value));
            CurrentHex = null;
        }

        public void ChangeTerrainType(HexCoord hexPosition)
        {
            if (HexMap[hexPosition].Type != CurrentTerrainType)
            {
                if (PaintHex != null) PaintHex(this, new TerrainEventArgs(hexPosition, CurrentTerrainType));
                HexMap[hexPosition].Type = CurrentTerrainType;
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
            if (CurrentHex != null && TargetHex != null)
            {
                var moveDistance = HexAlgorithms.CalculateDistance(CurrentHex.Value, TargetHex.Value);

                return "Current Hex : " + CurrentHex + "\nTarget Hex : "
                       + TargetHex + "\nDistance : " + moveDistance;
            }
            return "Current Hex :\nTarget Hex :\nDistance : ";
        }
    }
}
