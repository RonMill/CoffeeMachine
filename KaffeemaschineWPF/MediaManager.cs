﻿using KaffeemaschineWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace KaffeemaschineWPF
{
    public class MediaManager
    {
        private MediaPlayer mediaPlayer;

        public MediaManager()
        {

        }
        public async Task LoadSound(CoffeeMachineTasks coffeeMachineTasks)
        {
            switch (coffeeMachineTasks)
            {
                case CoffeeMachineTasks.FillBeans:
                    await PlaySound("Resources/fillbeans.wav", 2000);
                    break;
                case CoffeeMachineTasks.FillWater:
                    await PlaySound("Resources/fillwater.wav", 2000);
                    break;
                case CoffeeMachineTasks.MakeEspresso:
                    await PlaySound("Resources/espresso.wav", 5000);
                    break;
                case CoffeeMachineTasks.GrindCoffee:
                    await PlaySound("Resources/grindcoffee.wav", 3000);
                    break;
                case CoffeeMachineTasks.Pump:
                    await PlaySound("Resources/pump.wav", 3000);
                    break;
                default:
                    break;
            }
        }
        public async Task PlaySound(string file, int time)
        {
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri(file, UriKind.Relative));
            mediaPlayer.Play();
            var dispatcher = Dispatcher.CurrentDispatcher;
            await Task.Run(() =>
            {

                Thread.Sleep(time);
                dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    
                }));
            });
            mediaPlayer.Stop();
        }
    }
}