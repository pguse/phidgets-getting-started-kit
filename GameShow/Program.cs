using System;
using Phidget22;
using Phidget22.Events;

namespace GameShow
{
    class Program
    {
        static bool turnRedLEDOn = false;
        static bool turnGreenLEDOn = false;

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                if (turnRedLEDOn) {
                    turnRedLEDOn = false;
                } else {
                    turnRedLEDOn = true;
                }
            }
        }

        private static void greenButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                if (turnGreenLEDOn) {
                    turnGreenLEDOn = false;
                } else {
                    turnGreenLEDOn = true;
                }
            }
        }

        static int Question(){
            Random rnd = new Random();
            int a = rnd.Next(1,13);
            int b = rnd.Next(1,13);
            Console.Write("What is {0} x {1} ? ", a, b);
            return a*b;
        }

        static void Main(string[] args)
        {
            bool programIsRunning = true;
            bool newQuestion = true;

            bool redTurn = false;
            bool greenTurn = false;
            int redPoints = 0;
            int greenPoints = 0;

            int answer = 0;

            //create objects
            DigitalInput redButton = new DigitalInput();
            DigitalOutput redLED = new DigitalOutput();
            DigitalInput greenButton = new DigitalInput();
            DigitalOutput greenLED = new DigitalOutput();

            //address objects
            redButton.HubPort =0;
            redButton.IsHubPortDevice = true;
            greenButton.HubPort =5;
            greenButton.IsHubPortDevice = true;

            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;
            greenLED.HubPort = 4;
            greenLED.IsHubPortDevice = true;

            //add event handlers
            redButton.StateChange += redButton_StateChange;
            greenButton.StateChange += greenButton_StateChange;


            //open objects
            redButton.Open(1000);
            redLED.Open(1000);
            greenButton.Open(1000);
            greenLED.Open(1000);

            while (programIsRunning) {
                // Ask the question
                if (newQuestion){
                    answer = Question();
                    newQuestion = false;
                }

                // handle red response
                if (turnRedLEDOn && !greenTurn) {
                    redTurn = true;
                    redLED.State = true;
                    System.Threading.Thread.Sleep(500);
                    if ( answer == int.Parse( Console.ReadLine() ) ){
                        redPoints++;
                    }
                    Console.WriteLine("Red: {0}  Green: {1}", redPoints, greenPoints);
                    redLED.State = false;
                    turnRedLEDOn = false;
                    redTurn = false;
                    newQuestion = true;
                } else {
                    redLED.State = false;
                }
                
                // handle green response
                if (turnGreenLEDOn && !redTurn) {
                    greenTurn = true;
                    greenLED.State = true;
                    System.Threading.Thread.Sleep(500);
                    if ( answer == int.Parse( Console.ReadLine() ) ){
                        greenPoints++;
                    }
                    Console.WriteLine("Red: {0}  Green: {1}", redPoints, greenPoints);
                    greenLED.State = false;
                    turnGreenLEDOn = false;
                    greenTurn = false;
                    newQuestion = true;
                } else {
                    greenLED.State = false;
                }

                // check for winner
                if (redPoints == 5){
                    Console.WriteLine("Red Wins!");
                    programIsRunning = false;
                } else if (greenPoints == 5){
                    Console.WriteLine("Green Wins!");
                    programIsRunning = false;
                }
                
                //sleep for 150 milliseconds before checking keys again
                System.Threading.Thread.Sleep(150);
            }
            //close objects
            redButton.Close();
            redLED.Close();
            greenButton.Close();
            greenLED.Close();
        }
    }
}
