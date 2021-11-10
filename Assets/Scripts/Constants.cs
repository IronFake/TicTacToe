
namespace TicTacToe
{
    public static class Constants
    {
        public enum EScene
        {
            MainMenu = 1,
            Core = 2,
        }

        public static int GetSceneIndex(EScene scene)
        {
            return (int) scene;
        }

        public static class PlayerPrefsKeys
        {
            public static string PLAYER_DATA => "PlayerData";
            public static string BACKGROUND_MUSIC_VOLUME => "BGMusicVolume";
            public static string BACKGROUND_MUSIC_ON => "BGMusicOn";
        }
    }
}