using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistence : MonoBehaviour
{

    // Devuelve un int mediante una key proporcionada
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    // Devuelve un float mediante una key proporcionada
    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    // Devuelve un string mediante una key proporcionada
    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    // Devuelve un bool mediante una key proporcionada
    public bool GetBool(string key)
    {
        // Devuelve parseado a bool un valor string
        return bool.Parse(PlayerPrefs.GetString(key));
    }


    // S E T T E R S

    // Guarda en local un valor int junto a una key
    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Guarda en local un valor float junto a una key
    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    // Guarda en local un valor string junto a una key
    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    // Guarda en local un valor bool junto a una key
    public void SetBool(string key, bool value)
    {
        // Guarda un bool parseado a string
        PlayerPrefs.SetString(key, value.ToString());
        PlayerPrefs.Save();
    }


    // OTHERS

    // Comprueba si existe algun valor con la key proporcionada
    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    // Elimina todos los valores guardados en local
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}