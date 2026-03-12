namespace Utils
{
    public class GlobalData
    {
    
        public static int rowCountDefault = 4; //rowCountDefault should be between 2 to 6
        public static int columnCountDefault = 4; //columnCountDefault should be between 2 to 6
        public static int rowCount = rowCountDefault;
        public static int columnCount = columnCountDefault;
        public static int targetCount;

        public const int GAME_SCENE_INDEX = 1;
        public const int LOBBY_SCENE_INDEX = 0;
        public const int MATCH_MULTIPLIER = 10;
        public const int UNMATCH_MULTIPLIER = -2;
    }
}
