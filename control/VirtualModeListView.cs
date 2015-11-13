using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control
{
    public class VirtualModeListView : ListView
    {
        [Browsable(false), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        new public bool VirtualMode 
        {
            get { return base.VirtualMode; }
            protected set { base.VirtualMode = value; }
        }

        [Browsable(false), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        new public View View 
        {
            get { return base.View; }
            protected set { base.View = value; }
        }

        [Browsable(false), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        new public ListViewItemCollection Items
        {
            get { return base.Items; }
        }

        /// <summary>
        /// 不能空
        /// </summary>
        [Browsable(false), DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public List<ListViewItem> CacheItems { get; private set; }

        public VirtualModeListView()
        {
            this.CacheItems = new List<ListViewItem>();
            this.GridLines = true;
            this.FullRowSelect = true;
            this.View = View.Details;
            this.Scrollable = true;
            this.MultiSelect = false;
            this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.Visible = true;

            this.VirtualMode = true;
            this.VirtualListSize = 0;
            this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
        }

        public void Reload()
        {
            this.Items.Clear();

            this.VirtualListSize = CacheItems.Count;
        }
        public void ScrollToRow(int index)
        {
            try { this.EnsureVisible(index); }
            catch { }
        }
        public void ScrollToLastRow()
        {
            ScrollToRow(CacheItems.Count - 1);
        }
        public void AddItem(ListViewItem item)
        {
            this.CacheItems.Add(item);
            //Reload();
        }
        public void InsertItem(int index, ListViewItem item)
        {
            this.CacheItems.Insert(index, item);
            //Reload();
        }
        public void AddItem(params object[] ps)
        {
            string[] ss = new string[ps.Count()];
            for (int i=0; i<ps.Count(); i++)
            {
                ss[i] = ps[i] == null ? "" : ps[i].ToString();
            }
            ListViewItem item = new ListViewItem(ss);
            AddItem(item);
        }
        public void InsertItem(int index, params object[] ps)
        {
            string[] ss = new string[ps.Count()];
            for (int i = 0; i < ps.Count(); i++)
            {
                ss[i] = ps[i] == null ? "" : ps[i].ToString();
            }
            ListViewItem item = new ListViewItem(ss);
            InsertItem(index, item);
        }
        public void ClearItems()
        {
            this.CacheItems.Clear();
            //Reload();
        }
        public void RemoveItem(int index)
        {
            this.CacheItems.RemoveAt(index);
            //Reload();
        }

        private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (this.CacheItems == null || this.CacheItems.Count == 0)
                return;

            if (e.ItemIndex < 0 || e.ItemIndex >= this.CacheItems.Count)
                return;
            
            e.Item = this.CacheItems[e.ItemIndex];
        }
    }
}
