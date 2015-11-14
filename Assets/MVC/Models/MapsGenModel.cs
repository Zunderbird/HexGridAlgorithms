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

        public delegate void TerrainEvent(object sender, TerrainEventArgs e);
        public event TerrainEvent PaintHex;

        public const int MAX_SIZE_NUMBER = 300;

        public void CreateHexMap()
        {
            CurrentHex = null;
            TargetHex = null;

            if (HexMap != null) OnDeleteHexMap();

            HexMap = new Dictionary<HexCoord, Hex>();

            for (var x = 0; x < MapWidthInHex; x++)
            {
                for (var y = 0; y < MapHeightInHex; y++)
                {
                    var cubeCoord = new Point(x, y);
                    var hexCoord = cubeCoord.ToHexCoord();

                    HexMap.Add(cubeCoord.ToHexCoord(), new Hex(CurrentTerrainType));

                    OnHexCreated(cubeCoord, hexCoord, CurrentTerrainType); 
                }
            }
            OnMapLoaded(); 
        }

        public void SetNextTerrainType()
        {
            if ((int)CurrentTerrainType + 1 >= Enum.GetNames(typeof(TerrainTypes)).Length)
                CurrentTerrainType = 0;
            else
                CurrentTerrainType++;

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

        public override void SelectHex(HexCoord hexPosition)
        {
            if (CurrentHex == hexPosition) return;

            CurrentHex = hexPosition;

            OnUpdateDistance();

            ChangeTerrainType(CurrentHex.Value);       

        }

        public override void HitHex(HexCoord hexPosition)
        {
            if (TargetHex == hexPosition) return;

            OnSkipTargetHexIllumination(); 

            TargetHex = hexPosition;

            OnIlluminateTargetHex();
    
            OnUpdateDistance(); 
        }

        public override void SkipHittedHex()
        {
            OnSkipTargetHexIllumination(); 
            TargetHex = null;
        }

        public override void SkipSelectedHex()
        {
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
    }
}
