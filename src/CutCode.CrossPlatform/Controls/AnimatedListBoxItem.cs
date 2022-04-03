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
        base.OnAttachedToVisualTree(e);
        Attached(true);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        Attached(false);
        base.OnDetachedFromVisualTree(e);
    }
    
    private void Attached(bool attached)
    {
        PseudoClasses.Set(":Added", attached);
        PseudoClasses.Set(":Removed", !attached);
    }
}