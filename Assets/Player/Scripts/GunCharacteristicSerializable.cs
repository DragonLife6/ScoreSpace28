using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameter", menuName = "Gun/Parameter", order = 1)]
public class GunCharacteristicSerializable : ScriptableObject
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] string[] _descriptionLines;
    [SerializeField] Sprite _image;

    public string GetName()
    {
        return _name;
    }
    public int GetId()
    {
        return _id;
    }
    public string GetDescription()
    {
        string resultText = _descriptionLines[0] + "\n\n";
        for (int i = 1; i < _descriptionLines.Length; i++)
        {
            resultText += _descriptionLines[i] + "\n";
        }
        return resultText;
    }
    public Sprite GetImage()
    {
        return _image;
    }
}
