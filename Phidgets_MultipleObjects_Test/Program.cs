using System;
using Phidget22;
using Phidget22.Events;

namespace Phidgets_MultipleObjects_Test
{
    class Program
    {
        static bool turnLEDOn = false;

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                Console.WriteLine("Red Button Pressed");
                turnLEDOn = true;
            } else {
                Console.WriteLine("Red Button Not Pressed");
            }
        }

        static void Main(string[] args)
        {
            bool programIsRunning = true;

            //create objects
            DigitalInput redButton = new DigitalInput();
            DigitalOutput redLED = new DigitalOutput();

            //address objects
            redButton.HubPort =0;
            redButton.IsHubPortDevice = true;

            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;

            //add event handler
            redButton.StateChange += redButton_StateChange;

            //open objects
            redButton.Open(1000);
            redLED.Open(1000);

            while (programIsRunning) {

                if (turnLEDOn) {
                    turnLEDOn = false;
                    redLED.State = true;
                    System.Threading.Thread.Sleep(500);
                    redLED.State = false;
                }

                //check if any key has been pressed
                if (Console.KeyAvailable) {
                    Console.WriteLine("Ending Program");
                    programIsRunning = false;
                }
                
                //sleep for 150 milliseconds before checking keys again
                System.Threading.Thread.Sleep(150);
            }
            //close objects
            redButton.Close();
            redLED.Close();
        }
    }
}
