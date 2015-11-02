using System;
using System.Collections.Generic;
using Assets.HexGridAlgorithms;
using System.IO;
using LitJson;
using Assets.MVC.HexAlgorithmsEventArgs;

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

        public void SaveMap()
        {
            using (var sw = new StreamWriter(@"Assets/HexMap.json"))
            {
                foreach (var hex in HexMap)
                {
                    sw.WriteLine(JsonMapper.ToJson(hex));
                }
            }
        }

        public void LoadMap()
        { }

        public void FindMapsCentreCoords()
        {
            var centreCoord = new Point(MapWidthInHex / 2, MapHeightInHex / 2);
            if (MapsCentreCoordsFound != null) MapsCentreCoordsFound(this, new PointEventArgs(centreCoord));
        }

        public void NextStage()
        {
            if (LoadingNextStage != null) LoadingNextStage(this, EventArgs.Empty);
        }
    }
}
