using System;
using Phidget22;
using Phidget22.Events;

namespace RedLEDTimer
{
    class Program
    {
        static bool turnLEDOn = false;
        static DateTime oldTime;
        static TimeSpan timeSpan;

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true) {
                Console.WriteLine("Red Button Pressed");
                if (turnLEDOn) {
                    turnLEDOn = false;
                    timeSpan = DateTime.Now - oldTime;
                    Console.WriteLine(timeSpan.TotalSeconds);
                } else {
                    turnLEDOn = true;
                    oldTime = DateTime.Now;
                }
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
                    redLED.State = true;
                } else {
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
