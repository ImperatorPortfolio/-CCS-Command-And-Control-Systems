using System;
using VRageMath;

namespace AGS
{
    // Lays children out in equal-sized cells. If Rows/Columns are 0 they are derived
    // from the child count (columns first, roughly square).
    public sealed class UniformGrid : Panel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public float Spacing { get; set; }

        private int VisibleCount()
        {
            var count = 0;
            for (var i = 0; i < Children.Count; i++)
            {
                if (Children[i].Visibility != UiVisibility.Collapsed)
                {
                    count++;
                }
            }
            return count;
        }

        private void ResolveDimensions(out int rows, out int cols)
        {
            var count = Math.Max(1, VisibleCount());
            if (Columns > 0)
            {
                cols = Columns;
                rows = Rows > 0 ? Rows : (int)Math.Ceiling(count / (float)cols);
            }
            else if (Rows > 0)
            {
                rows = Rows;
                cols = (int)Math.Ceiling(count / (float)rows);
            }
            else
            {
                cols = (int)Math.Ceiling(Math.Sqrt(count));
                rows = (int)Math.Ceiling(count / (float)cols);
            }

            rows = Math.Max(1, rows);
            cols = Math.Max(1, cols);
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            int rows, cols;
            ResolveDimensions(out rows, out cols);
            var cellW = (availableSize.X - (Spacing * (cols - 1))) / cols;
            var cellH = (availableSize.Y - (Spacing * (rows - 1))) / rows;
            var cell = new Vector2(Math.Max(0f, cellW), Math.Max(0f, cellH));
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].Measure(cell, context);
            }
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            int rows, cols;
            ResolveDimensions(out rows, out cols);
            var cellW = (rect.Width - (Spacing * (cols - 1))) / cols;
            var cellH = (rect.Height - (Spacing * (rows - 1))) / rows;
            cellW = Math.Max(0f, cellW);
            cellH = Math.Max(0f, cellH);

            var slot = 0;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Visibility == UiVisibility.Collapsed)
                {
                    continue;
                }

                var col = slot % cols;
                var row = slot / cols;
                var x = rect.X + (col * (cellW + Spacing));
                var y = rect.Y + (row * (cellH + Spacing));
                child.Arrange(new UiRect(x, y, cellW, cellH), context);
                slot++;
            }
        }
    }

    // Lays children left-to-right (or top-to-bottom), wrapping to a new line when the
    // current one is full. Sizes from each child's measured DesiredSize.
    public sealed class WrapPanel : Panel
    {
        public UiOrientation Orientation { get; set; }
        public float ItemSpacing { get; set; }
        public float LineSpacing { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].Measure(availableSize, context);
            }

            if (Orientation == UiOrientation.Vertical)
            {
                return MeasureVertical(availableSize);
            }

            return MeasureHorizontal(availableSize);
        }

        private Vector2 MeasureHorizontal(Vector2 availableSize)
        {
            var lineWidth = 0f;
            var totalWidth = 0f;
            var totalHeight = 0f;
            var lineHeight = 0f;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Visibility == UiVisibility.Collapsed)
                {
                    continue;
                }

                var w = child.DesiredSize.X;
                var h = child.DesiredSize.Y;
                if (lineWidth > 0f && lineWidth + ItemSpacing + w > availableSize.X)
                {
                    totalWidth = Math.Max(totalWidth, lineWidth);
                    totalHeight += lineHeight + LineSpacing;
                    lineWidth = w;
                    lineHeight = h;
                }
                else
                {
                    lineWidth += (lineWidth > 0f ? ItemSpacing : 0f) + w;
                    lineHeight = Math.Max(lineHeight, h);
                }
            }

            totalWidth = Math.Max(totalWidth, lineWidth);
            totalHeight += lineHeight;
            return new Vector2(Math.Min(availableSize.X, totalWidth), totalHeight);
        }

        private Vector2 MeasureVertical(Vector2 availableSize)
        {
            var columnHeight = 0f;
            var totalHeight = 0f;
            var totalWidth = 0f;
            var columnWidth = 0f;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Visibility == UiVisibility.Collapsed)
                {
                    continue;
                }

                var w = child.DesiredSize.X;
                var h = child.DesiredSize.Y;
                if (columnHeight > 0f && columnHeight + ItemSpacing + h > availableSize.Y)
                {
                    totalHeight = Math.Max(totalHeight, columnHeight);
                    totalWidth += columnWidth + LineSpacing;
                    columnHeight = h;
                    columnWidth = w;
                }
                else
                {
                    columnHeight += (columnHeight > 0f ? ItemSpacing : 0f) + h;
                    columnWidth = Math.Max(columnWidth, w);
                }
            }

            totalHeight = Math.Max(totalHeight, columnHeight);
            totalWidth += columnWidth;
            return new Vector2(totalWidth, Math.Min(availableSize.Y, totalHeight));
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            if (Orientation == UiOrientation.Vertical)
            {
                ArrangeVertical(rect, context);
                return;
            }

            var cursorX = rect.X;
            var lineY = rect.Y;
            var lineHeight = 0f;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Visibility == UiVisibility.Collapsed)
                {
                    continue;
                }

                var w = Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                var h = Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                if (cursorX > rect.X && cursorX + w > rect.Right)
                {
                    cursorX = rect.X;
                    lineY += lineHeight + LineSpacing;
                    lineHeight = 0f;
                }

                child.Arrange(new UiRect(cursorX, lineY, w, h), context);
                cursorX += w + ItemSpacing;
                lineHeight = Math.Max(lineHeight, h);
            }
        }

        private void ArrangeVertical(UiRect rect, UiContext context)
        {
            var cursorY = rect.Y;
            var columnX = rect.X;
            var columnWidth = 0f;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Visibility == UiVisibility.Collapsed)
                {
                    continue;
                }

                var w = Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                var h = Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                if (cursorY > rect.Y && cursorY + h > rect.Bottom)
                {
                    cursorY = rect.Y;
                    columnX += columnWidth + LineSpacing;
                    columnWidth = 0f;
                }

                child.Arrange(new UiRect(columnX, cursorY, w, h), context);
                cursorY += h + ItemSpacing;
                columnWidth = Math.Max(columnWidth, w);
            }
        }
    }
}
