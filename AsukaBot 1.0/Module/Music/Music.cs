using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.IO;
using YoutubeExplode;
using System.Collections.Generic;
using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;
using System.Threading;
using Discord.Audio;
using System;
using System.Diagnostics;
using System.Linq;
using AsukaBot_1._0.Classes;

namespace AsukaBot_1._0.Module.Music
{
    public class Music : ModuleBase<ICommandContext>
    {
        static Queue<SongInfo> PlayList;
        public static Thread player;
        public static IVoiceChannel channel;
        public static IAudioClient audioClient;

        public Music()
        {
            if (PlayList == null)
            {
                PlayList = new Queue<SongInfo>();
            }
            if (player == null)
            {
                player = new Thread(new ThreadStart(PlayMusicAsync));
                player.Start();
            }
        }


        [Command("join")]
        public async Task JoinChannel(IVoiceChannel Currenchannel = null)
        {

            channel = (Context.Message.Author as IGuildUser)?.VoiceChannel;

            audioClient = await channel.ConnectAsync();
        }

        [Command("leave")]
        public async Task LeaveChannel()
        {
            Console.WriteLine(channel.Bitrate);
            await audioClient.StopAsync();
        }

        [Command("play")]
        public async Task PlayMusic([Remainder]string url)
        {
            EmbedBuilder builder = new EmbedBuilder();

            SongInfo songInfo = new SongInfo();
            if (audioClient == null)
            {
                channel = (Context.Message.Author as IGuildUser)?.VoiceChannel;
            }
            //await songInfo.AddVideoInfo(url);
            await songInfo.AddVideoInfoAudio(url);
            PlayList.Enqueue(songInfo);
            builder.AddField("Title", songInfo.title);
            builder.AddField("Duration", songInfo.duration);
            builder.AddField("Queue", PlayList.Count);
            await ReplyAsync("Youtube", false, builder.Build());
        }

        [Command("pause")]
        public async Task StopMusic()
        {
            await ReplyAsync("Paused");
        }

        [Command("unpause")]
        public async Task RestartMusic()
        {
            player.Resume();
            await ReplyAsync("Unpaused");
        }

        [Command("stop")]
        public async Task Killmusic()
        {
            player.Abort();
            await ReplyAsync("music stopped");
        }

        [Command("playlist")]
        public async Task GetPlayList()
        {
            if (PlayList.Count > 0)
            {
                for (int i = 0; i < PlayList.Count; i++)
                {
                    await Context.Channel.SendMessageAsync((1 + i) + ":" + PlayList.ToArray()[i].title); // fucking cancer
                }
            }
            else
            {
                await ReplyAsync("Error no songs in playlist");
            }

        }

        public async void PlayMusicAsync()
        {

            Process ffmpeg = null;
            AudioStream pcm = null;
            while (true)
            {
                if (PlayList.Count > 0)
                {
                    await GetAudioClient();
                    if (audioClient == null)
                    {
                        Console.WriteLine("[ERROR]");
                        await Task.Delay(900);
                    }
                    else
                    {
                        ffmpeg = CreateStreamAudio(await PlayList.Peek().Uri());
                        pcm = audioClient.CreatePCMStream(AudioApplication.Music);
                        try
                        {
                            await ffmpeg.StandardOutput.BaseStream.CopyToAsync(pcm);
                        }
                        finally { await pcm.FlushAsync(); }
                    }
                    PlayList.Dequeue();

                }
            }
        }


        private Process CreateStreamAudio(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg.exe",
                Arguments = $"-err_detect ignore_err -i {path} -f s16le -ar 48000 -vn -ac 2 pipe:1 -loglevel error",
                UseShellExecute = false,
                RedirectStandardOutput = true
            });
        }

        private async Task GetAudioClient()
        {
            if (audioClient == null || audioClient.ConnectionState != ConnectionState.Connected)
            {
                if (channel != null)
                {
                    audioClient = await channel.ConnectAsync();
                }
            }
        }
    }

    class SongInfo
    {
        public bool Ready = false;
        Video video;
        public string title;
        public string author;
        public string duration;
        public string ID;
        public Func<Task<string>> Uri;


        public async Task AddVideoInfoAudio(string url)
        {
            var client = new YoutubeClient();
            ID = YoutubeClient.ParseVideoId(url);
            video = await client.GetVideoAsync(ID);

            title = video.Title;
            author = video.Author;
            duration = video.Duration.ToString();
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(ID);
            var stream = streamInfoSet.Audio.WithHighestBitrate();

            Uri = async () =>
            {
                await Task.Yield();
                return stream.Url;
            };

        }
    }
}
