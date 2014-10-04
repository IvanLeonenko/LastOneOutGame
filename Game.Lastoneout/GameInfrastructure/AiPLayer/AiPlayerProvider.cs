using System;
using System.Collections.Generic;

namespace Game.Lastoneout.GameInfrastructure.AiPLayer
{
    class AiPlayerProvider : IAiPlayerProvider
    {
        private readonly Dictionary<AiPlayers, IAiPlayer> _aiPlayers = new Dictionary<AiPlayers, IAiPlayer>();
        
        public void AddPlayer(AiPlayers aiPlayerType, IAiPlayer aiPlayer)
        {
            _aiPlayers.Add(aiPlayerType, aiPlayer);
        }

        public IAiPlayer GetAiPlayer(AiPlayers aiPlayer)
        {
            if (_aiPlayers.ContainsKey(aiPlayer))
            {
                return _aiPlayers[aiPlayer];
            }
            throw new ApplicationException(string.Format("Ai Player {0} was not found!", aiPlayer));
        }
    }
}
