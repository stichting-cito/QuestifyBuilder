using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Questify.Builder.UI.Wpf.Presentation.ItemEditor
{
    public class AutoGreyableImage : Image
    {
        static AutoGreyableImage()
        {
            IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, OnAutoGreyScaleImageIsEnabledPropertyChanged));
            SourceProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(null, OnAutoGreyScaleImageSourcePropertyChanged));
        }

        protected static AutoGreyableImage GetImageWithSource(DependencyObject source)
        {
            var image = source as AutoGreyableImage;

            if (image?.Source == null)
            {
                return null;
            }

            return image;
        }

        protected static void OnAutoGreyScaleImageSourcePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs ars)
        {
            AutoGreyableImage image = GetImageWithSource(source);
            if (image != null)
            {
                ApplyGreyScaleImage(image, image.IsEnabled);
            }
        }

        protected static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            AutoGreyableImage image = GetImageWithSource(source);
            if (image != null)
            {
                var isEnabled = Convert.ToBoolean(args.NewValue);
                ApplyGreyScaleImage(image, isEnabled);
            }
        }

        protected static void ApplyGreyScaleImage(AutoGreyableImage autoGreyScaleImage, Boolean isEnabled)
        {
            if (autoGreyScaleImage == null)
            {
                throw new ArgumentNullException(nameof(autoGreyScaleImage));
            }

            if (!isEnabled)
            {
                BitmapSource bitmapImage;

                if (autoGreyScaleImage.Source is FormatConvertedBitmap)
                {
                    return;
                }
                if (autoGreyScaleImage.Source is BitmapSource)
                {
                    bitmapImage = (BitmapSource)autoGreyScaleImage.Source;
                }
                else
                {
                    bitmapImage = new BitmapImage(new Uri(autoGreyScaleImage.Source.ToString()));
                }
                FormatConvertedBitmap conv = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);
                autoGreyScaleImage.Source = conv;

                autoGreyScaleImage.OpacityMask = new ImageBrush(((FormatConvertedBitmap)autoGreyScaleImage.Source).Source);
            }
            else
            {
                if (autoGreyScaleImage.Source is FormatConvertedBitmap)
                {
                    autoGreyScaleImage.Source = ((FormatConvertedBitmap)autoGreyScaleImage.Source).Source;
                }
                else if (autoGreyScaleImage.Source is BitmapSource)
                {
                    return;
                }

                autoGreyScaleImage.OpacityMask = null;
            }

        }
    }
}
