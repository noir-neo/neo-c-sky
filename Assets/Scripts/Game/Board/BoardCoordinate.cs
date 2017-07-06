namespace NeoC.Game.Model
{
    [System.Serializable]
    public struct BoardCoordinate
    {
        public int x;
        public int y;

        public void SetValue(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}