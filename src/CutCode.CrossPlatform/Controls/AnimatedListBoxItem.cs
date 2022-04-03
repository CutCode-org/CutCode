using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.Styling;

namespace CutCode.CrossPlatform.Controls;

[PseudoClasses(":Added", ":Removed")]
public class AnimatedListBoxItem : ListBoxItem, IStyleable
{
    Type IStyleable.StyleKey => typeof(AnimatedListBoxItem);

    private bool isAttached = false;
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (isAttached) return;
        PseudoClasses.Set(":Added", true);
        isAttached = true;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        PseudoClasses.Set(":Removed", true);
        base.OnDetachedFromVisualTree(e);
    }
}