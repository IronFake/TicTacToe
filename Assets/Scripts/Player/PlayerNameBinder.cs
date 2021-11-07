namespace TicTacToe.Player
{
    public class PlayerNameBinder : PlayerDataBinder
    {
        protected override void UpdateValue(Player player)
        {
            textField.text = player.name;
        }
    }
}
