using Discord;
using Discord.Audio;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.Music
{
    public class AudioService
    {
        public static List<SongInfo> SongStorage = new List<SongInfo>();
        private readonly ConcurrentDictionary<ulong, IAudioClient> ConnectedChannels = new ConcurrentDictionary<ulong, IAudioClient>();
        public static IAudioClient client;


        public async Task JoinAudio(IGuild guild, IVoiceChannel target)
        {
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                return;
            }
            if (target.Guild.Id != guild.Id)
            {
                return;
            }

            client = await target.ConnectAsync();

            if (ConnectedChannels.TryAdd(guild.Id, client))
            {
                Console.WriteLine("Joined channel " + guild.Name);
            }
        }

        public async Task LeaveAudio(IGuild guild)
        {
            if (ConnectedChannels.TryRemove(guild.Id, out client))
            {
                await client.StopAsync();
                Console.WriteLine("Left voice channel " + guild.Name);
            }
        }

        public async Task StopAudio(IGuild guild)
        {
            await client.StopAsync();
            return;
        }



        public async Task SendAudioAsync(IGuild guild, IMessageChannel channel, string path)
        {
            if (!File.Exists(path))
            {
                await channel.SendMessageAsync("File does not exist.");
                return;
            }
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                Stream output = CreateStream(path).StandardOutput.BaseStream;
                AudioOutStream stream = client.CreateDirectPCMStream(AudioApplication.Music, 128 * 1024);
                await output.CopyToAsync(stream);
                await stream.FlushAsync().ConfigureAwait(false);

            }
        }


        public async Task SendLinkAsync(IGuild guild, IMessageChannel channel, string path)
        {
            if (ConnectedChannels.TryGetValue(guild.Id, out client))
            {
                Stream output = CreateLinkStream(path).StandardOutput.BaseStream;
                AudioOutStream stream = client.CreatePCMStream(AudioApplication.Music, 128 * 1024);
                await output.CopyToAsync(stream);
                await stream.FlushAsync().ConfigureAwait(false);

            }
        }

        private Process CreateStream(string path)
        {
            Process currentsong = new Process();
            currentsong.StartInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            currentsong.Start();
            return currentsong;
        }

        private Process CreateLinkStream(string url)
        {

            Process currentsong = new Process();

            currentsong.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C youtube-dl.exe -o - {url} | ffmpeg -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            currentsong.Start();
            return currentsong;
        }
    }
}
