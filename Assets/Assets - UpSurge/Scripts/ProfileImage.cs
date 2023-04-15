using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImage : MonoBehaviour
{
    public int Pointer = 0;
    public int MaleFemale = 0;
    public GameObject PrevButton;
    public GameObject NextButton;
    public List<Image> UIAvatars = new List<Image>();
    public List<Sprite> MaleAvatars;
    public List<Sprite> FemaleAvatars;

    void Start()
    {
        UpdateImages();
    }

    public void Previouse()
    {
        if (MaleAvatars.Count == 0 && FemaleAvatars.Count==0) return;

        Pointer--;
        if (Pointer < 0) Pointer = 0;
        UpdateImages();
    }

    public void Next()
    {
        if (MaleAvatars.Count == 0 && FemaleAvatars.Count == 0) return;

        Pointer++;

        if (MaleFemale == 0)
        {
            if (Pointer >= MaleAvatars.Count) Pointer = MaleAvatars.Count-1;
        }
        if (MaleFemale == 1)
        {
            if (Pointer >= FemaleAvatars.Count) Pointer = FemaleAvatars.Count - 1;
        }
        UpdateImages();
    }

    public void UpdateImages()
    {
        Sprite _sprite = null;
        if (MaleFemale == 0)
        {
            _sprite = MaleAvatars[Pointer];
        }
        if (MaleFemale == 1)
        {
            _sprite = FemaleAvatars[Pointer];
        }

        for (int i = 0; i < UIAvatars.Count; i++)
        {
            UIAvatars[i].sprite = _sprite;
        }
    }
}
