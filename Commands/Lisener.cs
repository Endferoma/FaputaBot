using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace DiscordBotTemplate.Commands {

    public class Lisener : BaseCommandModule{
        //public async Task<DiscordEmbedBuilder> BuildEmbed() {
        //    return new DiscordEmbedBuilder() {
        //        Color = new Optional<DiscordColor>(new DiscordColor(0, 255, 194))
        //    };
        //}
        //public async Task OnOnlineMessageCreated(DiscordClient discordClient) {
        //    var discordChanel = await discordClient.GetChannelAsync(1099396394768400405);
        //    var embed = await BuildEmbed();
        //    var discordMessageBuilder = new DiscordInteractionResponseBuilder();


        //    embed.WithTitle("About me")
        //        .WithDescription("A modular designed discord bot for moderation and stuff")
        //        .WithAuthor("Long", "Tabby.png",
        //            "Tabby.png")
        //        .WithColor(new DiscordColor(0, 255, 194))
        //        .AddField("Owner:", "[Plerx#0175](https://github.com/Plerx2493/)", true)
        //        .AddField("Source:", "[Github](https://github.com/Plerx2493/Mads)", true);

        //    discordMessageBuilder.AddEmbed(embed.Build());
        //    discordMessageBuilder.AddComponents(new DiscordButtonComponent(ButtonStyle.Success, "feedback-button",
        //        "Feedback"));
        //    discordMessageBuilder.AsEphemeral();

        //    await discordChanel.SendMessageAsync(InteractionResponseType.ChannelMessageWithSource, discordMessageBuilder);}

        [Command("lisener")]
        public async Task CreateButton(DiscordMessage message) {
            var builder = new DiscordMessageBuilder()
                .WithContent("Click the button below:")
                .AddComponents(new DiscordButtonComponent(ButtonStyle.Primary, "button_click", "Click Me"));

            var sentMessage = await message.Channel.SendMessageAsync(builder);

            // Handle button clicks
            Program.Client.ComponentInteractionCreated += async (s, e) =>
            {
                if (e.Id == "button_click") {
                    var member = e.User as DiscordMember; // Get the member who clicked the button (if applicable)

                    await e.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                        .WithContent($"{member.Username} clicked the button!"));

                    // You can perform additional actions here based on the button click.
                }
            };


        }

        // Handle button clicks
    }


}