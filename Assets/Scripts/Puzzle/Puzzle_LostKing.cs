using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Puzzle_LostKing : Puzzle_Lock
{

    public const string FAKE_HINTS_FOLDER = "Fake";
    public const string REAL_HINTS_FOLDER = "Hints";

    protected override void GenerateRandomHint()
    {
        code = "";
        Sprite[] fakeHints = Resources.LoadAll<Sprite>(Path.Combine(hintFolderPath, FAKE_HINTS_FOLDER));
        Sprite[] solutions = Resources.LoadAll<Sprite>(Path.Combine(hintFolderPath, REAL_HINTS_FOLDER));

        int hintIndex = Random.Range(0, hints.Length);
        int spriteIndex = Random.Range(0, solutions.Length);

        string spriteName = solutions[spriteIndex].name;
        SetHintImages(hints[hintIndex], solutions[spriteIndex]);
        code += spriteName.Substring(spriteName.IndexOf('_') + 1);

        for(int i = 0; i < hints.Length; i++)
        {
            if (hintIndex == i)
                continue;
            spriteIndex = Random.Range(0, fakeHints.Length);
            SetHintImages(hints[i], fakeHints[spriteIndex]);
        }
    }

    // Add a second sprite variable if a different sprite is needed for ExamineImage
    private void SetHintImages(Hint hint, Sprite sprite)
    {
        hint.SetSprite(sprite);
        hint.SetExamineImage(sprite);
    }
}
