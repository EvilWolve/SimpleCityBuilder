using UnityEngine;

namespace Save
{
	public class PlayerPrefsSaveService : ISaveService
	{
		public void Save<T>(string key, T data) where T : new()
		{
			string json = JsonUtility.ToJson(data);
			PlayerPrefs.SetString(key, json);
		}

		public T Load<T>(string key) where T : new()
		{
			string json = PlayerPrefs.GetString(key);
			return JsonUtility.FromJson<T>(json);
		}
	}
}
