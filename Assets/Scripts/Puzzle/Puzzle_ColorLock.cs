using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Puzzle_ColorLock : Puzzle
{

    public ColorToggle[] colorToggles;

    public const string ITEM_SPRITE_FOLDER = "Item";
    public const string POSTER_SPRITE_FOLDER = "Poster";

    public GreenChest greenChest;

    protected override void GenerateRandomHint()
    {
        code = "";
        Sprite[] itemSprites = Resources.LoadAll<Sprite>(Path.Combine(hintFolderPath, ITEM_SPRITE_FOLDER));
        Sprite[] posterSprites = Resources.LoadAll<Sprite>(Path.Combine(hintFolderPath, POSTER_SPRITE_FOLDER));

        Sprite item = itemSprites[Random.Range(0, itemSprites.Length)];
        Sprite poster = posterSprites[Random.Range(0, posterSprites.Length)];

        code += poster.name.Substring(poster.name.IndexOf('_') + 1);
        code += item.name.Substring(item.name.IndexOf('_') + 1);

        greenChest.SetDropSprite(item);
        hints[0].SetSprite(poster);
        hints[0].SetExamineImage(poster);
    }

    protected override bool IsSolved()
    {
        string currentCode = "";
        foreach (ColorToggle ct in colorToggles)
        {
            currentCode += ct.GetCodeValue();
        }
        return currentCode == code;
    }
}
