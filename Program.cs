
using Telegram.Bot;
using Telegram.Bot.Types;

const string token = "8973595494:AAFaMxr52MaMPI0JCwlQcLq3eg2KMWoWDwU";

var bot = new TelegramBotClient(token);

var me = await bot.GetMe();

Console.WriteLine($"Bot gestartet: @{me.Username}");

using var cts = new CancellationTokenSource();

bot.OnMessage += async (message, cancellationToken) =>
{
    if (message.Text is not { } text)
        return;

    Console.WriteLine(
        $"Nachricht von {message.Chat.Id}: {text}");

    string reply = text switch
    {
        "/start" =>
            "Willkommen! Dein C# Bot funktioniert.",

        "/test" =>
            "✅ Test erfolgreich! Der Bot läuft.",

        "/help" =>
            "Befehle:\n/start – Starten\n/test – Bot testen\n/help – Hilfe",

        _ =>
            $"Du hast geschrieben: {text}"
    };

    await bot.SendMessage(
        chatId: message.Chat.Id,
        text: reply,
        cancellationToken: cancellationToken);
};

await Task.Delay(Timeout.Infinite, cts.Token);
