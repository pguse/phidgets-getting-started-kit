using System;
using Phidget22;
using Phidget22.Events;

namespace WhackAMole
{
    class Program
    {
        static bool turnRedLEDOn = false;
        static bool turnGreenLEDOn = false;
        static bool newMole = true;
        static DateTime oldTime;
        static TimeSpan timeSpan;
        static int count = 0;

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true){
                if (turnRedLEDOn){
                    turnRedLEDOn = false;
                    newMole = true;
                    timeSpan += DateTime.Now - oldTime;
                    count++;
                }
            }
        }

        private static void greenButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            if (e.State == true){
                if (turnGreenLEDOn){
                    turnGreenLEDOn = false;
                    newMole = true;
                    timeSpan += DateTime.Now - oldTime;
                    count++;
                }
            }
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int light;

            //create objects
            DigitalInput redButton = new DigitalInput();
            DigitalInput greenButton = new DigitalInput();
            DigitalOutput redLED = new DigitalOutput();
            DigitalOutput greenLED = new DigitalOutput();

            //address objects
            redButton.HubPort = 0;
            redButton.IsHubPortDevice = true;
            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;

            greenButton.HubPort = 5;
            greenButton.IsHubPortDevice = true;
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

            // randomly light LED's 10 times
            while (count < 10){

                if (newMole){
                    redLED.State = false;
                    greenLED.State = false;
                    System.Threading.Thread.Sleep(200);
                    // randomly choose the LED
                    light = rnd.Next(0,2);
                    if (light == 0){
                        turnRedLEDOn = true;
                    } else {
                        turnGreenLEDOn = true;
                    }
                    oldTime = DateTime.Now;
                    newMole = false;
                }
                

                if (turnRedLEDOn) {
                    redLED.State = true;
                }

                if (turnGreenLEDOn) {
                    greenLED.State = true;
                }

                //sleep for 150 milliseconds before checking keys again
                System.Threading.Thread.Sleep(150);
            }

            Console.WriteLine("Total time: {0:0.00}", timeSpan.TotalSeconds);

            //close objects
            redButton.Close();
            redLED.Close();
            greenButton.Close();
            greenLED.Close();
        }
    }
}
