using System;
using System.Collections.Generic;
using Assets.HexGridAlgorithms;
using Assets.MVC.HexAlgorithmsEventArgs;
using System.IO;
using Newtonsoft.Json;

//using LitJson;

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

        public struct MyStruct
        {
             
        }

        public virtual void SaveMap()
        {
            var dict = new Dictionary<Hex, Hex>
            {
                { new Hex(TerrainTypes.Forest), new Hex(TerrainTypes.Forest)},
                { new Hex(TerrainTypes.Lake), new Hex(TerrainTypes.Lake)},
                { new Hex(TerrainTypes.Hill), new Hex(TerrainTypes.Hill)}
            };
            File.WriteAllText(@"Assets/HexMap.json", JsonConvert.SerializeObject(dict));
            //var dict = new Dictionary<string, Hex>();

            //foreach (var hex in HexMap)
            //{
            //    dict.Add(hex.Key.ToString(), hex.Value);
            //}

            //File.WriteAllText(@"Assets/HexMap.json", JsonMapper.ToJson(dict));
        }

        public virtual void LoadMap()
        {
            var text = File.ReadAllText(@"Assets/HexMap.json");

            var dict = JsonConvert.DeserializeObject<Dictionary<Hex, Hex>>(text);
            //var dict = JsonMapper.ToObject<Dictionary<string, Hex>>(text);

            foreach (var record in dict)
            {
                UnityEngine.Debug.Log(record.Key + ": " + record.Value.Type);
            }
            //HexMap = new Dictionary<HexCoord, Hex>();
            //var hexMap = JsonMapper.ToObject<Dictionary<HexCoord, Hex>>(text);
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
            if (LoadingNextStage != null) LoadingNextStage(this, EventArgs.Empty);
        }
    }
}
