using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KaffeemaschineWPF.Views
{
    /// <summary>
    /// Interaktionslogik für MarqueeTextUserControl.xaml
    /// </summary>
    public partial class MarqueeTextUserControl : UserControl
    {
        public MarqueeTextUserControl()
        {
            InitializeComponent();
            CanvasMain.Height = Height;
            CanvasMain.Width = Width;
        }

        public ScrollDirection ScrollDirection { get; set; }
        public double ScrollDurationInSeconds { get; set; }
        public String Text { set { TextBlockMain.Text = value; } }

        public void ScrollText(ScrollDirection scrollDirection)
        {
            switch (scrollDirection)
            {
                case ScrollDirection.LeftToRight:
                    LeftToRightMarquee();
                    break;
                case ScrollDirection.RightToLeft:
                    RightToLeftMarquee();
                    break;
                case ScrollDirection.TopToBottom:
                    TopToBottomMarquee();
                    break;
                case ScrollDirection.BottomToTop:
                    BottomToTopMarquee();
                    break;
            }
        }

        private void LeftToRightMarquee()
        {
            double height = CanvasMain.ActualHeight - TextBlockMain.ActualHeight;
            TextBlockMain.Margin = new Thickness(0, height / 2, 0, 0);

            var doubleAnimation = new DoubleAnimation
            {
                From = -TextBlockMain.ActualWidth,
                To = CanvasMain.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(ScrollDurationInSeconds))
            };

            TextBlockMain.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }

        private void RightToLeftMarquee()
        {
            double height = CanvasMain.ActualHeight - TextBlockMain.ActualHeight;
            //TextBlockMain.Margin = new Thickness(0, height / 2, 0, 0);

            var doubleAnimation = new DoubleAnimation
            {
                From = -TextBlockMain.ActualWidth,
                To = CanvasMain.ActualWidth,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(ScrollDurationInSeconds))
            };

            TextBlockMain.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        }

        private void TopToBottomMarquee()
        {
            double width = CanvasMain.ActualWidth - TextBlockMain.ActualWidth;
            TextBlockMain.Margin = new Thickness(width / 2, 0, 0, 0);

            var doubleAnimation = new DoubleAnimation
            {
                From = -TextBlockMain.ActualHeight,
                To = CanvasMain.ActualHeight,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(ScrollDurationInSeconds))
            };

            TextBlockMain.BeginAnimation(Canvas.TopProperty, doubleAnimation);
        }

        private void BottomToTopMarquee()
        {
            double width = CanvasMain.ActualWidth - TextBlockMain.ActualWidth;
            TextBlockMain.Margin = new Thickness(width / 2, 0, 0, 0);

            var doubleAnimation = new DoubleAnimation
            {
                From = -TextBlockMain.ActualHeight,
                To = CanvasMain.ActualHeight,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(ScrollDurationInSeconds))
            };

            TextBlockMain.BeginAnimation(Canvas.BottomProperty, doubleAnimation);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ScrollText(ScrollDirection);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollText(ScrollDirection);
        }
    }

    public enum ScrollDirection
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
}
