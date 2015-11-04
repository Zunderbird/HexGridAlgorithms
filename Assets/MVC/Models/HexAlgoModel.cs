using Assets.HexGridAlgorithms;

namespace Assets.MVC.Models
{
    public class HexAlgoModel : BaseModel
    {
        public override void SelectHex(HexCoord hexPosition)
        {
            if (CurrentHex == hexPosition) return;

            OnSkipCurrentHexIllumination();

            CurrentHex = hexPosition;

            OnIlluminateCurrentHex();

            OnUpdateDistance(); 
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
            OnSkipCurrentHexIllumination(); 

            CurrentHex = null;
        }
    }
}
