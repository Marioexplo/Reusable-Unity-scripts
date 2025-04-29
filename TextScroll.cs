using UnityEngine;

/// <summary>Automatically scrolls texts with functions for all situations</summary>
public class TextScroll : MonoBehaviour
{
    [Tooltip("A simple text")]
    [SerializeField] private string text;
    [Tooltip("A group of texts. It's perfect to show texts one after another")]
    [SerializeField] private string[] array;
    [Tooltip("The array's number of the text to get")]
    public int arrayNumber;
    /// <summary>You can also use <see cref="EnableAutoArray(float, bool)"/></summary>
    [Tooltip("Automatic change of texts of the array")]
    public bool autoArray;
    /// <summary>You can also use <see cref="EnableAutoArray(float, bool)"/></summary>
    [Tooltip("The duration of the autoarray's switching")]
    public float pauseTime;
    private float pauseTimer;
    [Tooltip("A group of texts to call with their name")]
    [SerializeField] private Branch[] structure;
    [Tooltip("The name of the branch to scroll")]
    public string branch;
    [Tooltip("The type of text you chose")]
    [SerializeField] private Option type;
    /// <summary>Also set <see cref="time"/></summary>
    [Tooltip("The scrolling state")]
    public bool playing;
    [Tooltip("The period of the scrolling")]
    public float time;
    private float timer;
    [Tooltip("The number of the scrolled letters")]
    public int count;

    void Update()
    {
        if (playing)
        {
            timer += Time.deltaTime;
            if (timer > time)
            {
                void Add()
                {
                    count++;
                    timer = 0f;
                }
                switch (type)
                {
                    case Option.simple:
                        if (count < text.Length)
                        {
                            Add();
                        }
                        break;

                    case Option.array:
                        if (count < array[arrayNumber].Length)
                        {
                            Add();
                        } else if (autoArray && arrayNumber < array.Length - 1)
                        {
                            pauseTimer += Time.deltaTime;
                            if (pauseTimer > pauseTime)
                            {
                                arrayNumber += 1;
                                pauseTimer = 0f;
                                Restart();
                            }
                        }
                        break;

                    case Option.branches:
                        foreach (Branch txt in structure)
                        {
                            if (txt.name == branch && count < txt.text.Length)
                            {
                                Add();
                                break;
                            }
                        }
                        break;
                }
            }
        }
    }

    /// <summary>Load a simple text. Use <see cref="playing"/> to start scrolling</summary>
    public void Load(string txt)
    {
        text = txt;
        type = Option.simple;
    }

    ///<summary>Load an array of texts. Use with <see cref="autoArray"/> or select an <see cref="arrayNumber"/>. Use <see cref="playing"/> to start scrolling</summary>
    public void Load(string[] texts)
    {
        array = texts;
        type = Option.array;
    }

    ///<summary>Load an array of texts, each with a <see cref="branch"/> name. Use <see cref="playing"/> to start scrolling</summary>
    public void Load(Branch[] branches)
    {
        structure = branches;
        type = Option.branches;
    }


    /// <summary>Set <see cref="playing"/> and <see cref="time"/></summary>
    /// <param name="pause">The time after which a letter is added</param>
    public void Play(float pause, bool scrolling = true)
    {
        playing = scrolling;
        time = pause;
    }


    ///<summary>Get the scrolled text</summary>
    public string GetScrolled()
    {
        switch (type)
        {
            case Option.simple: return text[..count];

            case Option.array: return array[arrayNumber][..count];

            case Option.branches:
                foreach (var txt in structure)
                {
                    if (txt.name == branch)
                    {
                        return txt.text[..count];
                    }
                }
                throw new System.Exception("error: wrong branch name");

            default: throw new System.Exception("error: type not set");
        }
    }


    ///<summary>Restart the timer and <see cref="count"/></summary>
    public void Restart() { timer = 0f; count = 0; }

    ///<summary>Dis/enable the automatic change of texts of the array</summary>
    ///<param name="interval">The time between the end of the scrolling of the text and the change of arrayNumber</param>
    public void EnableAutoArray(float interval, bool enable = true) { autoArray = enable; pauseTime = interval; }


    [System.Serializable]
    enum Option { simple, array, branches}

    [System.Serializable]
    /// <summary>It contains the branch name and its text</summary>
    public struct Branch
    {
        public string name;
        public string text;

        public Branch(string Name, string Text)
        {
            name = Name;
            text = Text;
        }
    }
}
