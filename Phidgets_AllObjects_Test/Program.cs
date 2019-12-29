using System;
using Phidget22;
using Phidget22.Events;

namespace Phidgets_AllObjects_Test
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

            while (programIsRunning) {

                if (turnRedLEDOn){
                    turnRedLEDOn = false;
                    redLED.State = true;
                    System.Threading.Thread.Sleep(500);
                    redLED.State = false;
                }

                if (turnGreenLEDOn){
                    turnGreenLEDOn = false;
                    greenLED.State = true;
                    System.Threading.Thread.Sleep(500);
                    greenLED.State = false;
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
