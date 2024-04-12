using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public Text HeadingText;
    public Text BodyText;
    public float Delay = 1f;

    void Start()
    {
        Invoke("StartCredits", 3);
    }

    void StartCredits()
    {
        HeadingText.text = "Music:";
        BodyText.text = "'Fluffing a Duck' Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 4.0 License http://creativecommons.org/licenses/by/4.0/";
        Invoke("Song2", Delay);
    }

    void Song2 ()
    {
        BodyText.text = "'Cipher' Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 4.0 License http://creativecommons.org/licenses/by/4.0/";
        Invoke("Song3", Delay);
    }

    void Song3 ()
    {
        BodyText.text = "'Wallpaper' Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 4.0 License http://creativecommons.org/licenses/by/4.0/";
        Invoke("Song4", Delay);
    }

    void Song4 ()
    {
        BodyText.text = "'Voxel Revolution' Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 4.0 License http://creativecommons.org/licenses/by/4.0/";
        Invoke("Song5", Delay);
    }

    void Song5 ()
    {
        BodyText.text = "'Meatball Parade' Kevin MacLeod (incompetech.com) Licensed under Creative Commons: By Attribution 4.0 License http://creativecommons.org/licenses/by/4.0/";
        Invoke("OtherCredits", Delay);
    }

    void OtherCredits ()
    {
        HeadingText.text = "Art:";
        BodyText.text = "MISTERPUG51";
        Invoke("Other2", Delay);
    }

    void Other2()
    {
        HeadingText.text = "Program:";
        BodyText.text = "MISTERPUG51";
        Invoke("Other3", Delay);
    }

    void Other3()
    {
        HeadingText.text = "Debug:";
        BodyText.text = "MISTERPUG51";
        Invoke("Other4", Delay);
    }

    void Other4()
    {
        HeadingText.text = "Producer:";
        BodyText.text = "MISTERPUG51";
        Invoke("Other5", Delay);
    }

    void Other5()
    {
        HeadingText.text = "MISTERPUG51";
        BodyText.text = "https://github.com/misterpug51/sticky";
    }

}
