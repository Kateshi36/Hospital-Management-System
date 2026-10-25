using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HospitalManagementSystem.Helpers
{
    /// <summary>
    /// The hospital logo, used on the login screen, in the main window header and as the window icon.
    ///
    /// Files (both in the project's Assets folder, copied next to the .exe on build):
    ///   Assets\logo.png  - the logo picture (a square PNG with a transparent background looks best)
    ///   Assets\app.ico   - the same logo as an icon (window title bar, taskbar and the .exe file)
    /// If a file is missing the screens simply keep their text-only look, so the app always runs.
    /// </summary>
    public static class LogoHelper
    {
        private const string LogoFile = "logo.png";
        private const string IconFile = "app.ico";

        private static bool _loaded;
        private static Image? _logo;
        private static Icon? _icon;

        /// <summary>The logo picture, or null when Assets\logo.png is not there.</summary>
        public static Image? Logo
        {
            get
            {
                EnsureLoaded();
                return _logo;
            }
        }

        /// <summary>The window icon, or null when neither Assets\app.ico nor Assets\logo.png is there.</summary>
        public static Icon? AppIcon
        {
            get
            {
                EnsureLoaded();
                return _icon;
            }
        }

        /// <summary>Sets the window icon of a form (title bar and taskbar).</summary>
        public static void ApplyIcon(Form form)
        {
            Icon? icon = AppIcon;
            if (icon != null) form.Icon = icon;
        }

        /// <summary>
        /// Shows the logo inside a label (the login screen's logo area), replacing the label's text symbol.
        /// The picture is re-scaled whenever the label changes size (window resize, moving to a monitor with
        /// another scaling). Without a logo file the label keeps its text.
        /// </summary>
        public static void ShowInLabel(Label label)
        {
            if (Logo == null) return;

            void Refit()
            {
                Image? old = label.Image;
                label.Image = CreateScaledLogo(label.ClientSize.Width - label.Padding.Horizontal,
                                               label.ClientSize.Height - label.Padding.Vertical);
                old?.Dispose();
            }

            label.Text = string.Empty;
            label.ImageAlign = ContentAlignment.MiddleCenter;
            Refit();
            label.SizeChanged += (s, e) => Refit();
            label.Disposed += (s, e) => label.Image?.Dispose();
        }

        /// <summary>
        /// Puts a small logo at the left of the main window header, just before the application name,
        /// and moves the name to the right to make room. Without a logo file nothing changes.
        /// </summary>
        public static void AddToHeader(Control header, Label appName)
        {
            if (Logo == null) return;

            float scale = header.DeviceDpi / 96f;
            int gap = (int)Math.Round(8 * scale);
            int size = Math.Max(16, header.Height - (int)Math.Round(14 * scale));

            var picture = new PictureBox
            {
                Name = "picHeaderLogo",
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(size, size),
                Location = new Point(appName.Left, (header.Height - size) / 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Image = CreateScaledLogo(size, size),
                TabStop = false
            };
            picture.Disposed += (s, e) => picture.Image?.Dispose();

            header.Controls.Add(picture);
            picture.BringToFront();
            appName.Left += size + gap;
            appName.Width = Math.Max(appName.Width - size - gap, 50);
        }

        /// <summary>
        /// A copy of the logo resized to fit inside the given box (aspect ratio kept, high-quality scaling).
        /// The caller owns the returned image. Returns null when there is no logo.
        /// </summary>
        public static Image? CreateScaledLogo(int maxWidth, int maxHeight)
        {
            Image? source = Logo;
            if (source == null || maxWidth <= 0 || maxHeight <= 0) return null;

            double ratio = Math.Min((double)maxWidth / source.Width, (double)maxHeight / source.Height);
            int width = Math.Max(1, (int)Math.Round(source.Width * ratio));
            int height = Math.Max(1, (int)Math.Round(source.Height * ratio));

            var result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(source, new Rectangle(0, 0, width, height));
            }
            return result;
        }

        private static void EnsureLoaded()
        {
            if (_loaded) return;
            _loaded = true;

            string folder = Path.Combine(AppContext.BaseDirectory, "Assets");

            try
            {
                string logoPath = Path.Combine(folder, LogoFile);
                if (File.Exists(logoPath))
                {
                    // Load through a memory copy so the file is not kept locked while the app runs.
                    byte[] bytes = File.ReadAllBytes(logoPath);
                    using var stream = new MemoryStream(bytes);
                    using var loaded = Image.FromStream(stream);
                    _logo = new Bitmap(loaded);
                }
            }
            catch (Exception)
            {
                _logo = null;   // unreadable image: keep the text-only look
            }

            try
            {
                string iconPath = Path.Combine(folder, IconFile);
                if (File.Exists(iconPath))
                {
                    _icon = new Icon(iconPath);
                }
                else if (_logo != null)
                {
                    using var small = (Bitmap)CreateScaledLogo(64, 64)!;
                    _icon = Icon.FromHandle(small.GetHicon());
                }
            }
            catch (Exception)
            {
                _icon = null;
            }
        }
    }
}
