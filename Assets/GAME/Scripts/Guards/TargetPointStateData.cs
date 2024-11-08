namespace GAME.Scripts.Guards
{
    public class TargetPointStateData : IStateData
    {
        public int TargetPointIndex { get; private set; }
        
        public TargetPointStateData(int targetPointIndex)
        {
            TargetPointIndex = targetPointIndex;
        }
    }
}