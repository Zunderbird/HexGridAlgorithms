using System;
using System.Collections.Generic;
using Assets.HexGridAlgorithms;
using Assets.MVC.HexAlgorithmsEventArgs;
using System.IO;
using Newtonsoft.Json;

namespace Assets.MVC.Models
{
    public abstract class BaseModel
    {
        public int MapWidthInHex { get; set; }
        public int MapHeightInHex { get; set; }

        protected Dictionary<HexCoord, Hex> HexMap;

        protected HexCoord? CurrentHex;
        protected HexCoord? TargetHex;

        public event EventHandler LoadingNextStage;
        public event EventHandler MapLoaded;

        public delegate void HexEvent(object sender, HexEventArgs e);
        public event HexEvent HexCreated;

        public delegate void PointEvent(object sender, PointEventArgs e);
        public event PointEvent MapsCentreCoordsFound;

        public delegate void HexCoordEvent(object sender, HexCoordEventArgs e);
        public event HexCoordEvent IlluminateTargetHex;
        public event HexCoordEvent IlluminateCurrentHex;
        public event HexCoordEvent SkipTargetHexIllumination;
        public event HexCoordEvent SkipCurrentHexIllumination;

        public delegate void TextEvent(object sender, TextEventArgs text);
        public event TextEvent UpdateDistance;

        public abstract void SelectHex(HexCoord hexPosition);
        public abstract void HitHex(HexCoord hexPosition);
        public abstract void SkipHittedHex();
        public abstract void SkipSelectedHex();

        protected void OnMapLoaded()
        {
            var handler = MapLoaded;
            if (handler != null && HexMap.Count > 0) handler(this, EventArgs.Empty);
        }

        protected void OnIlluminateTargetHex()
        {
            var handler = IlluminateTargetHex;
            if (handler != null && TargetHex != null) handler(this, new HexCoordEventArgs(TargetHex.Value));
        }

        protected void OnIlluminateCurrentHex()
        {
            var handler = IlluminateCurrentHex;
            if (handler != null && CurrentHex != null) handler(this, new HexCoordEventArgs(CurrentHex.Value));
        }

        protected void OnSkipTargetHexIllumination()
        {
            var handler = SkipTargetHexIllumination;
            if (handler != null && TargetHex != null) handler(this, new HexCoordEventArgs(TargetHex.Value));
        }

        protected void OnSkipCurrentHexIllumination()
        {
            var handler = SkipCurrentHexIllumination;
            if (handler != null && CurrentHex != null) handler(this, new HexCoordEventArgs(CurrentHex.Value));
        }

        protected void OnUpdateDistance()
        {
            var handler = UpdateDistance;
            if (handler != null) handler(this, new TextEventArgs(GenerateDistanceText()));
        }

        protected void OnHexCreated(Point point, HexCoord hexCoord, TerrainTypes terrainType)
        {
            var handler = HexCreated;
            if (handler != null) handler(this, new HexEventArgs(point, hexCoord, terrainType));
        }

        public virtual void SaveMap()
        {
            File.WriteAllText(@"Assets/HexMap.json", JsonConvert.SerializeObject(HexMap));
        }

        public virtual void LoadMap()
        {
            var text = File.ReadAllText(@"Assets/HexMap.json");

            var dict = JsonConvert.DeserializeObject<Dictionary<string, Hex>>(text);

            HexMap = new Dictionary<HexCoord, Hex>();

            foreach (var record in dict)
            {
                var hexCoord = JsonConvert.DeserializeObject<HexCoord>(record.Key);
                HexMap.Add(hexCoord, record.Value);
                OnHexCreated(hexCoord.ToHexCoord(), hexCoord, record.Value.Type);
            }
            OnMapLoaded();
        }

        public void FindMapsCentreCoords()
        {
            var centreCoord = new Point(MapWidthInHex / 2, MapHeightInHex / 2);
            if (MapsCentreCoordsFound != null) MapsCentreCoordsFound(this, new PointEventArgs(centreCoord));
        }

        public string GenerateDistanceText()
        {
            if (CurrentHex == null || TargetHex == null) return "Current Hex :\nTarget Hex :\nDistance : ";

            var moveDistance = HexAlgorithms.CalculateDistance(CurrentHex.Value, TargetHex.Value);

            return "Current Hex : " + CurrentHex + "\nTarget Hex : " + TargetHex + "\nDistance : " + moveDistance;       
        }

        public virtual void NextStage()
        {
            SaveMap();
            if (LoadingNextStage != null) LoadingNextStage(this, EventArgs.Empty);
        }
    }
}
