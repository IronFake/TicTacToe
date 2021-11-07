namespace TicTacToe.Player
{
    public class PlayerExpBinder : PlayerDataBinder
    {
        protected override void UpdateValue(Player player)
        {
            if (player.exp > 0)
            {
                textField.text = player.exp.ToString();
            }
            else if (player.exp < 0)
            {
                textField.text = "-" + player.exp;
            }
            else
            {
                textField.text = player.exp.ToString();
            }
        }
    }
}
