using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control
{
    /// <summary>
    /// 功能：
    ///     向上滚动文字
    /// 用法：
    ///     scroll.IsAutoLineNumber = true;
    ///     scroll.AddColumnHeader("Id号", 160, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("姓名", 160, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("性别", 120, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("年龄", 120, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("胶片类型", -0.3f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("科室", -0.3f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.AddColumnHeader("备注", -0.4f, ContentAlignment.MiddleCenter, ContentAlignment.MiddleCenter);
    ///     scroll.StartScroll();
    ///     
    ///     scroll.AddRow(new object[]
    ///         {
    ///             user.id,
    ///             user.name ?? "",
    ///             user.is_male ? "男" : "女",
    ///             user.age,
    ///             user.dcm_type ?? "",
    ///             user.study_department ?? "",
    ///             user.desc ?? ""
    ///         });
    /// </summary>
    public class ScrollingText : Control
    {
        #region 静态函数
        public static void DrawString(Graphics g, Font font, Brush brush,
            string str, RectangleF rect, ContentAlignment alignment)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;

            StringFormat sf = new StringFormat();
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            g.DrawString(str, font, brush, rect, sf);
        }
        public static void DrawString(Graphics g, Font font, Color textColor,
            string str, RectangleF rect, ContentAlignment alignment)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;

            StringFormat sf = new StringFormat();
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }

            SolidBrush brush = new SolidBrush(textColor);
            g.DrawString(str, font, brush, rect, sf);
            brush.Dispose();
        }
        #endregion

        #region 内部类
        public class ScrollingTextCell
        {
            public string Text { get; set; }
            public ContentAlignment TextAlignment { get; set; }

            public ScrollingTextCell()
            {
                this.Text = null;
                this.TextAlignment = ContentAlignment.MiddleLeft;
            }

            public void Draw(Graphics g, Font font, Color fontColor, RectangleF bound)
            {
                ScrollingText.DrawString(g, font, fontColor, this.Text, bound, this.TextAlignment);
            }
        }
        public class ScrollingTextRow
        {
            public List<ScrollingTextCell> Cells { get; set; }

            public ScrollingTextRow()
            {
                this.Cells = new List<ScrollingTextCell>();
            }

            /// <summary>
            /// 当非自动行号时，line_number无效
            /// 自动行号时，显示line_number
            /// </summary>
            /// <param name="g"></param>
            /// <param name="font"></param>
            /// <param name="fontColor"></param>
            /// <param name="cols"></param>
            /// <param name="y"></param>
            /// <param name="yheight"></param>
            /// <param name="line_number"></param>
            public void Draw(Graphics g, Font font, Color fontColor, List<ColumnHeader> cols, float y, float yheight, int line_number)
            {
                for (int i = 0; i < this.Cells.Count; i++)
                {
                    if (cols == null || i >= cols.Count)
                        return;

                    var col = cols[i];
                    var cell = this.Cells[i];
                    RectangleF bound = new RectangleF(col.Bounds.X, y, col.Bounds.Width, yheight);
                    if (col.IsAutoLineNumber)
                        cell.Text = line_number.ToString();
                    cell.Draw(g, font, fontColor, bound);
                }
            }
        }
        public class ColumnHeader
        {
            /// <summary>
            /// >0表示以像素表示的宽度
            /// <0表示占剩余宽度的百分比
            /// 只有在>0时才是实际宽度
            /// </summary>
            public float Width { get; set; }

            public string Text { get; set; }
            public ContentAlignment TextAlignment { get; set; }
            public RectangleF Bounds { get; set; }

            /// <summary>
            /// 是不是自动生成的？
            /// 用于自动行号
            /// </summary>
            public bool IsAutoLineNumber { get; set; }

            public ContentAlignment RowCellAlignment { get; set; }

            public ColumnHeader()
            {
                this.Text = "";
                this.TextAlignment = ContentAlignment.MiddleLeft;
                this.RowCellAlignment = ContentAlignment.MiddleLeft;
                this.IsAutoLineNumber = false;
            }

            public void Draw(Graphics g, Font font, Color fontColor)
            {
                ScrollingText.DrawString(g, font, fontColor,
                    this.Text, this.Bounds, this.TextAlignment);
            }
        }
        #endregion

        #region 公共属性+函数
        public bool ShowColumnHeader { get; set; }
        public Font ColumnHeaderFont { get; set; }
        public Color ColumnHeaderForeColor { get; set; }
        public Color ColumnHeaderBackColor { get; set; }
        public float ColumnHeaderHeight { get; set; }
        public List<ColumnHeader> ColumnHeaders { get; private set; }

        public List<ScrollingTextRow> Rows { get; private set; }
        public float RowHeight { get; set; }
        /// <summary>
        /// 开始显示的行号
        /// </summary>
        public int ShowRowIndex { get; private set; }
        /// <summary>
        /// 显示行时总体Y方向偏移量
        /// </summary>
        public float ShowRowYOffset { get; private set; }

        private bool _auto_line_number = false;
        public bool IsAutoLineNumber 
        {
            get { return _auto_line_number; }
            set
            {
                if (!value && CheckAutoLineNumber())
                {
                    this.ColumnHeaders.RemoveAt(0);
                    ReCalcColumnHeaders();
                }
                if (value && !CheckAutoLineNumber())
                {
                    InsertColumnHeader(0, "序号", 160);
                    this.ColumnHeaders[0].IsAutoLineNumber = true;
                    ReCalcColumnHeaders();
                }
                _auto_line_number = value;
            }
        }
        private bool CheckAutoLineNumber()
        {
            if (this.ColumnHeaders == null || this.ColumnHeaders.Count <= 0)
                return false;
            return this.ColumnHeaders[0].IsAutoLineNumber;
        }

        /// <summary>
        /// 滚动速度
        /// </summary>
        public float ScrollSpeed { get; set; }
        /// <summary>
        /// 最后的空白行数
        /// </summary>
        public int LastBlankRow { get; set; }

        public ScrollingText()
        {
            this.DoubleBuffered = true;

            this.ShowColumnHeader = true;
            this.ColumnHeaderFont = new Font("宋体", 22f);
            this.ColumnHeaderForeColor = Color.Black;
            this.ColumnHeaderBackColor = Color.LightBlue;
            this.ColumnHeaderHeight = 30f / 72f * 96f;
            this.ColumnHeaders = new List<ColumnHeader>();
            
            this.Rows = new List<ScrollingTextRow>();
            this.RowHeight = 30f;
            this.ShowRowIndex = 0;
            this.ShowRowYOffset = 0f;
            this.IsAutoLineNumber = true;

            this.ScrollSpeed = 1f;
            this.LastBlankRow = 2;

            _timer = new System.Timers.Timer();
            _timer.AutoReset = true;
            _timer.Interval = 1000;
            _timer.Enabled = false;
            _timer.Elapsed += timer_Elapsed;
        }
        public void Reset()
        {
            this.ShowRowIndex = 0;
            this.ShowRowYOffset = 0f;
        }
        public void AddColumnHeader(string header, float width, 
            ContentAlignment alignment=ContentAlignment.MiddleCenter, 
            ContentAlignment cell_alignment=ContentAlignment.MiddleCenter)
        {
            this.ColumnHeaders.Add(new ColumnHeader()
            {
                Text = header,
                TextAlignment = alignment,
                Width = width,
                RowCellAlignment = cell_alignment
            });
            ReCalcColumnHeaders();
        }
        public void InsertColumnHeader(int index, string header, float width,
            ContentAlignment alignment = ContentAlignment.MiddleCenter,
            ContentAlignment cell_alignment = ContentAlignment.MiddleCenter)
        {
            this.ColumnHeaders.Insert(index, new ColumnHeader()
            {
                Text = header,
                TextAlignment = alignment,
                Width = width,
                RowCellAlignment = cell_alignment
            });
            ReCalcColumnHeaders();
        }
        public void DeleteColumnHeader(int index)
        {
            this.ColumnHeaders.RemoveAt(index);
        }
        public void DeleteColumnHeader(string header)
        {
            this.ColumnHeaders = this.ColumnHeaders.Where(ch => !ch.Text.Equals(header)).ToList();
        }
        /// <summary>
        /// 先计算固定宽度，后根据实际宽度计算比例分配的宽度
        /// </summary>
        public void ReCalcColumnHeaders()
        {
            float total_percent = 0;
            float total_width = 0;
            this.ColumnHeaders.ForEach(ch =>
            {
                if (ch.Width < 0)
                    total_percent += ch.Width;
                else
                    total_width += ch.Width;
            });

            float x = 0;
            this.ColumnHeaders.ForEach(ch =>
            {
                ch.Bounds = new RectangleF(x, 0f,
                    ch.Width < 0 ? (this.Width - total_width) * (ch.Width / total_percent) : ch.Width,
                    this.ColumnHeaderHeight);
                x += ch.Bounds.Width;
            });
        }

        public ScrollingTextRow NewRow(int column_number)
        {
            ScrollingTextRow row = new ScrollingTextRow();

            //第一列
            if (IsAutoLineNumber)
                row.Cells.Add(new ScrollingTextCell());
            row.Cells[0].TextAlignment = this.ColumnHeaders[0].RowCellAlignment;

            //其它列
            for (int i = 0; i < column_number; i++)
                row.Cells.Add(new ScrollingTextCell());

            this.Rows.Add(row);
            return row;
        }
        public ScrollingTextRow NewRow(int index, int column_number)
        {
            ScrollingTextRow row = new ScrollingTextRow();

            if (IsAutoLineNumber)
                row.Cells.Add(new ScrollingTextCell());
            row.Cells[0].TextAlignment = this.ColumnHeaders[0].RowCellAlignment;

            for (int i = 0; i < column_number; i++)
                row.Cells.Add(new ScrollingTextCell());

            this.Rows.Insert(index, row);
            return row;
        }
        public void AddRow(object[] cells)
        {
            if (cells == null || cells.Length <= 0)
                throw new ArgumentException("输入数组参数不能为空.");

            ScrollingTextRow row = NewRow(cells.Length);
            int offset = IsAutoLineNumber ? 1 : 0;
            for (int i = 0; i < cells.Length; i++)
            {
                row.Cells[i + offset].Text = cells[i].ToString();
                row.Cells[i + offset].TextAlignment = this.ColumnHeaders[i + offset].RowCellAlignment;
            }
        }
        public void InsertRow(int index, object[] cells)
        {
            if (cells == null || cells.Length <= 0)
                throw new ArgumentException("输入数组参数不能为空.");

            ScrollingTextRow row = NewRow(index, cells.Length);
            int offset = IsAutoLineNumber ? 1 : 0;
            for (int i = 0; i < cells.Length; i++)
            {
                row.Cells[i + offset].Text = cells[i].ToString();
                row.Cells[i + offset].TextAlignment = this.ColumnHeaders[i + offset].RowCellAlignment;
            }
        }
        public void DeleteRow(int index)
        {
            this.Rows.RemoveAt(index);
        }
        public void DeleteRow(ScrollingTextRow row)
        {
            this.Rows.Remove(row);
        }

        public void StartScroll(double interval = 25)
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Interval = interval;
                _timer.Enabled = true;
            }
            Reset();
        }
        public void StopScroll()
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
            }
        }
        public bool NeedScroll()
        {
            float total_height = this.Rows.Count * this.RowHeight + 
                (this.ShowColumnHeader ? this.ColumnHeaderHeight : 0f);
            return total_height > this.Height;
        }
        #endregion

        public new void Dispose()
        {
            StopScroll();

            this._timer.Dispose();
            this.ColumnHeaders.Clear();
            this.Rows.Clear();

            base.Dispose();
        }

        private System.Timers.Timer _timer = null;
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (NeedScroll())
            {
                this.ShowRowYOffset -= this.ScrollSpeed;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(this.BackColor), this.Bounds);
            DrawRows(g);

            if (this.ShowColumnHeader)
            {
                g.FillRectangle(new SolidBrush(this.ColumnHeaderBackColor), new Rectangle(0, 0, this.Width, (int)ColumnHeaderHeight));
                DrawColumnHeaders(g);
                DrawGrid(g, new Pen(Color.Black, 2f));
            }
        }
        private void DrawGrid(Graphics g, Pen pen)
        {
            g.DrawLine(pen, 0f, this.ColumnHeaderHeight, this.Width, this.ColumnHeaderHeight);
        }
        private void DrawColumnHeaders(Graphics g)
        {
            foreach (var ch in this.ColumnHeaders)
            {
                ch.Draw(g, this.ColumnHeaderFont, this.ColumnHeaderForeColor);
            }
        }
        private void DrawRows(Graphics g)
        {
            int n = this.LastBlankRow;//最后空n行
            try
            {
                if (this.ShowRowYOffset <= -this.RowHeight)
                {
                    this.ShowRowIndex++;
                    this.ShowRowYOffset = 0f;
                }
                if (this.ShowRowIndex >= this.Rows.Count + n)
                    this.ShowRowIndex = 0;

                float y = this.ShowColumnHeader ? this.ColumnHeaderHeight : 0;
                y += this.ShowRowYOffset;
                int i;
                for (i = this.ShowRowIndex; i < this.Rows.Count; i++)
                {
                    ScrollingTextRow row = this.Rows[i];
                    row.Draw(g, this.Font, this.ForeColor, this.ColumnHeaders,
                        y, this.RowHeight, i + 1);
                    y += this.RowHeight;
                    if (y >= this.Height)
                        break;
                }

                if (this.ShowRowIndex > 0)
                {
                    //隐含y<this.Height
                    //填补，开头至ShowRowIndex的行
                    //补上2行空白后才开始绘制
                    int nn = this.Rows.Count + n - this.ShowRowIndex;
                    y += this.RowHeight * Math.Min(n, nn);
                    if (y < this.Height)
                    {
                        for (i = 0; i < this.ShowRowIndex; i++)
                        {
                            ScrollingTextRow row = this.Rows[i];
                            row.Draw(g, this.Font, this.ForeColor, this.ColumnHeaders,
                                y, this.RowHeight, i + 1);
                            y += this.RowHeight;
                            if (y >= this.Height)
                                break;
                        }
                        if (i == this.ShowRowIndex)
                        {
                            //未满屏，重置
                            Reset();
                        }
                    }
                }
            }
            catch { }
        }
    }
}
