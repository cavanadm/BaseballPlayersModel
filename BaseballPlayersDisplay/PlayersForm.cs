//Program By: Dylan Cavanaugh
//Manipulating data in a database

using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace BaseballPlayersDisplay
{
    public partial class PlayersForm : Form
    {
        public PlayersForm()
        {
            InitializeComponent();
        }

        //Create dbContext object
        private BaseballPlayersModel.BaseballEntities dbcontext = new BaseballPlayersModel.BaseballEntities();


        private void PlayersForm_Load(object sender, EventArgs e)
        {
            //Query ouputs the entire database ordered by the PlayerID
            dbcontext.Players
                .OrderBy(player => player.PlayerID)
                .Load();

            playerBindingSource.DataSource = dbcontext.Players.Local;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            //Query does same as above
            dbcontext.Players
                .OrderBy(player => player.PlayerID)
                .Load();

            playerBindingSource.DataSource = dbcontext.Players.Local;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            //Query searches for last name in database
            playerBindingSource.DataSource = dbcontext.Players.Local
                .Where(player => player.LastName.StartsWith(nameTextBox.Text))
                .OrderBy(player => player.LastName)
                .ThenBy(player => player.FirstName);

            playerBindingSource.MoveFirst();

        }

        private void batAvgButton_Click(object sender, EventArgs e)
        {

            //Query outputs the players that have the batting avg between the min and max
            playerBindingSource.DataSource = dbcontext.Players.Local
                .Where(player => player.BattingAverage >= (Convert.ToDecimal(minTextBox.Text)) &&
                player.BattingAverage <= (Convert.ToDecimal(maxTextBox.Text)))
                .OrderBy(player => player.LastName)
                .ThenBy(player => player.FirstName);

            playerBindingSource.MoveFirst();
        }
    }
}
