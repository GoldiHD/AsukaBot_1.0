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
        static Queue<string> PreQueue;
        public static Task player;
        public static Thread musicDownloader;
        public static IVoiceChannel channel;
        public static IAudioClient audioClient;
        private static CancellationTokenSource TokenMaster = new CancellationTokenSource();
        

        public Music()
        {
            if (PlayList == null)
            {
                PlayList = new Queue<SongInfo>();
            }
            if (player == null)
            {
                player = new Task(PlayMusicAsync, TokenMaster.Token);
                player.Start();
            }

            if (PreQueue == null)
            {
                PreQueue = new Queue<string>();
            }

            if (musicDownloader == null)
            {
                musicDownloader = new Thread(new ThreadStart(DownloadAndQueue));
                musicDownloader.Start();
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


            PreQueue.Enqueue(url);

            if (audioClient == null)
            {
                channel = (Context.Message.Author as IGuildUser)?.VoiceChannel;
            }
            await ReplyAsync("Song have been queued");
        }

        [Command("pause")]
        public async Task StopMusic()
        {
            await ReplyAsync("Paused");
        }

        [Command("unpause")]
        public async Task RestartMusic()
        {
            await ReplyAsync("Unpaused");
        }

        [Command("skip")]
        public async Task Killmusic()
        {
            Console.WriteLine(player.Status);
            TokenMaster.Cancel();
            Console.WriteLine(player.Status);
            await ReplyAsync("music stopped");
        }

        [Command("current")]
        public async Task ShowCurrentSong()
        {
            if (PlayList.Count > 0)
            {
                await Context.Channel.SendMessageAsync(PlayList.Peek().title);
            }
            else
            {
                await Context.Channel.SendMessageAsync("No songs on the playlist");
            }
        }

        [Command("clear")]
        public async Task ClearPlayList()
        {
            PreQueue.Clear();
            PlayList.Clear();
        }

        [Command("playlist")]
        public async Task GetPlayList()
        {
            int location = 1;
            string respone = "";
            Video TempHolder;
            YoutubeClient client = new YoutubeClient();
            if (PlayList.Count > 0)
            {
                for (int i = 0; i < PlayList.Count; i++)
                {
                    respone += (location) + ": " + PlayList.ToArray()[i].title + "\n";
                    location++;
                }
                if (PreQueue.Count > 0)
                {
                    for (int x = 0; x < PreQueue.Count; x++)
                    {

                        TempHolder = await client.GetVideoAsync(YoutubeClient.ParseVideoId(PreQueue.ToArray()[x]));
                        respone += (location) + ": " + TempHolder.Title+"\n";
                        location++;
                    }
                }
                await Context.Channel.SendMessageAsync(respone);
            }
            else
            {
                await ReplyAsync("Error no songs in playlist");
            }

        }

        public async void DownloadAndQueue()
        {
            YoutubeClient client = new YoutubeClient();
            while (true)
            {
                if (PreQueue.Count > 0)
                {
                    for(int c = 0; c < PreQueue.Count; c++)
                    {
                        if (PreQueue.ToList()[c].Contains("index") && PreQueue.ToList()[c].Contains ("list"))
                        {
                            Console.WriteLine("Playlist");
                            List<Video> playlist = new List<Video>();
                            int index = 0;
                            for (int i = 0; i < PreQueue.Count; i++)
                            {
                                if (PreQueue.ToList()[i].Contains("index") && PreQueue.ToList()[i].Contains("list"))
                                {
                                    index = i;
                                    break;
                                }
                            }
                            var templist = await client.GetPlaylistAsync(YoutubeClient.ParsePlaylistId(PreQueue.ToList()[index]));
                            playlist = templist.Videos.ToList();
                            List<string> tempholder = PreQueue.ToList();
                            tempholder.RemoveAt(index);
                            PreQueue = new Queue<string>(tempholder);
                            for (int x = 0; x < playlist.Count; x++)
                            {
                                PreQueue.Enqueue(playlist[x].GetUrl());
                            }
                        }
                    }
                }
                
                 if (PlayList.Count < 5 && PreQueue.Count > 0)
                {

                    if (PreQueue.Peek().Contains("www"))
                    {
                        SongInfo songInfo = new SongInfo();
                        await songInfo.AddVideoInfoAudio(PreQueue.Dequeue());
                        PlayList.Enqueue(songInfo);
                    }
                    else
                    {
                        //seach youtube
                    }
                }
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
