using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RandomPicFind.Classes;
public class SaveFile
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task SaveFileInFolderAsync(string format, string link)
    {
        Dictionary<string, string> formatFilter = new()
            {
                {"gif", "Image Files(*.GIF)|*.GIF|All files (*.*)|*.*"},
                {"webm", "Video Files(*.WEBM)|*.WEBM|All files (*.*)|*.*"},
                {"mp4", "Video Files(*.MP4)|*.MP4|All files (*.*)|*.*"}
            };

        // Генерация уникального имени файла на основе MD5-хэша ссылки
        using MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(link));
        StringBuilder sBuilder = new StringBuilder();
        foreach (byte b in data)
        {
            sBuilder.Append(b.ToString("x2"));
        }
        string fileName = sBuilder.ToString();

        // Открытие диалогового окна для сохранения файла
        SaveFileDialog dlg = new()
        {
            FileName = fileName,
            DefaultExt = $".{format}",
            Filter = formatFilter.ContainsKey(format) ? formatFilter[format] : "All files (*.*)|*.*"
        };

        if (dlg.ShowDialog() == true)
        {
            try
            {
                // Загрузка и сохранение файла асинхронно
                HttpResponseMessage response = await httpClient.GetAsync(link);
                response.EnsureSuccessStatusCode();
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                await File.WriteAllBytesAsync(dlg.FileName, fileBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Save File Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}