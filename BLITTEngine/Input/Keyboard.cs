using BLITTEngine.Platform;

namespace BLITTEngine.Input
{

    public static class Keyboard
    {
        private static GamePlatform platform;

        private static KeyboardState current_state;
        private static KeyboardState prev_state;


	    internal static void Init(GamePlatform game_platform)
        {
            platform = game_platform;
        }

	    internal static void Update()
	    {
            prev_state = current_state;
            current_state = platform.GetKeyboardState();
	    }

	    public static bool Down(Key key)
	    {
            return current_state[key];
	    }

	    public static bool Pressed(Key key)
	    {
            return current_state[key] && !prev_state[key];
	    }

	    public static bool Released(Key key)
	    {
            return !current_state[key] && prev_state[key];
        }

    }
}