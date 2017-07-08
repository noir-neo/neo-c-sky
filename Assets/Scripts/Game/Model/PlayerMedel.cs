using UniRx;

namespace NeoC.Game.Model
{
    public class PlayerMedel
    {
        public ReactiveProperty<BoardCoordinate> currentCoordinate { get; private set; }

        public PlayerMedel()
        {
            currentCoordinate = new ReactiveProperty<BoardCoordinate>();
        }

        public void UpdateCoordinate(BoardCoordinate boardCoordinate)
        {
            currentCoordinate.Value = boardCoordinate;
        }
    }
}
