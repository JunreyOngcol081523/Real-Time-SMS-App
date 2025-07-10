using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Real_Time_SMS_App.ViewModels;

public partial class ContactViewModel : ObservableObject
{
    private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "contacts.json");

    [ObservableProperty]
    private string newPhoneNumber;

    public ObservableCollection<string> PhoneNumbers { get; } = new();

    public ContactViewModel()
    {
        LoadPhoneNumbers();
    }

    [RelayCommand]
    private void AddPhoneNumber()
    {
        if (string.IsNullOrWhiteSpace(NewPhoneNumber)) return;
        if (!PhoneNumbers.Contains(NewPhoneNumber))
        {
            PhoneNumbers.Add(NewPhoneNumber);
            SavePhoneNumbers();
            NewPhoneNumber = string.Empty;
        }
    }

    [RelayCommand]
    private void DeletePhoneNumber(string number)
    {
        if (PhoneNumbers.Contains(number))
        {
            PhoneNumbers.Remove(number);
            SavePhoneNumbers();
        }
    }

    private void LoadPhoneNumbers()
    {
        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            var contactList = JsonSerializer.Deserialize<List<Contact>>(json);

            if (contactList != null)
            {
                foreach (var contact in contactList)
                {
                    if (!string.IsNullOrWhiteSpace(contact.PhoneNumber))
                        PhoneNumbers.Add(contact.PhoneNumber.Trim());
                }
            }
        }
    }


    private void SavePhoneNumbers()
    {
        var contactList = PhoneNumbers
            .Select(p => new Contact { PhoneNumber = p })
            .ToList();

        var json = JsonSerializer.Serialize(contactList, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, json);
    }

}
