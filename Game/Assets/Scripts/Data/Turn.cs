﻿namespace Data
{
    public enum TurnType
    {
        LocalPlayer,
        RemotePlayer
    }

    struct Turn
    {
        public static TurnType First;
        public static int CurrentTurn;
        public static TurnType CurrentPhase;
    }
}