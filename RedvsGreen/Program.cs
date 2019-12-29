using System;
using Phidget22;
using Phidget22.Events;

namespace RedvsGreen
{
    class Program
    {
        static bool turnRedLEDOn = false;
        static bool turnGreenLEDOn = false;

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                Console.WriteLine("Red Button Pressed");
                turnRedLEDOn = true;
            } else {
                Console.WriteLine("Red Button Not Pressed");
            }
        }

        private static void greenButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                Console.WriteLine("Green Button Pressed");
                turnGreenLEDOn = true;
            } else {
                Console.WriteLine("Green Button Not Pressed");
            }
        }
        static void Main(string[] args)
        {
            bool programIsRunning = true;
            int redCounter = 0;
            int greenCounter = 0;

            //create objects
            DigitalInput redButton = new DigitalInput();
            DigitalInput greenButton = new DigitalInput();
            DigitalOutput redLED = new DigitalOutput();
            DigitalOutput greenLED = new DigitalOutput();

            //address objects
            redButton.HubPort = 0;
            redButton.IsHubPortDevice = true;

            greenButton.HubPort = 5;
            greenButton.IsHubPortDevice = true;

            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;

            greenLED.HubPort = 4;
            greenLED.IsHubPortDevice = true;

            //add event handlers
            redButton.StateChange += redButton_StateChange;
            greenButton.StateChange += greenButton_StateChange;

            //open objects
            redLED.Open(1000);
            greenLED.Open(1000);
            redButton.Open(1000);
            greenButton.Open(1000);

            redLED.DutyCycle = 0;
            greenLED.DutyCycle = 0;

            while (programIsRunning) {

                if (turnRedLEDOn){
                    redCounter++;
                    redLED.DutyCycle = redCounter / 10.0;
                    turnRedLEDOn = false;
                }

                if (turnGreenLEDOn){
                    greenCounter++;
                    greenLED.DutyCycle = greenCounter / 10.0;
                    turnGreenLEDOn = false;
                }

                if (redCounter == 10){
                    greenLED.State = false;
                    for (var i=0; i < 10; i++){
                        redLED.State = false;
                        System.Threading.Thread.Sleep(200);
                        redLED.State = true;
                        System.Threading.Thread.Sleep(200);
                    }
                    programIsRunning = false;
                }

                if (greenCounter == 10){
                    redLED.State = false;
                    for (var i=0; i < 10; i++){
                        greenLED.State = false;
                        System.Threading.Thread.Sleep(200);
                        greenLED.State = true;
                        System.Threading.Thread.Sleep(200);
                    }
                    programIsRunning = false;
                }
                //check if any key has been pressed
                if (Console.KeyAvailable){
                    Console.WriteLine("Ending Program");
                    programIsRunning = false;
                }
                //sleep for 150 milliseconds before looping
                System.Threading.Thread.Sleep(150);
            }

            //close objects
            redLED.Close();
            greenLED.Close();
            redButton.Close();
            greenButton.Close();
        }
    }
}