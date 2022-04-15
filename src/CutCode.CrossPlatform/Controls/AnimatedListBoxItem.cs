using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CutCode.CrossPlatform.Controls;

[PseudoClasses(":Added", ":Removed")]
public class AnimatedListBoxItem : ListBoxItem, IStyleable
{
    public AnimatedListBoxItem()
    {
    }
    Type IStyleable.StyleKey => typeof(AnimatedListBoxItem);

    private bool isAttached = false;
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        if (isAttached) return;
        PseudoClasses.Set(":Added", true);
        isAttached = true;
        base.OnAttachedToVisualTree(e);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        PseudoClasses.Set(":Removed", true);
        base.OnDetachedFromVisualTree(e);
    }
}