﻿// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace OpenNETCF.IoC
{
    public class ManagedObjectCollection<TItem> : ICollection, IEnumerable, IEnumerable<KeyValuePair<string, TItem>>
        where TItem : class
    {
        private Dictionary<string, TItem> m_items = new Dictionary<string, TItem>();
        private object m_syncRoot = new object();
        private WorkItem m_root;

        public event EventHandler<DataEventArgs<KeyValuePair<string, TItem>>> Added;
        public event EventHandler<DataEventArgs<KeyValuePair<string, TItem>>> Removed;

        internal ManagedObjectCollection(WorkItem root)
        {
            m_root = root;
        }

        public object AddNew(Type typeToBuild)
        {
            return AddNew(typeToBuild, null, true, false);
        }

        public object AddNew(Type typeToBuild, string id)
        {
            return AddNew(typeToBuild, id, false, false);
        }

        public object AddNewDisposable(Type typeToBuild)
        {
            if (!typeToBuild.Implements<IDisposable>()) throw new ArgumentException("typeToBuild is not IDisposable");

            return AddNew(typeToBuild, null, true, true);
        }

        public object AddNewDisposable(Type typeToBuild, string id)
        {
            if (!typeToBuild.Implements<IDisposable>()) throw new ArgumentException("typeToBuild is not IDisposable");

            return AddNew(typeToBuild, id, false, true);
        }

        private object AddNew(Type typeToBuild, string id, bool expectNullId, bool wrapDisposables)
        {
            if (id == null)
            {
                if (expectNullId)
                {
                    id = ObjectFactory.GenerateItemName(typeToBuild, m_root);
                }
                else
                {
                    throw new ArgumentNullException("id");
                }
            }

            if (this.Contains(id))
            {
                throw new ArgumentException("Duplicate ID");
            }

            if (typeToBuild == null) throw new ArgumentNullException("typeToBuild");

            object instance = ObjectFactory.CreateObject(typeToBuild, m_root);

            if ((wrapDisposables) && (instance is IDisposable))
            {
                DisposableWrappedObject<IDisposable> dispInstance = new DisposableWrappedObject<IDisposable>(instance as IDisposable);
                dispInstance.Disposing += new EventHandler<GenericEventArgs<IDisposable>>(DisposableItemHandler);
                Add(dispInstance as TItem, id, expectNullId);
                instance = dispInstance;
            }
            else
            {
                Add(instance as TItem, id, expectNullId);
            }

            WorkItem wi = instance as WorkItem;

            if (wi != null)
            {
                wi.Parent = m_root;
            }

            return instance;
        }

        private void DisposableItemHandler(object sender, GenericEventArgs<IDisposable> e)
        {
            var key = m_items.FirstOrDefault(i => i.Value == sender).Key;
            if (key == null) return;
            m_items.Remove(key);
        }

        public TTypeToBuild AddNew<TTypeToBuild>()
            where TTypeToBuild : class
        {
            return (TTypeToBuild)AddNew(typeof(TTypeToBuild));
        }

        public TTypeToBuild AddNew<TTypeToBuild>(string id)
            where TTypeToBuild : class
        {
            return (TTypeToBuild)AddNew(typeof(TTypeToBuild), id);
        }

        public DisposableWrappedObject<IDisposable> AddNewDisposable<TTypeToBuild>()
            where TTypeToBuild : class, IDisposable
        {
            return (DisposableWrappedObject<IDisposable>)AddNew(typeof(TTypeToBuild), null, true, true);
        }

        public DisposableWrappedObject<IDisposable> AddNewDisposable<TTypeToBuild>(string id)
            where TTypeToBuild : class, IDisposable
        {
            return (DisposableWrappedObject<IDisposable>)AddNew(typeof(TTypeToBuild), id, false, true);
        }

        public void Add(TItem item)
        {
            Add(item, ObjectFactory.GenerateItemName(item.GetType(), m_root));
        }

        public void Add(TItem item, string id)
        {
            Add(item, id, false);
        }

        private void Add(TItem item, string id, bool expectNullId)
        {
            if (id == null) throw new ArgumentNullException("id");
            if ((item == null) && (!expectNullId)) throw new ArgumentNullException("item");

            lock (m_syncRoot)
            {
                m_items.Add(id, item as TItem);
                ObjectFactory.DoInjections(item, m_root);

                WorkItem wi = item as WorkItem;

                if (wi != null)
                {
                    wi.Parent = m_root;
                }

                if (Added == null) return;

                Added(this, new DataEventArgs<KeyValuePair<string, TItem>>(
                    new KeyValuePair<string, TItem>(id, item)));
            }
        }

        public TItem Get(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            if (m_items.ContainsKey(id)) return m_items[id];

            return default(TItem);
        }

        public TTypeToGet Get<TTypeToGet>(string id)
            where TTypeToGet : class
        {
            if (id == null) throw new ArgumentNullException("id");

            if (m_items.ContainsKey(id))
            {
                TTypeToGet t = m_items[id] as TTypeToGet;

                return t;
            }

            return default(TTypeToGet);
        }

        public object this[string id]
        {
            get { return this.Get(id); }
        }

        public ICollection<TSearchType> FindByType<TSearchType>() where TSearchType : class
        {
            return (from i in m_items
                    where i.Value is TSearchType
                    select i.Value as TSearchType)
                    .ToList();
        }

        public void Remove(object item)
        {
            if (item == null) throw new ArgumentNullException("item");
            lock (m_syncRoot)
            {
                var objList = (from i in m_items
                               where i.Value.Equals(item)
                               select i);

                if (objList.Count() == 0) return;
                var obj = objList.First();

                m_items.Remove(obj.Key);

                // dispose of IDisposable objects
                IDisposable d = obj.Value as IDisposable;
                if (d != null) d.Dispose();

                if (Removed == null) return;

                Removed(this, new DataEventArgs<KeyValuePair<string, TItem>>(obj));
            }
        }

        public ICollection<TItem> FindByType(Type searchType)
        {
            if (searchType.IsValueType) throw new ArgumentException("searchType must be a reference type");

            if (searchType.IsInterface)
            {
                return (from i in m_items
                        where i.Value.GetType().GetInterfaces().Contains(searchType)
                        select i.Value)
                        .ToList();
            }
            else
            {
                return (from i in m_items
                        where i.Value.GetType().Equals(searchType)
                        select i.Value)
                        .ToList();
            }
        }

        public bool Contains(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            return m_items.ContainsKey(id);
        }

        public bool ContainsObject(object item)
        {
            if (item == null) throw new ArgumentNullException("item");

            foreach (var i in m_items)
            {
                if (i.Value.Equals(item)) return true;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<string, TItem>> GetEnumerator()
        {
            return m_items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            m_items.ToArray().CopyTo(array, index);
        }

        public int Count
        {
            get { return m_items.Count; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { return m_syncRoot; }
        }

        public void Clear()
        {
            var vals = (from i in this
                        select i.Value).ToArray();

            for (int i = 0; i < vals.Length; i++)
            {
                this.Remove(vals[i]);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(string.Format("{0} : {1}\r\n", item.Key, item.Value.ToString()));
            }
            return sb.ToString();
        }
    }
}
