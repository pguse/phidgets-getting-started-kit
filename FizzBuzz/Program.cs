using System;
using Phidget22;
using Phidget22.Events;

namespace FizzBuzz
{
    class Program
    {
        static int counter = 0;
    
        static void Main(string[] args)
        {
            bool programIsRunning = true;

            //create object
            DigitalInput redButton = new DigitalInput();

            //address object
            redButton.HubPort = 0;
            redButton.IsHubPortDevice = true;
            redButton.StateChange += redButton_StateChange;

            //open object
            redButton.Open(1000);

            while (programIsRunning){
                
                //check for key press
                if (Console.KeyAvailable){
                    Console.WriteLine("Ending Program");
                    programIsRunning = false;
                }

                System.Threading.Thread.Sleep(150);
            }
            

            //close object
            redButton.Close();
        }

        private static void redButton_StateChange(object sender, DigitalInputStateChangeEventArgs e){
            //read digital input state
            if (e.State == true){
                counter++;
                if (counter % 3 == 0 && counter % 5 == 0){
                    Console.WriteLine("fizz buzz");
                } else if (counter % 3 == 0) {
                    Console.WriteLine("fizz");
                } else if (counter % 5 == 0) {
                    Console.WriteLine("buzz");
                } else {
                    Console.WriteLine(counter);
                }
            } else {
                Console.WriteLine("Red Button Not Pressed");
            }
        }
    }
}
