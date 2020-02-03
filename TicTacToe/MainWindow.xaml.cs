using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Holds value of x and o
        /// </summary>
        private Mark[,] mResults;
        /// <summary>
        /// Whether the turn of player one
        /// </summary>
        private bool playerOneTurn;
        /// <summary>
        /// If the game has ended
        /// </summary>
        private bool gameEnded;
        public MainWindow()
        {
            InitializeComponent();
            newGame();
        }

        private void newGame()
        {
            mResults = new Mark[3,3];
            for (var i = 0; i < mResults.GetLength(0); i++)
                for(var j = 0; j<mResults.GetLength(1); j++)
                    mResults[i,j] = Mark.Free;
            playerOneTurn = true;
            /// Iterate every button
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            
            {
                button.Content = String.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            gameEnded = false;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(gameEnded)
            {
                //MessageBox.Show("Game Ended");
                newGame();
                return;
            }
            // Cast to button
            var button = (Button)sender;
            // Get value of the button
            var col = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = 3 * row + col;

            if (mResults[row,col] != Mark.Free)
                return;
            // Set the value based on turn
            mResults[row,col] = playerOneTurn ? Mark.Cross : Mark.Circle;
            button.Content = playerOneTurn ? "X" : "O";
            playerOneTurn = !playerOneTurn;
            if (playerOneTurn!)
                button.Foreground = Brushes.Red;

            gameEnded = CheckIfEnded() || checkIfFull();
        }

        private bool checkIfFull()
        {
            for (var i = 0; i < mResults.GetLength(0); i++)
                for (var j = 0; j < mResults.GetLength(1); j++)
                {
                    if (mResults[i, j] == Mark.Free)
                        return false;
                    
                }
            MessageBox.Show("Game Ended");
            newGame();
            return true;
        }

        private bool CheckIfEnded()
        {
            bool win = false;
            string winnerP1 = "";

            /// Check the horizontal lines for win
            for (var i = 0; i < mResults.GetLength(0); i++)
            {
                if (mResults[i, 0] != Mark.Free && (mResults[i, 0] & mResults[i, 1] & mResults[i, 2]) == mResults[i, 0])
                {
                    win = true;
                    winnerP1 = mResults[i, 0] == Mark.Cross ? "Player One" : "Player Two";
                }
            }

            // check the vertical lines for win
            for (var j = 0; j < mResults.GetLength(1); j++)
            {
                if (mResults[0, j] != Mark.Free && (mResults[0, j] & mResults[1, j] & mResults[2, j]) == mResults[0, j])
                {
                    win = true;
                    winnerP1 = mResults[0, j] == Mark.Cross ? "Player One" : "Player Two";
                }
            }

            // check the diagonals for win
            if(mResults[0,0]!=Mark.Free && (mResults[0,0] & mResults[1, 1] & mResults[2, 2]) == mResults[0,0])
            {
                win = true;
                winnerP1 = mResults[1, 1] == Mark.Cross ? "Player One" : "Player Two";
            }
            if (mResults[0, 2] != Mark.Free && (mResults[0, 2] & mResults[1, 1] & mResults[2, 0]) == mResults[0, 2])
            {
                win = true;
                winnerP1 = mResults[1, 1] == Mark.Cross ? "Player One" : "Player Two";
            }

            if(win)
            {
                MessageBox.Show("Congratulations! "+ winnerP1 + " is the winner");
                newGame();
            }
            
            return win;
        }
    }
}
