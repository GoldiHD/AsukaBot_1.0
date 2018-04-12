using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Audio;
using System.Diagnostics;
using System.IO;
using System.Collections.Concurrent;

namespace AsukaBot_1._0.Module.Music
{
    public class Music : ModuleBase<ICommandContext>
    {

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

        private async Task SendAsync(IAudioClient client, string path)
        {
            var ffmpeg = CreateStream(path);
            var output = ffmpeg.StandardOutput.BaseStream;
            var discord = client.CreatePCMStream(AudioApplication.Mixed);
            await output.CopyToAsync(discord);
            await discord.FlushAsync();
        }
    }
}
