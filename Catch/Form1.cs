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
        List<int> ballSpeeds = new List<int>();
        List<string> ballColor = new List<string>();

        int score = 0;
        int time = 500;

        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

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

            if (randValue < 4)
            {
                int x = randGen.Next(0, this.Width);
                Rectangle newBall = new Rectangle(x, 0, ballSize, ballSize);
                balls.Add(newBall);
                ballSpeeds.Add(randGen.Next(4, 16));
                ballColor.Add("red");
            }
            else if (randValue < 11)
            {
                int x = randGen.Next(0, this.Width);
                Rectangle newBall = new Rectangle(x, 0, ballSize, ballSize);
                balls.Add(newBall);
                ballSpeeds.Add(randGen.Next(4, 16));
                ballColor.Add("green");
            }
            else if (randValue < 17)
            {
                int x = randGen.Next(0, this.Width);
                Rectangle newBall = new Rectangle(x, 0, ballSize, ballSize);
                balls.Add(newBall);
                ballSpeeds.Add(randGen.Next(4, 16));
                ballColor.Add("yellow");
            }


            //Move ball
            for (int i = 0; i < balls.Count; i++)
            {
                int y = balls[i].Y + ballSpeeds[i];
                balls[i] = new Rectangle(balls[i].X, y, ballSize, ballSize);
            }

            //check for collision
            for (int i = 0; i < balls.Count; i++)
            {
                if (hero.IntersectsWith(balls[i]))
                {
                    if(ballColor[i] == "green")
                    {
                        score += 10;
                    }
                    else if (ballColor[i] == "red")
                    {
                        score -= 5;
                    }
                    else if (ballColor[i] == "yellow")
                    {
                        time += 50;
                    }

                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    ballColor.RemoveAt(i);
                }
            }

            //Remove ball rectangle if off game screen
            for (int i = 0; i < balls.Count; i++) 
            {
                if (balls[i].Y > this.Height - groundHeight)
                {
                    balls.RemoveAt(i);
                    ballSpeeds.RemoveAt(i);
                    ballColor.RemoveAt(i);
                }
            }

            time--;

            if(time == 0)
            {
                gameTimer.Stop();
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
                if (ballColor[i] == "green")
                {
                    e.Graphics.FillEllipse(greenBrush, balls[i]);
                }
                else if (ballColor[i] == "red")
                {
                    e.Graphics.FillEllipse(redBrush, balls[i]);
                }
                else if (ballColor[i] == "yellow")
                {
                    e.Graphics.FillEllipse(yellowBrush, balls[i]);
                }
            }
        }
    }
}
