using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Audio;
using System.Diagnostics;
using Google.Apis.YouTube.v3;
using System.IO;

namespace AsukaBot_1._0.Module.Music
{
    public class Music : ModuleBase<ICommandContext>
    {
        private readonly AudioService _service;
        private IAudioClient client;

        public Music(AudioService service)
        {
            _service = service;
        }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel()
        {
            await _service.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
        }

        [Command("leave", RunMode = RunMode.Async)]
        public async Task LeaveChannel()
        {
            await _service.LeaveAudio(Context.Guild);
        }
        [Command("Stop")]
        public async Task StopSong()
        {

        }
        /*
        [Command("play", RunMode = RunMode.Async)]
        public async Task PlaySong([Remainder] string song)
        {
            await _service.SendAudioAsync(Context.Guild, Context.Channel, song);
        }
        */

        [Command("play", RunMode = RunMode.Async)]
        public async Task play(string path)
        {
            var channel = (Context.User as IVoiceState).VoiceChannel;
            var audioClient = await channel.ConnectAsync();
            try
            {
                var ffmpeg = CreateStream(path);
                Stream output = ffmpeg.StandardOutput.BaseStream;
                AudioOutStream discord = audioClient.CreatePCMStream(AudioApplication.Voice, 1920);
                await output.CopyToAsync(discord);
                await discord.FlushAsync();
            }catch(Exception ex)
            { Console.WriteLine(ex); }

        }


        private Process CreateStream(string path)
        {
            var ffmpeg = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i {path} -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            };
            return Process.Start(ffmpeg);
        }
        /*
        private Process CreateLinkStream2(string URL)
        {
            var currentsong = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguemtn
            };
            CreateLinkStream(URL)
        }

        private Process CreateLinkStream(string url)
        {

            var currentsong = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"youtube-dl.exe -o - "+url+" | ffmpeg -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            };
            
            return Process.Start(currentsong);
        }
        */


    }

}
