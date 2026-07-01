using System;
using System.Collections.Generic;
using Sandbox.Game.GameSystems.TextSurfaceScripts;
using VRage.Game.GUI.TextPanel;
using VRageMath;

namespace AGS
{
    public abstract class UiElement
    {
        private readonly List<UiElement> _children = new List<UiElement>();

        public UiElement Parent { get; private set; }
        public IList<UiElement> Children { get { return _children; } }
        public UiVisibility Visibility { get; set; }
        public bool IsEnabled { get; set; }
        public bool Focusable { get; set; }
        public bool CapturesPointer { get; set; }
        public bool ClipToBounds { get; set; }
        public bool IsHovered { get; set; }
        public bool IsPressed { get; set; }
        public bool IsFocused { get; set; }
        public int ZIndex { get; set; }
        public UiThickness Margin { get; set; }
        public UiHorizontalAlignment HorizontalAlignment { get; set; }
        public UiVerticalAlignment VerticalAlignment { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float MinWidth { get; set; }
        public float MinHeight { get; set; }
        public UiRect ArrangedRect { get; protected set; }
        public Vector2 DesiredSize { get; protected set; }
        public string Name { get; set; }
        public string FocusKey { get; set; }
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public int GridRowSpan { get; set; }
        public int GridColumnSpan { get; set; }
        public UiDock Dock { get; set; }
        public float CanvasLeft { get; set; }
        public float CanvasTop { get; set; }
        public float CanvasWidth { get; set; }
        public float CanvasHeight { get; set; }

        protected UiElement()
        {
            Visibility = UiVisibility.Visible;
            IsEnabled = true;
            HorizontalAlignment = UiHorizontalAlignment.Stretch;
            VerticalAlignment = UiVerticalAlignment.Stretch;
            Width = -1f;
            Height = -1f;
            CanvasLeft = -1f;
            CanvasTop = -1f;
            CanvasWidth = -1f;
            CanvasHeight = -1f;
            GridRowSpan = 1;
            GridColumnSpan = 1;
            Dock = UiDock.Left;
        }

        public void AddChild(UiElement child)
        {
            if (child == null)
            {
                return;
            }

            child.Parent = this;
            _children.Add(child);
        }

        public void ClearChildren()
        {
            _children.Clear();
        }

        public virtual void Measure(Vector2 availableSize, UiContext context)
        {
            if (Visibility == UiVisibility.Collapsed)
            {
                DesiredSize = Vector2.Zero;
                return;
            }

            var inner = new Vector2(Math.Max(0f, availableSize.X - Margin.Horizontal), Math.Max(0f, availableSize.Y - Margin.Vertical));
            var desired = MeasureCore(inner, context);
            if (Width >= 0f)
            {
                desired.X = Width;
            }
            if (Height >= 0f)
            {
                desired.Y = Height;
            }
            desired.X = Math.Max(desired.X, MinWidth);
            desired.Y = Math.Max(desired.Y, MinHeight);
            DesiredSize = new Vector2(desired.X + Margin.Horizontal, desired.Y + Margin.Vertical);
        }

        protected virtual Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var max = Vector2.Zero;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.Measure(availableSize, context);
                max.X = Math.Max(max.X, child.DesiredSize.X);
                max.Y = Math.Max(max.Y, child.DesiredSize.Y);
            }
            return max;
        }

        public virtual void Arrange(UiRect finalRect, UiContext context)
        {
            if (Visibility == UiVisibility.Collapsed)
            {
                ArrangedRect = new UiRect();
                return;
            }

            var inner = finalRect.Deflate(Margin);
            var desiredWidth = Math.Max(0f, DesiredSize.X - Margin.Horizontal);
            var desiredHeight = Math.Max(0f, DesiredSize.Y - Margin.Vertical);
            var width = Width >= 0f ? Width : desiredWidth;
            var height = Height >= 0f ? Height : desiredHeight;
            if (HorizontalAlignment == UiHorizontalAlignment.Stretch && Width < 0f)
            {
                width = inner.Width;
            }
            else
            {
                width = Math.Min(inner.Width, width);
            }
            if (VerticalAlignment == UiVerticalAlignment.Stretch && Height < 0f)
            {
                height = inner.Height;
            }
            else
            {
                height = Math.Min(inner.Height, height);
            }
            width = Math.Max(width, MinWidth);
            height = Math.Max(height, MinHeight);
            ArrangedRect = UiLayout.Align(inner, new Vector2(width, height), HorizontalAlignment, VerticalAlignment, new UiThickness(0f));
            ArrangeCore(ArrangedRect, context);
        }

        protected virtual void ArrangeCore(UiRect rect, UiContext context)
        {
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].Arrange(rect, context);
            }
        }

        public virtual void Render(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
            if (Visibility == UiVisibility.Collapsed)
            {
                return;
            }

            RenderSelf(frame, origin, context);

            if (!RequiresZOrdering())
            {
                for (var i = 0; i < _children.Count; i++)
                {
                    _children[i].Render(frame, origin, context);
                }
                return;
            }

            // Lower ZIndex paints first so higher ZIndex children sit on top.
            var order = BuildZOrder();
            for (var i = 0; i < order.Count; i++)
            {
                _children[order[i]].Render(frame, origin, context);
            }
        }

        protected virtual void RenderSelf(MySpriteDrawFrame frame, Vector2 origin, UiContext context)
        {
        }

        public virtual UiElement HitTest(Vector2 point)
        {
            if (Visibility == UiVisibility.Collapsed || !IsEnabled)
            {
                return null;
            }

            if (ClipToBounds && !ArrangedRect.Contains(point))
            {
                return null;
            }

            if (!RequiresZOrdering())
            {
                for (var i = _children.Count - 1; i >= 0; i--)
                {
                    var hit = _children[i].HitTest(point);
                    if (hit != null)
                    {
                        return hit;
                    }
                }
            }
            else
            {
                // Test highest ZIndex first so the top-most child wins the hit.
                var order = BuildZOrder();
                for (var i = order.Count - 1; i >= 0; i--)
                {
                    var hit = _children[order[i]].HitTest(point);
                    if (hit != null)
                    {
                        return hit;
                    }
                }
            }

            if (ArrangedRect.Contains(point))
            {
                return this;
            }

            return null;
        }

        private bool RequiresZOrdering()
        {
            for (var i = 0; i < _children.Count; i++)
            {
                if (_children[i].ZIndex != 0)
                {
                    return true;
                }
            }

            return false;
        }

        // Stable ascending order by ZIndex; ties keep insertion order.
        private List<int> BuildZOrder()
        {
            var order = new List<int>(_children.Count);
            for (var i = 0; i < _children.Count; i++)
            {
                order.Add(i);
            }

            order.Sort(delegate(int a, int b)
            {
                var za = _children[a].ZIndex;
                var zb = _children[b].ZIndex;
                if (za != zb)
                {
                    return za < zb ? -1 : 1;
                }
                return a < b ? -1 : (a > b ? 1 : 0);
            });
            return order;
        }

        public virtual UiCommand OnClick(UiContext context)
        {
            return null;
        }

        public virtual UiCommand OnScroll(int delta, UiContext context)
        {
            return null;
        }

        // Called every frame the pointer is held after capturing this element.
        public virtual UiCommand OnDrag(UiContext context)
        {
            return null;
        }

        public UiElement FindByFocusKey(string focusKey)
        {
            if (string.IsNullOrEmpty(focusKey))
            {
                return null;
            }

            if (Focusable && GetFocusKey() == focusKey)
            {
                return this;
            }

            for (var i = 0; i < _children.Count; i++)
            {
                var found = _children[i].FindByFocusKey(focusKey);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        public void ResetInputState()
        {
            IsHovered = false;
            IsPressed = false;
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].ResetInputState();
            }
        }

        public void MarkHovered()
        {
            IsHovered = true;
            if (Parent != null)
            {
                Parent.MarkHovered();
            }
        }

        public void MarkPressed()
        {
            IsPressed = true;
            if (Parent != null)
            {
                Parent.MarkPressed();
            }
        }

        public void ApplyFocus(FocusManager focusManager)
        {
            IsFocused = Focusable && focusManager != null && focusManager.IsFocused(GetFocusKey());
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].ApplyFocus(focusManager);
            }
        }

        public string GetFocusKey()
        {
            if (!string.IsNullOrEmpty(FocusKey))
            {
                return FocusKey;
            }
            return !string.IsNullOrEmpty(Name) ? Name : GetType().Name;
        }
    }

    public abstract class FrameworkElement : UiElement
    {
    }

    public abstract class Control : FrameworkElement
    {
        public UiStyle Style { get; set; }
        public UiControlState State
        {
            get
            {
                if (!IsEnabled)
                {
                    return UiControlState.Disabled;
                }
                if (IsPressed)
                {
                    return UiControlState.Pressed;
                }
                if (IsFocused)
                {
                    return UiControlState.Focused;
                }
                if (IsHovered)
                {
                    return UiControlState.Hover;
                }
                return UiControlState.Normal;
            }
        }
    }

    public abstract class Panel : FrameworkElement
    {
    }

    public class ContentControl : Control
    {
        private UiElement _content;

        public UiElement Content
        {
            get { return _content; }
            set
            {
                ClearChildren();
                _content = value;
                if (_content != null)
                {
                    AddChild(_content);
                }
            }
        }
    }

    public class ItemsControl : Control
    {
        public IList<UiElement> Items
        {
            get { return Children; }
        }
    }

    public sealed class CanvasPanel : Panel
    {
        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var max = Vector2.Zero;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.Measure(availableSize, context);
                max.X = Math.Max(max.X, child.DesiredSize.X);
                max.Y = Math.Max(max.Y, child.DesiredSize.Y);
            }
            return max;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var width = child.CanvasWidth >= 0f ? child.CanvasWidth : Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                var height = child.CanvasHeight >= 0f ? child.CanvasHeight : Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                var x = child.CanvasLeft >= 0f ? rect.X + child.CanvasLeft : rect.X;
                var y = child.CanvasTop >= 0f ? rect.Y + child.CanvasTop : rect.Y;
                child.Arrange(new UiRect(x, y, width, height), context);
            }
        }
    }

    public sealed class StackPanel : Panel
    {
        public UiOrientation Orientation { get; set; }
        public float Spacing { get; set; }

        public StackPanel()
        {
            Orientation = UiOrientation.Vertical;
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var width = 0f;
            var height = 0f;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.Measure(availableSize, context);
                if (Orientation == UiOrientation.Vertical)
                {
                    width = Math.Max(width, child.DesiredSize.X);
                    height += child.DesiredSize.Y;
                    if (i > 0)
                    {
                        height += Spacing;
                    }
                }
                else
                {
                    width += child.DesiredSize.X;
                    if (i > 0)
                    {
                        width += Spacing;
                    }
                    height = Math.Max(height, child.DesiredSize.Y);
                }
            }
            return new Vector2(width, height);
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var cursor = Orientation == UiOrientation.Vertical ? rect.Y : rect.X;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var desiredWidth = Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                var desiredHeight = Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                if (Orientation == UiOrientation.Vertical)
                {
                    var childHeight = child.Height >= 0f ? child.Height : desiredHeight;
                    child.Arrange(new UiRect(rect.X, cursor, rect.Width, childHeight), context);
                    cursor += child.ArrangedRect.Height + Spacing + child.Margin.Vertical;
                }
                else
                {
                    var childWidth = child.Width >= 0f ? child.Width : desiredWidth;
                    child.Arrange(new UiRect(cursor, rect.Y, childWidth, rect.Height), context);
                    cursor += child.ArrangedRect.Width + Spacing + child.Margin.Horizontal;
                }
            }
        }
    }

    public sealed class DockPanel : Panel
    {
        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            var remaining = new Vector2(availableSize.X, availableSize.Y);
            var used = Vector2.Zero;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                child.Measure(remaining, context);
                if (child.Dock == UiDock.Top || child.Dock == UiDock.Bottom)
                {
                    remaining.Y = Math.Max(0f, remaining.Y - child.DesiredSize.Y);
                    used.Y += child.DesiredSize.Y;
                    used.X = Math.Max(used.X, child.DesiredSize.X);
                }
                else
                {
                    remaining.X = Math.Max(0f, remaining.X - child.DesiredSize.X);
                    used.X += child.DesiredSize.X;
                    used.Y = Math.Max(used.Y, child.DesiredSize.Y);
                }
            }
            return new Vector2(Math.Max(used.X, availableSize.X - remaining.X), Math.Max(used.Y, availableSize.Y - remaining.Y));
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            var remaining = rect;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                if (child.Dock == UiDock.Top)
                {
                    var desiredHeight = child.Height >= 0f ? child.Height : Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                    child.Arrange(UiLayout.DockTop(ref remaining, desiredHeight), context);
                }
                else if (child.Dock == UiDock.Bottom)
                {
                    var desiredHeight = child.Height >= 0f ? child.Height : Math.Max(0f, child.DesiredSize.Y - child.Margin.Vertical);
                    child.Arrange(UiLayout.DockBottom(ref remaining, desiredHeight), context);
                }
                else if (child.Dock == UiDock.Right)
                {
                    var desiredWidth = child.Width >= 0f ? child.Width : Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                    child.Arrange(UiLayout.DockRight(ref remaining, desiredWidth), context);
                }
                else
                {
                    var desiredWidth = child.Width >= 0f ? child.Width : Math.Max(0f, child.DesiredSize.X - child.Margin.Horizontal);
                    child.Arrange(UiLayout.DockLeft(ref remaining, desiredWidth), context);
                }
            }
        }
    }

    public sealed class GridPanel : Panel
    {
        public IList<GridDefinition> Rows { get; private set; }
        public IList<GridDefinition> Columns { get; private set; }

        public GridPanel()
        {
            Rows = new List<GridDefinition>();
            Columns = new List<GridDefinition>();
        }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            EnsureDefinitions();
            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].Measure(availableSize, context);
            }
            CalculateAutoDefinitions(Rows, true);
            CalculateAutoDefinitions(Columns, false);
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            EnsureDefinitions();
            ResolveDefinitions(Columns, rect.Width);
            ResolveDefinitions(Rows, rect.Height);

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var cell = GetCellRect(rect, child.GridRow, child.GridColumn, child.GridRowSpan, child.GridColumnSpan);
                child.Arrange(cell, context);
            }
        }

        private void EnsureDefinitions()
        {
            if (Rows.Count == 0)
            {
                Rows.Add(new GridDefinition(UiLength.Star(1f)));
            }
            if (Columns.Count == 0)
            {
                Columns.Add(new GridDefinition(UiLength.Star(1f)));
            }
        }

        private void CalculateAutoDefinitions(IList<GridDefinition> definitions, bool rows)
        {
            for (var i = 0; i < definitions.Count; i++)
            {
                if (definitions[i].Length.UnitType == GridUnitType.Auto)
                {
                    definitions[i].ActualSize = 0f;
                }
                else if (definitions[i].Length.UnitType == GridUnitType.Pixel)
                {
                    definitions[i].ActualSize = definitions[i].Length.Value;
                }
            }

            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var index = rows ? child.GridRow : child.GridColumn;
                if (index < 0 || index >= definitions.Count)
                {
                    continue;
                }
                if (definitions[index].Length.UnitType == GridUnitType.Auto)
                {
                    var size = rows ? child.DesiredSize.Y : child.DesiredSize.X;
                    if (size > definitions[index].ActualSize)
                    {
                        definitions[index].ActualSize = size;
                    }
                }
            }
        }

        private void ResolveDefinitions(IList<GridDefinition> definitions, float total)
        {
            var used = 0f;
            var starWeight = 0f;
            for (var i = 0; i < definitions.Count; i++)
            {
                if (definitions[i].Length.UnitType == GridUnitType.Pixel || definitions[i].Length.UnitType == GridUnitType.Auto)
                {
                    used += definitions[i].ActualSize;
                }
                else
                {
                    starWeight += definitions[i].Length.Value;
                }
            }

            var remaining = Math.Max(0f, total - used);
            for (var i = 0; i < definitions.Count; i++)
            {
                if (definitions[i].Length.UnitType == GridUnitType.Star)
                {
                    definitions[i].ActualSize = starWeight > 0f ? remaining * (definitions[i].Length.Value / starWeight) : 0f;
                }
            }
        }

        private UiRect GetCellRect(UiRect rect, int row, int column, int rowSpan, int columnSpan)
        {
            var x = rect.X;
            for (var i = 0; i < column && i < Columns.Count; i++)
            {
                x += Columns[i].ActualSize;
            }
            var y = rect.Y;
            for (var i = 0; i < row && i < Rows.Count; i++)
            {
                y += Rows[i].ActualSize;
            }
            var width = 0f;
            for (var i = column; i < Math.Min(Columns.Count, column + Math.Max(1, columnSpan)); i++)
            {
                width += Columns[i].ActualSize;
            }
            var height = 0f;
            for (var i = row; i < Math.Min(Rows.Count, row + Math.Max(1, rowSpan)); i++)
            {
                height += Rows[i].ActualSize;
            }
            return new UiRect(x, y, width, height);
        }
    }

    public sealed class ScrollViewer : ContentControl
    {
        public UiCommand ScrollUpCommand { get; set; }
        public UiCommand ScrollDownCommand { get; set; }

        protected override Vector2 MeasureCore(Vector2 availableSize, UiContext context)
        {
            if (Content != null)
            {
                Content.Measure(availableSize, context);
                return new Vector2(Math.Min(availableSize.X, Content.DesiredSize.X), Math.Min(availableSize.Y, Content.DesiredSize.Y));
            }
            return availableSize;
        }

        protected override void ArrangeCore(UiRect rect, UiContext context)
        {
            if (Content != null)
            {
                Content.Arrange(rect, context);
            }
        }

        public override UiCommand OnScroll(int delta, UiContext context)
        {
            if (delta > 0 && ScrollUpCommand != null)
            {
                return ScrollUpCommand;
            }
            if (delta < 0 && ScrollDownCommand != null)
            {
                return ScrollDownCommand;
            }
            return null;
        }
    }
}
