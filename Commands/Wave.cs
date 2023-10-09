using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Commands
{
    public class Wave : BaseCommandModule
    {
        [Command("wave")]
        public async Task WaveCommand(CommandContext ctx) 
        {
            //await ctx.Channel.SendMessageAsync("Test Message");

            var discordChanel = ctx.Channel;

            await discordChanel.SendMessageAsync(RandomImageMessageCreator("stickers", "", ctx.User.Mention));
        }

        public DiscordMessageBuilder RandomImageMessageCreator(string type, string target, string sender) {
            var embed = new DiscordEmbedBuilder();
            var discordMessageBuilder = new DiscordMessageBuilder();

            embed.WithImageUrl(RandomImageSelector(type));
            discordMessageBuilder.Content = sender + " said hi to " + target;
            discordMessageBuilder.AddEmbed(embed.Build());
            discordMessageBuilder.AddMention(UserMention.All);

            return discordMessageBuilder;
        }

        public string RandomImageSelector(string type) {
            List<string> images = ReadImageUrls(type);

            if (images.Count != 0) {
                Random rnd = new Random();
                return images[rnd.Next(images.Count)];
            }
            
            return @"https://cdn.discordapp.com/attachments/1099396394768400405/1156506863404339200/Text_-_Error_404_An_error_haz_okuued.jpg";
        }

        public List<string> ReadImageUrls(string type) {
            List<string> images = new List<string>();
            images.AddRange(File.ReadAllLines("Images\\wave\\"+type));
            return images;
        }

        [Command("lisener")]
        public async Task CreateButton(CommandContext ctx) {
            var builder = new DiscordMessageBuilder()
                .WithContent("Click the button below:")
                .AddComponents(new DiscordButtonComponent(ButtonStyle.Primary, "button_click", "Sticker Wave"));

            var sentMessage = await ctx.Message.Channel.SendMessageAsync(builder);

            // Handle button clicks
            Program.Client.ComponentInteractionCreated += async (s, e) => {
                if (e.Id == "button_click") {
                    var member = e.User as DiscordMember; // Get the member who clicked the button (if applicable)

                    await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"{member.Username} clicked the button!"));

                    // You can perform additional actions here based on the button click.
                }
            };
        }
    }
}
