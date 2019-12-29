using System;
using Phidget22;

namespace Phidgets_DigitalOutput_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //create object
            DigitalOutput redLED = new DigitalOutput();

            //address object
            redLED.HubPort = 1;
            redLED.IsHubPortDevice = true;

            //open object
            redLED.Open(1000);

            //set output high (turn LED on)
            //redLED.State = true;
            //set output to 10%
            //redLED.DutyCycle = 0.1;

            //blink LED on for one second and off for one second
            for(var i=0; i < 10; i++){
                redLED.State = true;
                System.Threading.Thread.Sleep(1000);
                redLED.State = false;
                System.Threading.Thread.Sleep(1000);
            }
            
            //gradually increase then decrease the brightness
            //for(var i=1; i <= 200; i = i + 1) {
            //    redLED.DutyCycle = Math.Sin(i*Math.PI/200);
            //    System.Threading.Thread.Sleep(10);
            //}

            //sleep for 500 milliseconds (half a second)
            //System.Threading.Thread.Sleep(500);

            //close object
            redLED.Close();
        }
    }
}
