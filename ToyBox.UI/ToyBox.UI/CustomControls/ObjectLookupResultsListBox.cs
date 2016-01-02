using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using ToyBox.Models.Models;
using ToyBox.UI.ThirdParties;

namespace ToyBox.UI.CustomControls
{
    internal class ObjectLookupResultsListBox : BaseListBox
    {

        #region Constants

        private const int ItemBadgeMaxWidth = 50;
        private const int ItemFieldSpacing = 10;
        private const string ItemFontFamilyName = "Consolas";
        private const float ItemFontSize = 10.0f;
        private const int ItemHorizontalMargin = 10;
        private const int ItemDefaultHeight = 20;

        #endregion

        #region Private data
        private int _maxItemWidth;
        private int _fontHeight;

        private BindingList<ObjectLookupResultModel> _dataSource;
        private Font _font;

        #endregion

        #region Constructor

        public ObjectLookupResultsListBox()
        {
            InitializeControl();
            InitializeDrawingElements();
        }

        #endregion

        #region Setup

        private void InitializeControl()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            ItemHeight = ItemDefaultHeight;
        }

        #endregion

        #region Other overrides

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);
            _dataSource = (BindingList<ObjectLookupResultModel>)DataSource;
            _dataSource.ListChanged += DataSourceOnListChanged;
        }

        private void DataSourceOnListChanged(object sender, ListChangedEventArgs listChangedEventArgs)
        {
            if (listChangedEventArgs.ListChangedType == ListChangedType.Reset)
            {
                CalculateMaxItemWidth();
                CalculateSize();
            }
        }

        #endregion

        #region Drawing

        private void InitializeDrawingElements()
        {
            _font = Font;
            CalculateFontHeight();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (_dataSource == null || e.Index < 0 || e.Index >= _dataSource.Count)
            {
                return;
            }

            var dataSourceItem = _dataSource[e.Index];
            e.DrawBackground();
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var isSelected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            if (isSelected)
            {
                e.Graphics.FillRectangle(ColorTheme.SelectionBrush, e.Bounds);
            }

            RenderItem(e.Graphics, e.Bounds, dataSourceItem, isSelected);
        }

        private void CalculateMaxItemWidth()
        {
            if (_dataSource == null)
            {
                _maxItemWidth = Width;
                return;
            }

            _maxItemWidth = 0;
            var maxItemLength = 0;
            foreach (var item in _dataSource)
            {
                var approximateItemString = item.DatabaseObject.ObjectName +
                    String.Format(Resources.TxtObjectLookupItemFormat, item.DatabaseObject.SchemaName, item.DatabaseObject.Database.Name) +
                    item.DatabaseObject.Database.Server.Name +
                    item.DatabaseObject.MappedObjectType;

                if (approximateItemString.Length > maxItemLength)
                {
                    maxItemLength = approximateItemString.Length;
                    var itemWidth = TextRenderer.MeasureText(approximateItemString, _font).Width;
                    itemWidth += ItemFieldSpacing * 2 + 35;

                    if (_maxItemWidth < itemWidth)
                    {
                        _maxItemWidth = itemWidth;
                    }
                }
            }

            Debug.WriteLine(_maxItemWidth);
        }

        private void CalculateFontHeight()
        {
            _fontHeight = TextRenderer.MeasureText("A", _font).Height;
        }

        private void RenderItem(Graphics graphics, Rectangle bounds, ObjectLookupResultModel dataItem, bool isSelected)
        {
            var offsetX = RenderItemBadge(graphics, bounds, dataItem, isSelected);
            offsetX = RenderItemName(graphics, new Point(offsetX, 0), bounds, dataItem, isSelected);
            offsetX = RenderItemDatabaseName(graphics, new Point(offsetX, 0), bounds, dataItem, isSelected);
            RenderItemServer(graphics, bounds, new Point(offsetX, 0), dataItem, isSelected);
        }

        private int RenderItemBadge(Graphics graphics, Rectangle bounds, ObjectLookupResultModel dataItem, bool isSelected)
        {
            const int xOffset = 2;
            int yOffset = bounds.Top + (ItemDefaultHeight - _fontHeight) / 2 - 2;
            int width = 0;

            using (var f = new Font(_font, FontStyle.Bold))
            {
                width = TextRenderer.MeasureText(graphics, dataItem.DatabaseObject.MappedObjectType.ToString(), f).Width + 10;
                int height = _fontHeight + 4;

                graphics.FillRoundedRectangle(ColorTheme.BadgeBackBrush, xOffset, yOffset, width, height, 4);
                TextRenderer.DrawText(graphics, dataItem.DatabaseObject.MappedObjectType.ToString(), f, new Point(xOffset + 5, yOffset + 2), ColorTheme.BadgeForeColor);
            }

            return xOffset + width;
        }

        private int RenderItemName(Graphics graphics, Point offset, Rectangle bounds, ObjectLookupResultModel dataItem, bool isSelected)
        {
            int xOffset = offset.X + 2;
            int yOffset = bounds.Top + (ItemDefaultHeight - _fontHeight) / 2;

            foreach (string textElement in dataItem.MatchElements)
            {
                if (dataItem.MatchElements.IndexOf(textElement) == dataItem.MatchIndex)
                {
                    using (var f = new Font(_font, FontStyle.Bold))
                    {
                        TextRenderer.DrawText(graphics, textElement, f, new Point(xOffset, yOffset), ColorTheme.LookupAutocompletionColor);
                        xOffset += TextRenderer.MeasureText(graphics, textElement, f, Size.Empty, TextFormatFlags.NoPadding).Width + 1;
                    }
                }
                else
                {
                    //xOffset -= GetTextRightMargin(Font);
                    TextRenderer.DrawText(graphics, textElement, _font, new Point(xOffset, yOffset), isSelected ? Color.White : Color.Black);
                    xOffset += TextRenderer.MeasureText(graphics, textElement, _font, Size.Empty, TextFormatFlags.NoPadding).Width;
                }
            }

            return xOffset;
        }

        private int RenderItemDatabaseName(Graphics graphics, Point offset, Rectangle bounds, ObjectLookupResultModel dataItem, bool isSelected)
        {
            int xOffset = offset.X;
            int yOffset = bounds.Top + (ItemDefaultHeight - _fontHeight) / 2;

            var databaseText = String.Format(Resources.TxtObjectLookupItemFormat, dataItem.DatabaseObject.SchemaName, dataItem.DatabaseObject.Database.Name);
            TextRenderer.DrawText(
                graphics,
                databaseText,
                _font,
                new Point(xOffset, yOffset),
                isSelected ? Color.White : Color.FromArgb(1, 109, 109, 109));

            return xOffset + TextRenderer.MeasureText(graphics, dataItem.DatabaseObject.FullyQualifiedName, Font).Width;
        }

        private void RenderItemServer(Graphics graphics, Rectangle bounds, Point offset, ObjectLookupResultModel dataItem, bool isSelected)
        {
            TextRenderer.DrawText(graphics,
                dataItem.DatabaseObject.Database.Server.Name,
                _font, bounds,
                isSelected ? Color.White : Color.DarkGray,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }

        #endregion

        #region Disposable

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                //if (_font != null)
                //{
                //    _font.Dispose();
                //}
            }
        }

        #endregion

        #region Control sizing

        private void CalculateSize()
        {
            var maxAllowedWidth = MaximumSize.Width;
            var maxAllowedHeight = MaximumSize.Height;

            var calculatedWidth = _maxItemWidth + SystemInformation.BorderMultiplierFactor * 2 +
                                  SystemInformation.VerticalScrollBarWidth;
            var calculatedHeight = CalculateHeightBasedOnDataSource(maxAllowedHeight);

            Size = new Size(calculatedWidth, calculatedHeight);
        }

        private int CalculateHeightBasedOnDataSource(int maxHeight)
        {
            int numberOfItems = _dataSource.Count;
            var heightForAllItems = numberOfItems * ItemDefaultHeight;
            if (heightForAllItems > maxHeight)
            {
                return maxHeight - (maxHeight % ItemDefaultHeight);
            }

            return heightForAllItems;
        }

        #endregion

    }
}
