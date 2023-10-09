using DiscordBotTemplate.Commands;
using DiscordBotTemplate.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DiscordBotTemplate.Classes;
using System.Threading;

namespace DiscordBotTemplate
{
    public sealed class Program
    {
        public static DiscordClient Client { get; private set; }
        public static CommandsNextExtension Commands { get; private set; }

        private static Timer timer;

        static async Task Main(string[] args)
        {
            //1. Get the details of your config.json file by deserialising it
            var configJsonFile = new JSONReader();
            await configJsonFile.ReadJSON();

            //2. Setting up the Bot Configuration
            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJsonFile.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            //3. Apply this config to our DiscordClient
            Client = new DiscordClient(discordConfig);

            //4. Set the default timeout for Commands that use interactivity
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            //5. Set up the Task Handler Ready event
            Client.Ready += OnClientReady;

            //6. Set up the Commands Configuration
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJsonFile.prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            //7. Register your commands

            Commands.RegisterCommands<Wave>();
            //Commands.RegisterCommands<Lisener>();

            //8. Connect to get the Bot online
            await Client.ConnectAsync();

            // Create a timer to change the channel name every minute
            timer = new Timer(ChangeChannelName, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

            // Add this event handler to your bot's initialization code, typically in the Main method or where you configure your bot client.
            ReactionButtonCreation();

            MCApiCall();

            await Task.Delay(-1);

            

        }

        private static async void ChangeChannelName(object state) {
            try {
                // Get the guild (server) where the channel exists
                var guild = Client.GetGuildAsync(1090959714252226582);
                
                // Get the channel by its ID
                var channel = await Client.GetChannelAsync(1160881539601678456);

                if (channel != null) {
                    // Generate a new channel name (you can modify this logic as needed)
                    string newChannelName = "test2";

                    // Update the channel's name
                    await channel.ModifyAsync(properties => properties.Name = newChannelName);
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }//as

        private static void ReactionButtonCreation() {
            Client.MessageCreated += async (s, e) => {
                // Check if the message sender is a bot
                if (e.Message.Author.IsBot && e.Channel.Id == 1101169298115923988 && e.Author.Username == "Sapphire") {
                    // Create and send the button message in response to the bot's message
                    var builder = new DiscordMessageBuilder()
                        .WithContent(" ")
                        .AddComponents(new DiscordButtonComponent(ButtonStyle.Primary, "btn_click1", "Anime Hi"), new DiscordButtonComponent(ButtonStyle.Primary, "btn_click2", "IRL Hi"), new DiscordButtonComponent(ButtonStyle.Primary, "btn_click3", "Sticker Hi"));


                    Client.ComponentInteractionCreated += async (ds, ev) => {
                        string imgName = "";
                        if (ev.Id == "btn_click1") {
                            imgName = "anime.txt";
                        }
                        else if (ev.Id == "btn_click2") {
                            imgName = "irl.txt";
                        }
                        else if (ev.Id == "btn_click3") {
                            imgName = "stickers.txt";
                        }

                        if (ev.Id == "btn_click1" || ev.Id == "btn_click2" || ev.Id == "btn_click3") {
                            var member = ev.User as DiscordMember; // Get the member who clicked the button (if applicable)

                            var discordChanel = e.Channel;
                            Wave w = new Wave();
                            await discordChanel.SendMessageAsync(w.RandomImageMessageCreator(imgName, e.MentionedUsers[0].Mention, ev.Interaction.User.Mention));

                            //await ev.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                            //    .WithContent($"{member.Username} clicked the button!"));

                            // You can perform additional actions here based on the button click.
                        }
                    };

                    var sentMessage = await e.Message.Channel.SendMessageAsync(builder);
                }
            };
        }

        private static async void MCApiCall() {
            string apiUrl = "https://api.mcsrvstat.us/3/survive.mineinabyss.com";

            using (HttpClient httpClient = new HttpClient()) {
                try {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode) {
                        string json = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON into a Root object
                        ServerInfo.Root serverInfo = JsonConvert.DeserializeObject<ServerInfo.Root>(json);

                        // Now you can access the data in the object like this:
                        if (serverInfo.online) {

                        }
                        else {

                        }

                        //Console.WriteLine(json);

                    }
                    else {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private static Task OnClientReady(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
