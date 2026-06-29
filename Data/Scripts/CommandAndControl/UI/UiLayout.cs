using VRageMath;

namespace AGS
{
    public enum UiHorizontalAlignment
    {
        Left,
        Center,
        Right,
        Stretch
    }

    public enum UiVerticalAlignment
    {
        Top,
        Center,
        Bottom,
        Stretch
    }

    public struct UiThickness
    {
        public float Left;
        public float Top;
        public float Right;
        public float Bottom;

        public UiThickness(float uniform)
        {
            Left = uniform;
            Top = uniform;
            Right = uniform;
            Bottom = uniform;
        }

        public UiThickness(float horizontal, float vertical)
        {
            Left = horizontal;
            Right = horizontal;
            Top = vertical;
            Bottom = vertical;
        }

        public UiThickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public float Horizontal
        {
            get { return Left + Right; }
        }

        public float Vertical
        {
            get { return Top + Bottom; }
        }
    }

    public struct UiRect
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public UiRect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
        }

        public Vector2 Size
        {
            get { return new Vector2(Width, Height); }
        }

        public Vector2 Center
        {
            get { return new Vector2(X + (Width * 0.5f), Y + (Height * 0.5f)); }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public UiRect Deflate(UiThickness thickness)
        {
            return new UiRect(X + thickness.Left, Y + thickness.Top, Width - thickness.Horizontal, Height - thickness.Vertical);
        }

        public bool Contains(Vector2 point)
        {
            return point.X >= X && point.Y >= Y && point.X <= Right && point.Y <= Bottom;
        }
    }

    public static class UiLayout
    {
        public static UiRect DockTop(ref UiRect remaining, float height)
        {
            var rect = new UiRect(remaining.X, remaining.Y, remaining.Width, height);
            remaining = new UiRect(remaining.X, remaining.Y + height, remaining.Width, remaining.Height - height);
            return rect;
        }

        public static UiRect DockBottom(ref UiRect remaining, float height)
        {
            var rect = new UiRect(remaining.X, remaining.Bottom - height, remaining.Width, height);
            remaining = new UiRect(remaining.X, remaining.Y, remaining.Width, remaining.Height - height);
            return rect;
        }

        public static UiRect DockLeft(ref UiRect remaining, float width)
        {
            var rect = new UiRect(remaining.X, remaining.Y, width, remaining.Height);
            remaining = new UiRect(remaining.X + width, remaining.Y, remaining.Width - width, remaining.Height);
            return rect;
        }

        public static UiRect DockRight(ref UiRect remaining, float width)
        {
            var rect = new UiRect(remaining.Right - width, remaining.Y, width, remaining.Height);
            remaining = new UiRect(remaining.X, remaining.Y, remaining.Width - width, remaining.Height);
            return rect;
        }

        public static UiRect Align(UiRect parent, Vector2 size, UiHorizontalAlignment horizontal, UiVerticalAlignment vertical, UiThickness margin)
        {
            var width = horizontal == UiHorizontalAlignment.Stretch ? parent.Width - margin.Horizontal : size.X;
            var height = vertical == UiVerticalAlignment.Stretch ? parent.Height - margin.Vertical : size.Y;

            var x = parent.X + margin.Left;
            if (horizontal == UiHorizontalAlignment.Center)
            {
                x = parent.X + ((parent.Width - width) * 0.5f) + (margin.Left - margin.Right);
            }
            else if (horizontal == UiHorizontalAlignment.Right)
            {
                x = parent.Right - width - margin.Right;
            }

            var y = parent.Y + margin.Top;
            if (vertical == UiVerticalAlignment.Center)
            {
                y = parent.Y + ((parent.Height - height) * 0.5f) + (margin.Top - margin.Bottom);
            }
            else if (vertical == UiVerticalAlignment.Bottom)
            {
                y = parent.Bottom - height - margin.Bottom;
            }

            return new UiRect(x, y, width, height);
        }
    }
}
