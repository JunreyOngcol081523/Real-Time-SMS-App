using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Time_SMS_App
{
    public class ItemIndexBehavior : Behavior<Label>
    {
        protected override void OnAttachedTo(Label label)
        {
            base.OnAttachedTo(label);

            label.BindingContextChanged += (s, e) =>
            {
                if (label.BindingContext is Casualty item &&
                    label.Parent is Element parent &&
                    FindParentCollectionView(parent) is CollectionView collectionView)
                {
                    var group = collectionView.ItemsSource as IEnumerable<IEnumerable<Casualty>>;
                    if (group != null)
                    {
                        foreach (var g in group)
                        {
                            var list = g.ToList();
                            var index = list.IndexOf(item);
                            if (index >= 0)
                            {
                                label.Text = $"{index + 1}.";
                                break;
                            }
                        }
                    }
                }
            };
        }

        private CollectionView FindParentCollectionView(Element element)
        {
            while (element != null && !(element is CollectionView))
                element = element.Parent;

            return element as CollectionView;
        }
    }

}
