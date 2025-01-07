using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch
{
    public partial class Form1 : Form
    {
        //Hero variables
        Rectangle hero = new Rectangle(280, 540, 40, 10);
        int heroSpeed = 10;

        //Ball variables
        int ballSize = 10;
        int ballSpeed = 8;

        //List of balls
        List<Rectangle> balls = new List<Rectangle>();

        int score = 0;
        int time = 500;

        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();
        int randValue = 0;

        int groundHeight = 50;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }


        private void gameTime_Tick(object sender, EventArgs e)
        {
            //move Hero
            if(leftPressed == true && hero.X > 0)
            {
                hero.X -= heroSpeed;
            }

            if(rightPressed == true && hero.X < this.Width - hero.Width)
            {
                hero.X += heroSpeed;
            }

            //create ball
            int randValue = randGen.Next(0, 101);

            if(randValue < 10)
            {
                int x = randGen.Next(0, this.Width);
                Rectangle newBall = new Rectangle(x, 0, ballSize, ballSize);
                balls.Add(newBall);
            }

            //Move ball
            for (int i = 0; i < balls.Count; i++)
            {
                int y = balls[i].Y + ballSpeed;
                balls[i] = new Rectangle(balls[i].X, y, ballSize, ballSize);
            }

            //check for points
            for (int i = 0; i < balls.Count; i++)
            {
                if (hero.IntersectsWith(balls[i]))
                {
                    score += 10;
                    balls.RemoveAt(i);
                }
            }

            //Remove ball rectangle if off game screen
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].Y > this.Height - groundHeight)
                {
                    balls.RemoveAt(i);
                }
            }



            //redraw the screen
            Refresh();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //update labels
            timeLabel.Text = $"Time Left: {time}";
            scoreLabel.Text = $"Score: {score}";

            //draw ground
            e.Graphics.FillRectangle(greenBrush, 0, this.Height - groundHeight, this.Width, groundHeight);

            //draw hero
            e.Graphics.FillRectangle(whiteBrush, hero);

            //draw balls
            for(int i = 0; i < balls.Count; i++)
            {
                e.Graphics.FillEllipse(greenBrush, balls[i]);
            }
        }
    }
}
