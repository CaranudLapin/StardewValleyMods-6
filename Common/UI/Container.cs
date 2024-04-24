using Microsoft.Xna.Framework.Graphics;

namespace Common.UI;

public abstract class Container : Element
{
    public List<Element> Children = new();

    public void AddChild(params Element[] elements)
    {
        foreach (var element in elements)
        {
            element.Parent?.RemoveChild(element);
            Children.Add(element);
            element.Parent = this;
        }
    }

    public void RemoveChild(Element element)
    {
        if (element.Parent != this) throw new ArgumentException("Element must be a child of this container.");
        Children.Remove(element);
        element.Parent = null;
    }

    public override void Update()
    {
        base.Update();
        foreach (var element in Children) element.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (IsHidden()) return;
        foreach (var element in Children) element.Draw(spriteBatch);
    }
}