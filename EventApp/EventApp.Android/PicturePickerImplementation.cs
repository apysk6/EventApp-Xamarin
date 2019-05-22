﻿using System.IO;
using System.Threading.Tasks;
using Android.Content;
using EventApp.Droid;
using EventApp.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePickerImplementation))]
namespace EventApp.Droid
{
    public class PicturePickerImplementation : IPicturePicker
    {
        [System.Obsolete]
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Forms.Context as MainActivity;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }
    }
}