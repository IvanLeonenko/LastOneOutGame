namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    public interface IAiPlayerProvider
    {
        IAiPlayer GetAiPlayer(AiPlayers aiplayer);
    }
}
