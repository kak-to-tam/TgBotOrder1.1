using tgBotOrderV11.TgBot.TgLogic.MachineState.User;

namespace tgBotOrderV11.TgBot.TgLogic.Service;

public class TRC20Operation : IOperation
{
    public string From {get; set;}
    public string To {get; set;}
    public float Amout {get; set;}

}
