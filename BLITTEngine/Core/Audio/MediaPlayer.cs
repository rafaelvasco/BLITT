using System;
using BLITTEngine.Core.Foundation.SDL;
using BLITTEngine.Core.Numerics;

namespace BLITTEngine.Core.Audio
{
    public static class MediaPlayer
    {
        private static Song current_song;
        private static int global_song_volume;

        public static int MaxVolume { get; internal set; }

        internal static void Init()
        {
            if (SDL_mixer.Mix_OpenAudio(SDL_mixer.MIX_DEFAULT_FREQUENCY, SDL_mixer.MIX_DEFAULT_FORMAT, 2, 1024) == -1)
            {
                throw new Exception("Error on initializing SDL_mixer");
            }

            MaxVolume = SDL_mixer.MIX_MAX_VOLUME;

            global_song_volume = MaxVolume;
        }

        public static void PlayEffect(Effect effect)
        {
            SDL_mixer.Mix_VolumeChunk(effect.Handle, MaxVolume);
            SDL_mixer.Mix_PlayChannel(1, effect.Handle, 0);
        }

        public static void PlayEffectEx(Effect effect, int volume = 100, float pan = 0, float pitch = 1.0f)
        {
            volume = Calc.Clamp(volume, 0, MaxVolume);

            SDL_mixer.Mix_Volume(1, volume);

            pan = Calc.NormalizeVar(pan, -1.0f, 1.0f);

            byte left_vol = (byte) (127 - 127 * pan);

            SDL_mixer.Mix_SetPanning(1, left_vol, (byte) (254 - left_vol));

            //SDL_mixer.Mix_RegisterEffect()

            SDL_mixer.Mix_PlayChannel(1, effect.Handle, 0);
        }

        public static void PlaySong(Song song)
        {
            if (current_song == song)
            {
                if (SDL_mixer.Mix_PausedMusic() == 0)
                {
                    song.Playing = false;
                    SDL_mixer.Mix_PauseMusic();
                }
                else
                {
                    song.Playing = true;
                    SDL_mixer.Mix_ResumeMusic();
                }
                
                return;
            }

            if (current_song == null)
            {
                if (song.FadeMs == 0)
                {
                    SDL_mixer.Mix_PlayMusic(song.Handle, -1);
                    
                }
                else
                {
                    int res = SDL_mixer.Mix_FadeInMusic(song.Handle, -1, song.FadeMs);
                    Console.WriteLine(res);
                }

                song.Playing = true;

                current_song = song;
            }
            else
            {
                if (song.FadeMs == 0)
                {
                    
                    SDL_mixer.Mix_PlayMusic(song.Handle, -1);
                }
                else
                {
                    SDL_mixer.Mix_FadeOutMusic(current_song.FadeMs);
                    SDL_mixer.Mix_FadeInMusic(song.Handle, -1, song.FadeMs);
                }

                current_song.Playing = false;
                song.Playing = true;

                current_song = song;
            }

            SyncSongVolume();
        }

        public static void SetSongVolume(int volume)
        {
            if (current_song == null)
            {
                return;
            }

            global_song_volume = volume;

            global_song_volume = Calc.Clamp(global_song_volume, 0, MaxVolume);

            SDL_mixer.Mix_VolumeMusic((int) (global_song_volume * current_song.VolumeFactor));
        }

        public static void AddSongVolume(int delta)
        {
            if (current_song == null)
            {
                return;
            }

            global_song_volume += delta;

            global_song_volume = Calc.Clamp(global_song_volume, 0, MaxVolume);

            SDL_mixer.Mix_VolumeMusic((int) (global_song_volume * current_song.VolumeFactor));
        }

        public static void SongMute(bool mute)
        {
            if (current_song == null)
            {
                return;
            }

            global_song_volume = mute ? 0 : MaxVolume;

            SDL_mixer.Mix_VolumeMusic((int) (global_song_volume * current_song.VolumeFactor));
        }

        internal static void SyncSongVolume()
        {
            SetSongVolume(global_song_volume);
        }

        internal static Effect LoadEffect(string file)
        {
            IntPtr handle = SDL_mixer.Mix_LoadWAV(file);

            var effect = new Effect()
            {
                Handle = handle
            };

            return effect;
        }

        internal static Song LoadSong(string file)
        {
            IntPtr handle = SDL_mixer.Mix_LoadMUS(file);

            var song = new Song()
            {
                Handle = handle
            };

            return song;
        }

        internal static void FreeEffect(Effect effect)
        {
            SDL_mixer.Mix_FreeChunk(effect.Handle);
        }

        internal static void FreeSong(Song song)
        {
            SDL_mixer.Mix_FreeMusic(song.Handle);
        }

       
        internal static void Shutdown()
        {
            if (SDL_mixer.Mix_PlayingMusic() == 1)
            {
                SDL_mixer.Mix_HaltMusic();
            }

            if (SDL_mixer.Mix_Playing(1) == 1)
            {
                SDL_mixer.Mix_HaltChannel(1);
            }
            
            SDL_mixer.Mix_CloseAudio();
        }

    }
}
