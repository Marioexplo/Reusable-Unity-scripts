using System.Collections.Generic;
using UnityEngine;

namespace TextScroller
{
    /// <summary>Automatically scrolls texts with functions for all situations</summary>
    public class TextScroll : MonoBehaviour
    {
        #region General variables
        [SerializeField, InspectorName("Text"), TextArea]
        private string current;
        [Tooltip("A group of texts. It's perfect to show texts one after another")]
        [SerializeField] private string[] array;
        private int arrayNumber;
        /// <summary>You can also use <see cref="EnableAutoArray(float, bool)"/></summary>
        [Tooltip("Automatic change of texts of the array")]
        public bool autoArray;
        /// <summary>You can also use <see cref="EnableAutoArray(float, bool)"/></summary>
        [Tooltip("The period of the AutoArray's switching")]
        public float pauseTime;
        [Tooltip("A group of texts to call through name")]
        [SerializeField] private List<Branch> branches;
        public List<Branch> Branches => branches;
        [Tooltip("The period of the scrolling")]
        public float time;

        [System.Serializable]
        /// <summary>It contains the branch name and its text</summary>
        public struct Branch
        {
            public string name;
            [TextArea] public string text;

            public Branch(string Name, string Text)
            {
                name = Name;
                text = Text;
            }
        }
        #endregion

        #region Scrolling
        private float timer;
        private string scrolled = string.Empty;
        private int Count => scrolled.Length;

        void Update()
        {
            timer += Time.deltaTime;
            if (timer > time) Add();
        }

        private void Add()
        {
            scrolled += current[Count];
            timer = 0f;
            if (HasFinished)
            {
                if (autoArray && arrayNumber < array.Length - 1)
                {
                    enabled = false;
                    System.Collections.IEnumerator PauseTimer()
                    {
                        yield return new WaitForSeconds(pauseTime);
                        SetArrayIndex(arrayNumber + 1);
                    }
                    StartCoroutine(PauseTimer());
                }
                else enabled = false;
            }
        }
        #endregion

        #region Interface
        /// <remarks>Also <see cref="Reenable"/>s the Behaviour</remarks>
        private void Load(string simple)
        {
            Reenable();
            current = simple;
        }

        /// <inheritdoc cref="Load(string)"/>
        /// <summary>Load an array of texts: the first one is taken to start scrolling</summary>
        public void Load(string[] texts)
        {
            array = texts;
            arrayNumber = 0;
            Load(texts[0]);
        }

        /// <inheritdoc cref="SetBranch(string)"/>
        /// <summary>Load an array of texts, each one with a branch name</summary>
        /// <param name="firstBranch">The name of the first branch to scroll</param>
        /// <exception cref="KeyNotFoundException"/>
        public void Load(List<Branch> structure, string firstBranch)
        {
            branches = structure;
            SetBranch(firstBranch);
        }

        /// <inheritdoc cref="Load(string)"/>
        /// <exception cref="System.IndexOutOfRangeException"/>
        public void SetArrayIndex(int index) => Load(array[arrayNumber = index]);

        /// <summary>Dis/enable the automatic change of texts of the array</summary>
        /// <param name="interval">The time between the end of the scrolling of the text and the change of arrayNumber</param>
        public void EnableAutoArray(float interval, bool enable = true)
        {
            autoArray = enable;
            pauseTime = interval;
        }

        /// <inheritdoc cref="Load(string)"/>
        /// <exception cref="KeyNotFoundException"/>
        public void SetBranch(string name)
        {
            int index = -1;
            for (int i = 0; i < branches.Count; i++) if (branches[i].name == name) index = i;
            if (index == -1) throw new KeyNotFoundException();
            Load(branches[index].text);
        }


        public string GetScrolled => scrolled;

        /// <summary>Check if it has finished scrolling the current text</summary>
        public bool HasFinished => Count == current.Length;


        /// <summary>Restart the timer and scrolled letters</summary>
        public void Restart()
        {
            timer = 0f;
            scrolled = string.Empty;
        }

        /// <summary><see cref="Restart"/>s and enables the Behaviour</summary>
        public void Reenable()
        {
            Restart();
            enabled = true;
        }
        #endregion
    }
}