using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Resources;

namespace BLITTEngine.Core.Audio
{
    public static class MediaPlayer
    {
        private static Soloud engine;
        private static uint current_song_instance;
        private static Song current_song;
        private static float sfx_vol = 1.0f;
        private static float song_vol = 1.0f;
        private static bool muted;

        public static float EffectVolume
        {
            get => sfx_vol * 100;
            set => sfx_vol = value / 100;
        }

        public static float SongVolume
        {
            get => song_vol * 100;
            set
            {
                song_vol = value / 100;

                if (current_song_instance > 0) engine.setVolume(current_song_instance, song_vol);
            }
        }

        internal static void Init()
        {
            engine = new Soloud();

            engine.init(Soloud.CLIP_ROUNDOFF, Soloud.AUTO, 22050, 1024);
        }

        public static void Play(Song song, bool loop = true)
        {
            if (current_song == song)
            {
                Pause(!IsPaused(song));

                return;
            }

            var was_playing_something_else = false;

            if (current_song != null)
            {
                was_playing_something_else = true;
                engine.fadeVolume(current_song_instance, 0.0f, 1.0f);
                engine.scheduleStop(current_song_instance, 1.0f);
            }

            current_song = song;

            current_song_instance = engine.play(song.song_stream);
            engine.setProtectVoice(current_song_instance, 1);
            engine.setLooping(current_song_instance, loop ? 1 : 0);

            if (was_playing_something_else)
            {
                engine.setVolume(current_song_instance, 0.0f);
                engine.fadeVolume(current_song_instance, song_vol, 1.0f);
            }
            else
            {
                engine.setVolume(current_song_instance, song_vol);
            }
        }

        public static void Fire(Effect effect, float pan = 0.0f, float speed = 1.0f)
        {
            var voice = engine.play(effect.wave);
            engine.setVolume(voice, sfx_vol);
            engine.setPan(voice, pan);

            if (speed < 0.1f) speed = 0.1f;

            engine.setRelativePlaySpeed(voice, speed);
        }

        public static void ToggleAllAudio()
        {
            if (muted)
            {
                engine.setGlobalVolume(1.0f);
                muted = false;
            }
            else
            {
                engine.setGlobalVolume(0.0f);
                muted = true;
            }
        }

        public static void GlobalFade(float to, float seconds)
        {
            engine.fadeGlobalVolume(to, seconds);
        }

        public static void Pause(bool pause)
        {
            if (pause)
            {
                engine.schedulePause(current_song_instance, 1.0f);
                engine.fadeVolume(current_song_instance, 0.0f, 1.0f);
            }
            else
            {
                engine.fadeVolume(current_song_instance, 1.0f, 1.0f);
                engine.setPause(current_song_instance, 0);
            }
        }

        internal static Song LoadSong(string file)
        {
            var stream = new WavStream();
            stream.load(file);
            
            var song = new Song(stream);

            return song;
        }

        internal static bool IsPlaying(Song song)
        {
            return song == current_song;
        }

        internal static bool IsPaused(Song song)
        {
            return song == current_song && engine.getPause(current_song_instance) == 1;
        }

        internal static void Shutdown()
        {
            engine.stopAll();
            engine.deinit();
        }
    }
}