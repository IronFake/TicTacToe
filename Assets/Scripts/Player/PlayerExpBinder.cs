namespace TicTacToe.Player
{
    public class PlayerExpBinder : PlayerDataBinder
    {
        protected override void UpdateValue(Player player)
        {
            textField.text = player.exp.ToString();
        }
    }
}
