using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotAberto
{
    class Program
    {
        private const string tk = "Coloque aqui o token do bot criado no BotFather";
        private static readonly TelegramBotClient bot = new TelegramBotClient(tk);  
        static void Main(string[] args)
        {
            Console.Title = "Telegram Bot Básico";

            bot.OnMessage += Bot_OnMessage;
            
            bot.StartReceiving();
            Console.WriteLine("Olá, o Bot está online!");
            Console.WriteLine("Pressione qualquer tecla para encerrar");
            Console.ReadLine();
            //Thread.Sleep(Timeout.Infinite);
            bot.StopReceiving();
        }

        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var msg = e.Message;
            if (msg.Text == "/start")
            {
                string texto = $"🙋‍♂️ Olá *{msg.Chat.FirstName} {msg.Chat.LastName}*. O texto que você enviar será retornado pelo Bot. " +
                    $"Você pode compartilhar a sua *localização* ou o seu *contato*, basta pressionar os botões abaixo.";
                
                var RequestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    KeyboardButton.WithRequestLocation("📍 Localização"),
                    KeyboardButton.WithRequestContact("👤 Contato"),
                });
                await bot.SendTextMessageAsync(
                    chatId: msg.Chat.Id,
                    text: texto,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: RequestReplyKeyboard);
            }
            
            else if (msg.Type == MessageType.Text)
            {
                await bot.SendTextMessageAsync(msg.Chat.Id, $"Olá *{msg.Chat.FirstName}*. \nVocê escreveu: \n*{msg.Text}*", ParseMode.Markdown);
            }
        }
    }
}
