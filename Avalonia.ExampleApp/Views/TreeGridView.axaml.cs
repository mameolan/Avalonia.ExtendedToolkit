using Avalonia;
using Avalonia.Controls;
using Avalonia.ExtendedToolkit;
using Avalonia.Markup.Xaml;
using System;

namespace Avalonia.ExampleApp.Views
{
    public partial class TreeGridView : UserControl
    {
        private const int Levels = 3;
        private const int Roots = 10;
        private const int ItemsPerLevel = 1;
        private int childCount = 0;
        private int value;
        private TreeGridModel model;


        public TreeGridView()
        {
            this.InitializeComponent();

            // Initialize the model
            InitModel();

            DataGrid grid = this.FindControl<DataGrid>("grid");

            // Set the model for the grid
            //.edited
            grid.ItemsSource = model.FlatModel;

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitModel()
        {
            // Create the model
            model = new TreeGridModel();

            // Add a bunch of items at the root
            for (int count = 0; count < Roots; count++)
            {
                // Create the root item
                Item root = new Item(String.Format("Root {0}", count), value++, true);

                // Add children to the root
                AddChildren(root);

                // Add the root to the model
                model.Add(root);
            }
        }

        private void AddChildren(Item item, int level = 0)
        {
            // Determine if the item will have children
            bool hasChildren = (level < Levels);

            // Create children for the item
            for (int count = 0; count < ItemsPerLevel; count++)
            {
                childCount++;
                // Create the child
                Item child = new Item(String.Format("Child {0}, Level {1}", childCount, level), value++, hasChildren);
                

                // Does the child have children?
                if (hasChildren)
                {
                    // Recursively add children to the child
                    AddChildren(child, (level + 1));
                }

                // Add the child to the item
                item.Children.Add(child);
            }
        }



    }

    public class Item : TreeGridElement
    {
        public int Value { get; private set; }
        public object Content { get; private set; }

        public Item(string name, int value, bool hasChildren)
        {
            // Initialize the item
            Content = name;
            Value = value;
            HasChildren = hasChildren;
        }
    }

}
