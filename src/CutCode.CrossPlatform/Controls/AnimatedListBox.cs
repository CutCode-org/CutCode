using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Metadata;
using Avalonia.Styling;
using AvaloniaEdit;

namespace CutCode.CrossPlatform.Controls;

public class AnimatedListBox : ListBox, IStyleable
{
    Type IStyleable.StyleKey => typeof(ListBox);
    
    protected override IItemContainerGenerator CreateItemContainerGenerator()
    {
        return new ItemContainerGenerator<AnimatedListBoxItem>(
            this, 
            AnimatedListBoxItem.ContentProperty,
            AnimatedListBoxItem.ContentTemplateProperty);
    }
}