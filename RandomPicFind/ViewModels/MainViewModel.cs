using Newtonsoft.Json;
using RandomPicFind.Classes;
using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace RandomPicFind.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string imageUrl = "https://c.tenor.com/4k4PssZTZTAAAAAC/finding-nemo-darla.gif", descriptionText = "Welcome! Tap to FIND button...";
    private bool webmBtnEnable = false, mp4BtnEnable = false, gifBtnEnable = false;
    private string? webmLink = null, mp4Link = null, gifLink = null;

    public ICommand FindCommand { get; }
    public ICommand SaveGifCommand { get; }
    public ICommand SaveWebmCommand { get; }
    public ICommand SaveMp4Command { get; }

    public MainViewModel()
    {
        FindCommand = new ViewModelCommand(ExecutedLoginCommand);
        SaveGifCommand = new ViewModelCommand(ExecutedSaveGifCommand, CanExecuteSaveGifCommand);
        SaveWebmCommand = new ViewModelCommand(ExecutedSaveWebmCommand, CanExecuteSaveWebmCommand);
        SaveMp4Command = new ViewModelCommand(ExecutedSaveMp4Command, CanExecuteSaveMp4Command);
    }

    private async void ExecutedSaveGifCommand(object obj)
    {
        await SaveFile.SaveFileInFolderAsync("gif", GifLink);
    }

    private async void ExecutedSaveWebmCommand(object obj)
    {
        await SaveFile.SaveFileInFolderAsync("webm", WebmLink);
    }

    private async void ExecutedSaveMp4Command(object obj)
    {
        await SaveFile.SaveFileInFolderAsync("mp4", Mp4Link);
    }
    private bool CanExecuteSaveGifCommand(object obj)
    {
        return !string.IsNullOrEmpty(GifLink);
    }

    private bool CanExecuteSaveWebmCommand(object obj)
    {
        return !string.IsNullOrEmpty(WebmLink);
    }

    private bool CanExecuteSaveMp4Command(object obj)
    {
        return !string.IsNullOrEmpty(Mp4Link);
    }

    private async void ExecutedLoginCommand(object obj)
    {
        Random random = new();

        try
        {
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                WordObject wordObject = new();
                bool go = true;

                while (go)
                {
                    string word = await wordObject.FindRandomWordAsync();
                    string response = await client.GetStringAsync(@$"https://g.tenor.com/v1/search?q={word}&key=YOUR_API_KEY&limit=1000");
                    Rootobject? context = JsonConvert.DeserializeObject<Rootobject>(response);

                    if (context?.results.Length > 0)
                    {
                        var gifCount = context.results.Length;
                        var randomObject = random.Next(gifCount);

                        var result = context.results[randomObject];
                        GifLink = result.media[0].gif.url;
                        WebmLink = result.media[0].webm.url;
                        Mp4Link = result.media[0].mp4.url;

                        WebmBtnEnable = !string.IsNullOrEmpty(WebmLink);
                        MP4BtnEnable = !string.IsNullOrEmpty(Mp4Link);
                        GIFBtnEnable = !string.IsNullOrEmpty(GifLink);

                        ImageUrl = GifLink;
                        DescriptionText = result.content_description;

                        go = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error text: ", ex.Message);
        }
    }

    public string GifLink
    {
        get { return gifLink; }
        set
        {
            if (gifLink != value)
            {
                gifLink = value;
                RaisePropertyChanged(nameof(GifLink));
                ((ViewModelCommand)SaveGifCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string WebmLink
    {
        get { return webmLink; }
        set
        {
            if (webmLink != value)
            {
                webmLink = value;
                RaisePropertyChanged(nameof(WebmLink));
                ((ViewModelCommand)SaveWebmCommand).RaiseCanExecuteChanged();
            }
        }
    }

    public string Mp4Link
    {
        get { return mp4Link; }
        set
        {
            if (mp4Link != value)
            {
                mp4Link = value;
                RaisePropertyChanged(nameof(Mp4Link));
                ((ViewModelCommand)SaveMp4Command).RaiseCanExecuteChanged();
            }
        }
    }

    public string DescriptionText
    {
        get { return descriptionText; }
        set
        {
            if (descriptionText != value)
            {
                descriptionText = value;
                RaisePropertyChanged(nameof(DescriptionText));
            }
        }
    }

    public string ImageUrl
    {
        get { return imageUrl; }
        set
        {
            imageUrl = value;
            RaisePropertyChanged("ImageUrl");
        }
    }

    public bool WebmBtnEnable
    {
        get { return webmBtnEnable; }
        set
        {
            if (webmBtnEnable != value)
            {
                webmBtnEnable = value;
                RaisePropertyChanged(nameof(WebmBtnEnable));
            }
        }
    }

    public bool MP4BtnEnable
    {
        get { return mp4BtnEnable; }
        set
        {
            if (mp4BtnEnable != value)
            {
                mp4BtnEnable = value;
                RaisePropertyChanged(nameof(MP4BtnEnable));
            }
        }
    }

    public bool GIFBtnEnable
    {
        get { return gifBtnEnable; }
        set
        {
            if (gifBtnEnable != value)
            {
                gifBtnEnable = value;
                RaisePropertyChanged(nameof(GIFBtnEnable));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void RaisePropertyChanged(string name)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}