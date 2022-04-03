using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.Styling;

namespace CutCode.CrossPlatform.Controls;

[PseudoClasses(":Added"), PseudoClasses(":Removed")]
public class AnimatedListBoxItem : ListBoxItem, IStyleable
{
    Type IStyleable.StyleKey => typeof(ListBoxItem);
    
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        PseudoClasses.Set(":Added", true);
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        PseudoClasses.Set(":Removed", true);
        base.OnDetachedFromVisualTree(e);
    }
}