using CommunityToolkit.Mvvm.Input;
using Real_Time_SMS_App;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App
{
    using System.Collections.ObjectModel;
    using System.Text.Json;

    public static class PreferencesHelper
    {
        /// <summary>
        /// Saves an ObservableCollection to Preferences using JSON serialization.
        /// </summary>
        public static void SaveCollection<T>(string key, ObservableCollection<T> collection)
        {
            if (collection == null) return;

            var json = JsonSerializer.Serialize(collection);
            Preferences.Set(key, json);
        }

        /// <summary>
        /// Loads an ObservableCollection from Preferences using JSON deserialization.
        /// </summary>
        public static ObservableCollection<T> LoadCollection<T>(string key)
        {
            var json = Preferences.Get(key, string.Empty);
            if (string.IsNullOrWhiteSpace(json))
                return new ObservableCollection<T>();

            try
            {
                var list = JsonSerializer.Deserialize<ObservableCollection<T>>(json);
                return list ?? new ObservableCollection<T>();
            }
            catch
            {
                // Optional: log or handle corrupted data here
                return new ObservableCollection<T>();
            }
        }
        //remove collection from preferences
        public static void RemoveFromCollection<T>(string key, T item)
        {
            var collection = LoadCollection<T>(key);
            if (collection.Contains(item))
            {
                collection.Remove(item);
                SaveCollection(key, collection);
            }
        }
    }

}
//example usage in a ViewModel
//[ObservableProperty]
//ObservableCollection<string> cities;

//const string CitiesKey = "SavedCities";

//public MyViewModel()
//{
//    Cities = PreferencesHelper.LoadCollection<string>(CitiesKey);
//}

//[RelayCommand]
//private void SaveCities()
//{
//    PreferencesHelper.SaveCollection(CitiesKey, Cities);
//}