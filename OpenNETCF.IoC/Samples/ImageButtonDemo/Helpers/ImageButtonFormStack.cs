using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;

namespace ImageButtonDemo
{
    public class ImageButtonFormStack : IEnumerable<SmartPart>
    {
        private List<SmartPart> InstanceCache { get; set; }
        private List<SmartPart> Stack { get; set; }
        private Workspace Workspace { get; set; }

        public int CurrentPosition { get; private set; }

        public ImageButtonFormStack(Workspace parent)
        {
            Workspace = parent;
            Stack = new List<SmartPart>();
            InstanceCache = new List<SmartPart>();
        }

        public SmartPart Push<T>()
            where T : SmartPart, new()
        {
            T view = InstanceCache.FirstOrDefault(v => v.GetType().Equals(typeof(T))) as T;

            if (view == null)
            {
                view = new T();
                InstanceCache.Add(view);
            }

            // truncate at current position
            if(CurrentPosition < Stack.Count - 1)
            {
                Stack.RemoveRange(CurrentPosition + 1, Count - CurrentPosition - 1);
            }

            Stack.Add(view);
            CurrentPosition = Count - 1;

            Workspace.Show(view);

            return view;
        }

        public SmartPart Back()
        {
            if (CurrentPosition == 0)
            {
                return null;
            }

            CurrentPosition--;

            SmartPart view = Stack[CurrentPosition];
            Workspace.Show(view);
            return view;
        }

        public SmartPart Forward()
        {
            if (CurrentPosition == Count - 1)
            {
                return null;
            }

            CurrentPosition++;

            SmartPart view = Stack[CurrentPosition];
            Workspace.Show(view);
            return view;
        }

        public SmartPart Top
        {
            get { return Stack[Stack.Count - 1]; }
        }

        public SmartPart Current
        {
            get { return Stack[CurrentPosition]; }
        }

        public IEnumerator<SmartPart> GetEnumerator()
        {
            return Stack.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Stack.GetEnumerator();
        }

        public int Count
        {
            get { return Stack.Count; }
        }

        public SmartPart this[int index]
        {
            get { return Stack[index]; }
        }
    }
}
